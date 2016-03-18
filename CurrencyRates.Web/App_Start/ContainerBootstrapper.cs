using Castle.Windsor;
using Castle.Windsor.Installer;
using System;

namespace CurrencyRates.Web
{
    public class ContainerBootstrapper : IContainerAccessor, IDisposable
    {
        private ContainerBootstrapper(IWindsorContainer container)
        {
            Container = container;
        }

        public IWindsorContainer Container { get; }

        public static ContainerBootstrapper Bootstrap()
        {
            var container = new WindsorContainer()
                .Install(FromAssembly.This());

            return new ContainerBootstrapper(container);
        }

        public void Dispose()
        {
            Container.Dispose();
        }
    }
}