using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using CurrencyRates.Model;

namespace CurrencyRates.Web.Installers
{
    public class ContextInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Component
                    .For<Context>()
                    .LifestylePerWebRequest()
            );
        }
    }
}