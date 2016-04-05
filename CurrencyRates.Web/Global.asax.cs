using Castle.Windsor;
using Castle.Windsor.Installer;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace CurrencyRates.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        static IWindsorContainer Container;

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            BootstrapContainer();
        }

        protected void Application_End()
        {
            Container.Dispose();
        }

        static void BootstrapContainer()
        {
            Container = new WindsorContainer()
                .Install(FromAssembly.This());
        }
    }
}
