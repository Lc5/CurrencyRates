namespace CurrencyRates.Base
{
    using System;

    using Castle.Windsor;
    using Castle.Windsor.Installer;

    public class ContainerBootstrapper : IContainerAccessor, IDisposable
    {
        private ContainerBootstrapper(IWindsorContainer container)
        {
            this.Container = container;
        }

        public IWindsorContainer Container { get; }

        public static ContainerBootstrapper Bootstrap()
        {
            var container = new WindsorContainer().Install(FromAssembly.This());

            return new ContainerBootstrapper(container);
        }

        public void Dispose()
        {
            this.Container.Dispose();
        }
    }
}
