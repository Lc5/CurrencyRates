﻿using CurrencyRates.Base.Service;
using System.Diagnostics;
using System.ServiceProcess;
using System.Timers;

namespace CurrencyRates.WindowsService
{
    public partial class Scheduler : ServiceBase
    {
        static int EventId;
        readonly Synchronizer Synchronizer;

        public Scheduler(Synchronizer synchronizer)
        {
            Synchronizer = synchronizer;
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            EventLog.WriteEntry(GetType().Name + " started.", EventLogEntryType.Information, ++EventId);

            var timer = new Timer() { Interval = 60000 };
            timer.Elapsed += OnTimer;
            timer.Start();
        }

        protected override void OnStop()
        {
            EventLog.WriteEntry(GetType().Name + " stopped.", EventLogEntryType.Information, ++EventId);
        }

        void OnTimer(object sender, ElapsedEventArgs args)
        {
            Synchronizer.SyncAll();
            EventLog.WriteEntry("Synchronized rates.", EventLogEntryType.Information, ++EventId);
        }
    }
}
