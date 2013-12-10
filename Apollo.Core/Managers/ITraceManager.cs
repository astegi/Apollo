using System;
using Apollo.Core.Attributes;
using Apollo.Core.Entities;

namespace Apollo.Core.Managers
{
    [ProxyContract]
    public interface ITraceManager
    {
        void Trace(string message);
        void Debug(string message);
        void LogMessage(int messageCode, string details);
        void LogException(Exception ex);

        TraceEvent GetTraceEvent(int messageCode);
    }
}
