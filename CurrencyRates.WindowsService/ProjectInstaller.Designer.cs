using System.ComponentModel;
using System.Configuration.Install;
using System.ServiceProcess;

namespace CurrencyRates.WindowsService
{
    partial class ProjectInstaller
    {
        IContainer components = null;
        ServiceProcessInstaller serviceProcessInstaller;
        ServiceInstaller serviceInstaller;
    
        void InitializeComponent()
        {
            serviceProcessInstaller = new ServiceProcessInstaller()
            {
                Account = ServiceAccount.LocalSystem,
                Password = null,
                Username = null
            };

            serviceInstaller = new ServiceInstaller()
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
            if (disposing && (components != null))
            {
                components.Dispose();
            }

            base.Dispose(disposing);
        }
    }
}
