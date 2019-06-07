using System;
using System.Net.WebSockets;

namespace NextGenSoftware.Holochain.HoloNET
{
    public class ConnectedEventArgs : EventArgs
    {
        public ConnectedEventArgs(string endPoint)
        {
            EndPoint = endPoint;
        }

        public string EndPoint { get; private set; }
    }

    public class DisconnectedEventArgs : EventArgs
    {
        public DisconnectedEventArgs(string endPoint, string reason)
        {
            EndPoint = endPoint;
            Reason = reason;
        }

        public string EndPoint { get; private set; }
        public string Reason { get; private set; }
    }

    public class ErrorEventArgs : EventArgs
    {
        public ErrorEventArgs(string endPoint, string reason, Exception errorDetails)
        {
            EndPoint = endPoint;
            Reason = reason;
            ErrorDetails = errorDetails;
        }

        public ErrorEventArgs(string endPoint, string reason)
        {
            EndPoint = endPoint;
            Reason = reason;
        }

        public string EndPoint { get; private set; }
        public string Reason { get; private set; }
        public Exception ErrorDetails { get; private set; }
    }

    public class DataReceivedEventArgs : EventArgs
    {
        public DataReceivedEventArgs(string endPoint, string rawJSONData, WebSocketReceiveResult webSocketResult)
        {
            EndPoint = endPoint;
            RawJSONData = rawJSONData;
            WebSocketResult = webSocketResult;
        }

        public string EndPoint { get; private set; }
        public string RawJSONData { get; private set; }
        public WebSocketReceiveResult WebSocketResult { get; private set; }
    }

    public class ZomeFunctionCallBackEventArgs : EventArgs
    {
        public ZomeFunctionCallBackEventArgs(string id, string endPoint, string instance, string zome, string zomeFunction, string zomeReturnData, string rawJSONData, WebSocketReceiveResult webSocketResult)
        {
            Id = id;
            EndPoint = endPoint;
            Instance = instance;
            Zome = zome;
            ZomeFunction = zomeFunction;
            ZomeReturnData = zomeReturnData;
            RawJSONData = rawJSONData;
            WebSocketResult = webSocketResult;
        }

        public string Id { get; private set; }
        public string EndPoint { get; private set; }
        public string Instance { get; private set; }
        public string Zome { get; private set; }
        public string ZomeFunction { get; private set; }
        public string ZomeReturnData { get; private set; }
        public string RawJSONData { get; private set; }
        public WebSocketReceiveResult WebSocketResult { get; private set; }
    }

}
