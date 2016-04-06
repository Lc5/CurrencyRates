namespace CurrencyRates.Web
{
    using System.Web;
    using System.Web.Mvc;
    using System.Web.Optimization;
    using System.Web.Routing;

    using Castle.Windsor;
    using Castle.Windsor.Installer;

    public class MvcApplication : HttpApplication
    {
        private static IWindsorContainer container;

        protected void Application_End()
        {
            container.Dispose();
        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            BootstrapContainer();
        }

        private static void BootstrapContainer()
        {
            container = new WindsorContainer().Install(FromAssembly.This());
        }
    }
}
