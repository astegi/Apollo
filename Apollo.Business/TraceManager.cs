using System;
using Apollo.Business.Attributes;
using Apollo.Core.Miscellaneous;
using Apollo.Core.Managers;
using Apollo.Core.Entities;
using Apollo.Core.Cache;

namespace Apollo.Business
{
    [ProxyImplementation]
    public class TraceManager : MarshalByRefObject, ITraceManager
    {
        public void Trace(string message)
        {
            Tracer.Trace(message);
        }

        public void Debug(string message)
        {
            Tracer.Debug(message);
        }

        public void LogMessage(int messageCode, string details)
        {
            TraceEvent message = GetTraceEvent(messageCode);
            Tracer.LogMessage(message, details);
        }

        public void LogException(Exception ex)
        {
            Tracer.LogException(ex);
        }


        public TraceEvent GetTraceEvent(int messageCode)
        {
            return CacheManager.Instance.TraceEventCache.Find(x => x.MessageCode == messageCode);
        }
    }
}