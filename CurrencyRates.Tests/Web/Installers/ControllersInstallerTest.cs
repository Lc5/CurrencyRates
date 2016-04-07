namespace CurrencyRates.Tests.Web.Installers
{
    using System;
    using System.Linq;
    using System.Web.Mvc;

    using Castle.Core;
    using Castle.Core.Internal;
    using Castle.Windsor;

    using CurrencyRates.Tests.TestUtils;
    using CurrencyRates.Web.Controllers;
    using CurrencyRates.Web.Installers;
    using CurrencyRates.Web.Plumbing;

    using NUnit.Framework;

    [TestFixture]
    public class ControllersInstallerTest
    {
        private IWindsorContainer containerWithControllers;

        [SetUp]
        public void SetUp()
        {
            this.containerWithControllers = new WindsorContainer().Install(new ControllersInstaller());
        }

        [Test]
        public void TestAllAndOnlyControllersHaveControllersSuffix()
        {
            var allControllers = GetPublicClassesFromApplicationAssembly(c => c.Name.EndsWith("Controller"));
            var registeredControllers = this.containerWithControllers.GetImplementationTypesFor(typeof(IController));

            Assert.That(allControllers, Is.EqualTo(registeredControllers));
        }

        [Test]
        public void TestAllAndOnlyControllersLiveInControllersNamespace()
        {
            var allControllers = GetPublicClassesFromApplicationAssembly(c => c.Namespace.Contains("Controllers"));
            var registeredControllers = this.containerWithControllers.GetImplementationTypesFor(typeof(IController));

            Assert.That(allControllers, Is.EqualTo(registeredControllers));
        }

        [Test]
        public void TestAllControllersAreRegistered()
        {
            var allControllers = GetPublicClassesFromApplicationAssembly(c => c.Is<IController>());
            var registeredControllers = this.containerWithControllers.GetImplementationTypesFor(typeof(IController));

            Assert.That(allControllers, Is.EqualTo(registeredControllers));
        }

        [Test]
        public void TestAllControllersAreTransient()
        {
            var nonTransientControllers = this.containerWithControllers
                .GetHandlersFor(typeof(IController))
                .Where(controller => controller.ComponentModel.LifestyleType != LifestyleType.Transient)
                .ToArray();

            Assert.That(nonTransientControllers, Is.Empty);
        }

        [Test]
        public void TestAllControllersExposeThemselvesAsService()
        {
            var controllersWithWrongName = this.containerWithControllers.GetHandlersFor(typeof(IController))
                .Where(controller => controller.ComponentModel.Services.Single() != controller.ComponentModel.Implementation)
                .ToArray();

            Assert.That(controllersWithWrongName, Is.Empty);
        }

        [Test]
        public void TestAllControllersImplementIController()
        {
            var allHandlers = this.containerWithControllers.GetAllHandlers();
            var controllerHandlers = this.containerWithControllers.GetHandlersFor(typeof(IController));

            Assert.That(allHandlers, Is.Not.Empty);
            Assert.That(allHandlers, Is.EqualTo(controllerHandlers));
        }

        [Test]
        public void TestControllerBuilderUsesWindsorControllerFactory()
        {
            Assert.That(ControllerBuilder.Current.GetControllerFactory(), Is.InstanceOf<WindsorControllerFactory>());
        }

        private static Type[] GetPublicClassesFromApplicationAssembly(Predicate<Type> where)
        {
            return
                typeof(HomeController).Assembly.GetExportedTypes()
                    .Where(t => t.IsClass)
                    .Where(t => t.IsAbstract == false)
                    .Where(where.Invoke)
                    .OrderBy(t => t.Name)
                    .ToArray();
        }
    }
}
