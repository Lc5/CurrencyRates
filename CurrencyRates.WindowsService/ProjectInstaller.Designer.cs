namespace CurrencyRates.WindowsService
{
    using System.ComponentModel;
    using System.Configuration.Install;
    using System.ServiceProcess;

    public partial class ProjectInstaller
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
            var serviceProcessInstaller = new ServiceProcessInstaller()
            {
                Account = ServiceAccount.LocalSystem,
                Password = null,
                Username = null
            };

            var serviceInstaller = new ServiceInstaller()
            {
                Description = "CurrencyRates Scheduler",
                DisplayName = "CurrencyRates Scheduler",
                ServiceName = "CurrencyRates Scheduler",
                StartType = ServiceStartMode.Automatic
            };
    
            Installers.AddRange(new Installer[] { serviceProcessInstaller, serviceInstaller });
        }    
    }
}
