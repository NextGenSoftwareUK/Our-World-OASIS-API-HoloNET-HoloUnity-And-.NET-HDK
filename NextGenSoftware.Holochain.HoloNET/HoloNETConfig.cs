
using System.Configuration;

namespace NextGenSoftware.Holochain.HoloNET.Client
{
    public class HoloNETConfig
    {
        public int TimeOutSeconds { get; set; }
        public bool NeverTimeOut { get; set; }
        public int KeepAliveSeconds { get; set; }
        public int ReconnectionAttempts { get; set; }
        public int ReconnectionIntervalSeconds { get; set; }
        public int SendChunkSize { get; set; }
        public int ReceiveChunkSize { get; set; }
        public ErrorHandlingBehaviour ErrorHandlingBehaviour { get; set; }
    }

    public enum ErrorHandlingBehaviour
    {
        AlwaysThrowExceptionOnError,
        OnlyThrowExceptionIfNoErrorHandlerSubscribedToOnErrorEvent,
        NeverThrowExceptions
    }
}
