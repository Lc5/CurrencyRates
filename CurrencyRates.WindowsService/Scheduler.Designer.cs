using System.ComponentModel;

namespace CurrencyRates.WindowsService
{
    partial class Scheduler
    {
        IContainer Components = null;

        void InitializeComponent()
        {
            ServiceName = "CurrencyRates Scheduler";
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (Components != null))
            {
                Components.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
