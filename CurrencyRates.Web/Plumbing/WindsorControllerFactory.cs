namespace CurrencyRates.Web.Plumbing
{
    using System;
    using System.Web.Mvc;
    using System.Web.Routing;

    using Castle.Windsor;

    public class WindsorControllerFactory : DefaultControllerFactory
    {
        private readonly IWindsorContainer container;

        public WindsorControllerFactory(IWindsorContainer container)
        {
            this.container = container;
        }

        public override void ReleaseController(IController controller)
        {
            this.container.Release(controller);
        }

        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {
            if (controllerType != null && this.container.Kernel.HasComponent(controllerType))
            {
                return (IController)this.container.Resolve(controllerType);
            }

            return base.GetControllerInstance(requestContext, controllerType);
        }
    }
}
