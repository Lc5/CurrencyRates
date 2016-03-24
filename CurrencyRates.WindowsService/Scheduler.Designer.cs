using System.ComponentModel;
using System.Diagnostics;

namespace CurrencyRates.WindowsService
{
    partial class Scheduler
    {
        IContainer components = null;
        EventLog eventLog;

        void InitializeComponent()
        {
            eventLog = new EventLog();
            ((ISupportInitialize)(this.eventLog)).BeginInit();
            eventLog.Log = "CurrencyRates";
            eventLog.Source = "CurrencyRates";
            ServiceName = "Scheduler";
            ((ISupportInitialize)(this.eventLog)).EndInit();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
