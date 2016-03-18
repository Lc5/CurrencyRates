using Castle.Windsor;
using System;
using System.Web.Mvc;
using System.Web.Routing;

namespace CurrencyRates.Web.Plumbing
{
    public class WindsorControllerFactory : DefaultControllerFactory
    {
        private readonly IWindsorContainer Container;

        public WindsorControllerFactory(IWindsorContainer container)
        {
            Container = container;
        }

        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {
            if (controllerType != null && Container.Kernel.HasComponent(controllerType))
            {
                return (IController) Container.Resolve(controllerType);
            }

            return base.GetControllerInstance(requestContext, controllerType);
        }

        public override void ReleaseController(IController controller)
        {
            Container.Release(controller);
        }
    }
}