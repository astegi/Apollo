using System;
using Apollo.Core;
using Apollo.Core.Cache;
using NLog;
using NLog.Config;
using NLog.Layouts;
using NLog.Targets;
using Apollo.Core.Entities;

namespace Apollo.Core.Miscellaneous
{
    /// <summary>
    /// Naplózást támogató osztály
    /// </summary>
    public static class Tracer
    {
        static Logger logCore;
        static Tracer()
        {
            LogManager.Configuration = new NLog.Config.LoggingConfiguration();
            EventLogTarget target = new EventLogTarget();
            target.Log = "Application";
            target.Source = Constants.EVENTLOG_NAME;
            target.Layout = SimpleLayout.FromString("${message}${newline}${exception:format=tostring}");
            target.EventId = SimpleLayout.FromString("${event-context:item=messageCode}");
            LogManager.Configuration.AddTarget("eventLog", target);

            LoggingRule rule = new LoggingRule("*", LogLevel.FromString(Apollo.Core.Properties.Settings.Default.LogLevel), target);
            LogManager.Configuration.LoggingRules.Add(rule);

            LogManager.ReconfigExistingLoggers();
            logCore = LogManager.GetCurrentClassLogger();
        }

        /// <summary>
        ///   Elnaplózza a helyi naplóban az adott üzenetkódú
        ///   üzenetet a megadott részletekkel.
        /// </summary>
        /// <param name="messageCode"></param>
        /// <param name="details"></param>
        public static void Log(int messageCode, string details)
        {
            TraceEvent message = GetTraceEvent(messageCode);
            LogMessage(message, details);
        }

        /// <summary>
        ///   Elnaplózza a szerver naplójában az adott üzenetet.
        ///   Kliens oldalról ne hívjuk meg, használjuk helyette
        ///   a Log metódust!
        /// </summary>
        /// <param name="message"></param>
        /// <param name="details"></param>
        public static void LogMessage(TraceEvent message, string details)
        {
            if (message != null)
            {
                LogEventInfo eventInfo = new LogEventInfo();
                eventInfo.Message = String.IsNullOrEmpty(details) ? message.Description : String.Format("{0}: {1}", message.Description, details);
                eventInfo.Properties.Add("messageCode", message.MessageCode);
                eventInfo.LoggerName = logCore.Name;
                eventInfo.TimeStamp = DateTime.Now;

                switch (message.Severity)
                {
                    case Constants.TRACE_EVENT_ERROR:
                        eventInfo.Level = LogLevel.Error;
                        logCore.Log(eventInfo);
                        break;
                    case Constants.TRACE_EVENT_FAILURE:
                        eventInfo.Level = LogLevel.Fatal;
                        logCore.Log(eventInfo);
                        break;
                    case Constants.TRACE_EVENT_INFORMATION:
                    case Constants.TRACE_EVENT_SUCCESS:
                        eventInfo.Level = LogLevel.Info;
                        logCore.Log(eventInfo);
                        break;
                    case Constants.TRACE_EVENT_WARNING:
                        eventInfo.Level = LogLevel.Warn;
                        logCore.Log(eventInfo);
                        break;
                    default:
                        logCore.Fatal("Message type '{0}' is invalid!", message.Severity);
                        break;
                }
            }
            else logCore.Fatal("Fatal exception: cannot log null message!");
        }

        /// <summary>
        /// Kivételek naplózása.
        /// </summary>
        /// <param name="ex"></param>
        public static void LogException(Exception ex)
        {
            logCore.ErrorException(ex.Message, ex);
        }

        /// <summary>
        /// Debug üzenetek naplózása
        /// </summary>
        /// <param name="message"></param>
        public static void Debug(string message)
        {
            logCore.Debug(message);
        }

        /// <summary>
        /// Trace üzenetek naplózása
        /// </summary>
        /// <param name="message"></param>
        public static void Trace(string message)
        {
            logCore.Trace(message);
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="messageCode"></param>
        /// <returns></returns>
        private static TraceEvent GetTraceEvent(int messageCode)
        {
            return Proxy.TraceManager.GetTraceEvent(messageCode);
        }

    }
}
