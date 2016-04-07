namespace CurrencyRates.Base.Extensions
{
    using System;
    using System.Diagnostics;

    public static class ExceptionExtension
    {
        private const string EventSource = "Application";

        private const string LogName = "Application";

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
