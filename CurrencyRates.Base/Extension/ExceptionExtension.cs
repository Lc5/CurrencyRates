using System;
using System.Diagnostics;

namespace CurrencyRates.Base.Extension
{
    public static class ExceptionExtension
    {
        const string LogName = "Application";
        const string EventSource = "Application";

        public static void Log(this Exception exception)
        {
            using (var eventLog = new EventLog(LogName))
            {
                if (!EventLog.SourceExists(EventSource))
                {
                    EventLog.CreateEventSource(EventSource, LogName);
                }

                eventLog.Source = EventSource;
                eventLog.WriteEntry(exception.ToString(), EventLogEntryType.Error);
            }           
        }
    }
}
