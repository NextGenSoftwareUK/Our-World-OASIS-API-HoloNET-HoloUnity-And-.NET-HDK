using System;
using System.Collections.Generic;
using System.Text;

namespace NextGenSoftware.Holochain.HoloNET
{
    public struct HoloNETConfig
    {
        public int TimeOutSeconds { get; set; }  //TODO: Move these into a Config struct property.
        public int KeepAliveSeconds { get; set; }
        public int ReconnectionAttempts { get; set; }
        public int ReconnectionIntervalSeconds { get; set; }
        public int SendChunkSize { get; set; }
        public int ReceiveChunkSize { get; set; }
    }
}
