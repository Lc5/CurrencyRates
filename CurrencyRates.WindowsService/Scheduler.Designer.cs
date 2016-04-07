namespace CurrencyRates.WindowsService
{
    using System.ComponentModel;

    public partial class Scheduler
    {
        private IContainer components = null;
     
        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }

            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            ServiceName = "CurrencyRates Scheduler";
        }
    }
}
