using System;
using System.Collections.Generic;
using System.Text;

namespace NextGenSoftware.Holochain.HoloNET.Client
{
    public interface ILogger
    {
        void Log(string message, LogType type);
    }

    public enum LogType
    {
        Debug,
        Info,
        Warn,
        Error
    }
}
