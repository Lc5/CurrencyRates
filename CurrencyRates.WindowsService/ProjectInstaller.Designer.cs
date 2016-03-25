using System.ComponentModel;
using System.Configuration.Install;
using System.ServiceProcess;

namespace CurrencyRates.WindowsService
{
    partial class ProjectInstaller
    {
        IContainer Components = null;
    
        void InitializeComponent()
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
    
            Installers.AddRange(new Installer[] {serviceProcessInstaller, serviceInstaller});
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
