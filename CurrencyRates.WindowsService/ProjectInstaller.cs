using System.ComponentModel;
using System.Configuration.Install;

namespace CurrencyRates.WindowsService
{
    [RunInstaller(true)]
    public partial class ProjectInstaller : Installer
    {
        public ProjectInstaller()
        {
            InitializeComponent();
        }
    }
}
