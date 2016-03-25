using CurrencyRates.Base.Service;
using System.Diagnostics;
using System.ServiceProcess;
using System.Timers;

namespace CurrencyRates.WindowsService
{
    public partial class Scheduler : ServiceBase
    {
        static int EventId;
        readonly int Interval;
        readonly Synchronizer Synchronizer;

        public Scheduler(Synchronizer synchronizer, int interval = 60000)
        {
            Synchronizer = synchronizer;
            Interval = interval;
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            EventLog.WriteEntry(GetType().FullName + " started.", EventLogEntryType.Information, ++EventId);

            var timer = new Timer() { Interval = Interval };
            timer.Elapsed += OnTimer;
            timer.Start();
        }

        protected override void OnStop()
        {
            EventLog.WriteEntry(GetType().FullName + " stopped.", EventLogEntryType.Information, ++EventId);
        }

        void OnTimer(object sender, ElapsedEventArgs args)
        {
            Synchronizer.SyncAll();
            EventLog.WriteEntry("Synchronized rates.", EventLogEntryType.Information, ++EventId);
        }
    }
}
