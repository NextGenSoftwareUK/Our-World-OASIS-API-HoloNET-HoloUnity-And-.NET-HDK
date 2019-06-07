using System;
using System.Text;
using System.Threading;
using System.Net.WebSockets;
using Newtonsoft.Json ;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NextGenSoftware.Holochain.HoloNET
{
    public class HoloNET
    {
        private const int ReceiveChunkSizeDefault = 1024;
        private const int SendChunkSizeDefault = 1024;
        private const int KeepAliveSecondsDefault = 30;
        private const int TimeOutSecondsDefault = 30; 
        private const int ReconnectionAttemptsDefault = 5;
        private const int ReconnectionIntervalSecondsDefault = 5;

        private readonly CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();
        private readonly CancellationToken _cancellationToken;
        private Dictionary<string, string> _instanceLookup = new Dictionary<string, string>();
        private Dictionary<string, string> _zomeLookup = new Dictionary<string, string>();
        private Dictionary<string, string> _funcLookup = new Dictionary<string, string>();
        private Dictionary<string, ZomeFunctionCallBack> _callbackLookup = new Dictionary<string, ZomeFunctionCallBack>();

        //Events
        public delegate void Connected(object sender, ConnectedEventArgs e);
        public event Connected OnConnected;

        public delegate void Disconnected(object sender, DisconnectedEventArgs e);
        public event Disconnected OnDisconnected;

        public delegate void DataReceived(object sender, DataReceivedEventArgs e);
        public event DataReceived OnDataReceived;

        public delegate void ZomeFunctionCallBack(object sender, ZomeFunctionCallBackEventArgs e);
        public event ZomeFunctionCallBack OnZomeFunctionCallBack;

        public delegate void Error(object sender, ErrorEventArgs e);
        public event Error OnError;

        // Properties
        public string EndPoint { get; private set; }
        public ClientWebSocket WebSocket { get; private set; }
        public HoloNETConfig Config { get; private set; }

        public WebSocketState State
        {
            get
            {
                return WebSocket.State;
            }
        }

        public HoloNET(string holochainURI)
        {
            WebSocket = new ClientWebSocket();
            WebSocket.Options.KeepAliveInterval = TimeSpan.FromSeconds(Config.KeepAliveSeconds == 0 ? KeepAliveSecondsDefault : Config.KeepAliveMilliseconds);
            EndPoint = holochainURI;

            _cancellationToken = _cancellationTokenSource.Token; //TODO: do something with this!
        }

        async public void Connect()
        {
            try
            {
                await WebSocket.ConnectAsync(new Uri(EndPoint), CancellationToken.None);

                if (WebSocket.State == WebSocketState.Open)
                {
                    OnConnected?.Invoke(this, new ConnectedEventArgs(EndPoint));
                    StartListen();
                }
            }
            catch (Exception e)
            {
                HandleError(string.Concat("Error occured connecting to ", EndPoint), e);
            }
        }

        public async void CallZomeFunctionAsync(string id, string instanceId, string zome, string function, object paramsObject, bool matchIdToInstanceZomeFuncInCallback = true)
        {
            CallZomeFunctionAsync(id, instanceId, zome, function, null, paramsObject, matchIdToInstanceZomeFuncInCallback);
        }

        public async void CallZomeFunctionAsync(string id, string instanceId, string zome, string function, ZomeFunctionCallBack callback, object paramsObject, bool matchIdToInstanceZomeFuncInCallback = true)
        {
            Console.WriteLine("CallZomeFunctionAsync ENTER");

            if (matchIdToInstanceZomeFuncInCallback)
            {
                _instanceLookup[id] = instanceId;
                _zomeLookup[id] = zome;
                _funcLookup[id] = function;
            }

            if (callback != null)
                _callbackLookup[id] = callback;

            SendMessageAsync(JsonConvert.SerializeObject(
                new
                {
                    jsonrpc = "2.0",
                    id = id,
                    method = "call",
                    @params = new { instance_id = instanceId, zome = zome, function = function, @params = paramsObject }
                }
            ));

            Console.WriteLine("CallZomeFunctionAsync EXIT");
        }

        public async void SendMessageAsync(string message)
        {
            Console.WriteLine("SendMessageAsync ENTER");

            if (WebSocket.State != WebSocketState.Open)
            {
                if (OnError != null)
                    OnError(this, new ErrorEventArgs(this.EndPoint, "Connection is not open."));
                else
                    throw new InvalidOperationException("Connection is not open.");
            }

            int sendChunkSize = Config.SendChunkSize == 0 ? SendChunkSizeDefault : Config.SendChunkSize;
            var messageBuffer = Encoding.UTF8.GetBytes(message);
            var messagesCount = (int)Math.Ceiling((double)messageBuffer.Length / sendChunkSize);

            for (var i = 0; i < messagesCount; i++)
            {
                var offset = (sendChunkSize * i);
                var count = sendChunkSize;
                var lastMessage = ((i + 1) == messagesCount);

                if ((count * (i + 1)) > messageBuffer.Length)                
                    count = messageBuffer.Length - offset;

                await WebSocket.SendAsync(new ArraySegment<byte>(messageBuffer, offset, count), WebSocketMessageType.Text, lastMessage, _cancellationToken);
            }

            //StartListen();

            Console.WriteLine("SendMessageAsync EXIT");
        }

        private async void StartListen()
        {
            var buffer = new byte[Config.ReceiveChunkSize == 0 ? ReceiveChunkSizeDefault : Config.ReceiveChunkSize];

            try
            {
                while (WebSocket.State == WebSocketState.Open)
                {
                    var stringResult = new StringBuilder();

                    WebSocketReceiveResult result;
                    do
                    {
                        using (var cts = new CancellationTokenSource(Config.TimeOutSeconds == 0 ? TimeOutSecondsDefault : Config.TimeOutSeconds))
                        {
                            result = await WebSocket.ReceiveAsync(new ArraySegment<byte>(buffer), cts.Token);
                            
                            //TODO: Handle timeout.
                            //TODO: Handle cancellation token being cancelled.
                            //TODO: Add re-connection attemps.
                        }

                        if (result.MessageType == WebSocketMessageType.Close)
                        {
                            string msg = "Closing because received close message from Holochain."; //TODO: Move all strings to constants at top or resources.strings
                            await WebSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, msg, CancellationToken.None);
                            OnDisconnected?.Invoke(this, new DisconnectedEventArgs(this.EndPoint, msg));

                            //AttemptReconnect(); //TODO: Not sure re-connect
                        }
                        else
                        {
                            var str = Encoding.UTF8.GetString(buffer, 0, result.Count);
                            stringResult.Append(str);
                        }

                    } while (!result.EndOfMessage);


                    string rawData = stringResult.ToString();
                    OnDataReceived?.Invoke(this, new DataReceivedEventArgs(EndPoint, rawData, result));

                    //var dataObject = JsonConvert.DeserializeObject(rawData);
                    //ZomeReturnObject test = JsonConvert.DeserializeObject<ZomeReturnObject>(rawData);
                    JObject data = JObject.Parse(rawData);

                    if (data.ContainsKey("signal"))
                    {
                        //TODO: Handle Signals here (need to check how thesr work in hc...)
                    }
                    else
                    {
                        string id = data["id"].ToString();
                        ZomeFunctionCallBackEventArgs args = new ZomeFunctionCallBackEventArgs(id, this.EndPoint,  GetItemFromCache(id, _instanceLookup), GetItemFromCache(id, _zomeLookup), GetItemFromCache(id, _funcLookup), data["result"].ToString(), rawData, result);

                        if (_callbackLookup[id] != null)
                            _callbackLookup[id].DynamicInvoke(this, args);

                        OnZomeFunctionCallBack?.Invoke(this, args);

                        _instanceLookup.Remove(id);
                        _zomeLookup.Remove(id);
                        _funcLookup.Remove(id);
                        _callbackLookup.Remove(id);
                    }   
                }
            }

            catch (Exception ex)
            {
                OnDisconnected?.Invoke(this, new DisconnectedEventArgs(this.EndPoint, string.Concat("Error occured: ", ex)));
                HandleError("Disconnected because an error occured.", ex);
                AttemptReconnect();
            }

            finally
            {
                WebSocket.Dispose();
            }
        }

        private async void AttemptReconnect()
        {
            for (int i = 0; i < (Config.ReconnectionAttempts == 0 ? ReconnectionAttemptsDefault : Config.ReconnectionAttempts); i++)
            {
                Console.WriteLine("Attempting to reconnect... Attempt " + i);
                Connect();

                if (WebSocket.State == WebSocketState.Open)
                    break;

                await Task.Delay(Config.ReconnectionIntervalSeconds == 0 ? ReconnectionIntervalSecondsDefault : Config.ReconnectionIntervalSeconds);
            }
        }

        private string GetItemFromCache(string id, Dictionary<string, string> cache)
        {
            return cache.ContainsKey(id) ? cache[id] : null;
        }

        private void HandleError(string message, Exception exception)
        {
            message = string.Concat(message, "\nError Details: ", exception.ToString());

            if (OnError != null)
                OnError(this, new ErrorEventArgs(this.EndPoint, message, exception));
            else
                throw new Exception(message, exception);

            //TODO: Add logging here.
        }
    }
}
