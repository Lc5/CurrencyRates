namespace CurrencyRates.WindowsService
{
    using System.Diagnostics;
    using System.ServiceProcess;
    using System.Timers;

    using CurrencyRates.Base.Services;

    public partial class Scheduler : ServiceBase
    {
        private static int eventId;

        private readonly int interval;

        private readonly Synchronizer synchronizer;

        public Scheduler(Synchronizer synchronizer, int interval = 60000)
        {
            this.synchronizer = synchronizer;
            this.interval = interval;
            this.InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            this.EventLog.WriteEntry(this.GetType().FullName + " started.", EventLogEntryType.Information, ++eventId);

            var timer = new Timer { Interval = this.interval };
            timer.Elapsed += this.OnTimer;
            timer.Start();
        }

        protected override void OnStop()
        {
            this.EventLog.WriteEntry(this.GetType().FullName + " stopped.", EventLogEntryType.Information, ++eventId);
        }

        private void OnTimer(object sender, ElapsedEventArgs args)
        {
            this.synchronizer.SyncAll();
            this.EventLog.WriteEntry("Synchronized rates.", EventLogEntryType.Information, ++eventId);
        }
    }
}
