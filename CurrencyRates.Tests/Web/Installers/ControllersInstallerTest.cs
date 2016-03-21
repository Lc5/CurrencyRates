using System;
using System.Linq;
using System.Web.Mvc;
using Castle.Core;
using Castle.MicroKernel;
using Castle.Windsor;
using CurrencyRates.Web.Controllers;
using CurrencyRates.Web.Installers;
using NUnit.Framework;
using Castle.Core.Internal;

namespace CurrencyRates.Tests.Web.Installers
{
    class ControllersInstallerTest
    {
        IWindsorContainer ContainerWithControllers;

        [SetUp]
        public void SetUp()
        {
            ContainerWithControllers = new WindsorContainer().Install(new ControllersInstaller());
        }

        [Test]
        public void TestAllControllersImplementIController()
        {
            var allHandlers = GetAllHandlers(ContainerWithControllers);
            var controllerHandlers = GetHandlersFor(typeof (IController), ContainerWithControllers);

            Assert.That(allHandlers, Is.Not.Empty);
            Assert.That(allHandlers, Is.EqualTo(controllerHandlers));
        }

        [Test]
        public void TestAllControllersAreRegistered()
        {
            var allControllers = GetPublicClassesFromApplicationAssembly(c => c.Is<IController>());
            var registeredControllers = GetImplementationTypesFor(typeof(IController), ContainerWithControllers);

            Assert.That(allControllers, Is.EqualTo(registeredControllers));
        }

        [Test]
        public void TestAllAndOnlyControllersHaveControllersSuffix()
        {
            var allControllers = GetPublicClassesFromApplicationAssembly(c => c.Name.EndsWith("Controller"));
            var registeredControllers = GetImplementationTypesFor(typeof(IController), ContainerWithControllers);

            Assert.That(allControllers, Is.EqualTo(registeredControllers));
        }

        [Test]
        public void TestAllAndOnlyControllersLiveInControllersNamespace()
        {
            var allControllers = GetPublicClassesFromApplicationAssembly(c => c.Namespace.Contains("Controllers"));
            var registeredControllers = GetImplementationTypesFor(typeof(IController), ContainerWithControllers);

            Assert.That(allControllers, Is.EqualTo(registeredControllers));
        }

        [Test]
        public void TestAllControllersAreTransient()
        {
            var nonTransientControllers = GetHandlersFor(typeof(IController), ContainerWithControllers)
                .Where(controller => controller.ComponentModel.LifestyleType != LifestyleType.Transient)
                .ToArray();

            Assert.That(nonTransientControllers, Is.Empty);
        }

        [Test]
        public void TestAllControllersExposeThemselvesAsService()
        {
            var controllersWithWrongName = GetHandlersFor(typeof(IController), ContainerWithControllers)
                .Where(controller => controller.ComponentModel.Services.Single() != controller.ComponentModel.Implementation)
                .ToArray();

            Assert.That(controllersWithWrongName, Is.Empty);
        }

        private IHandler[] GetAllHandlers(IWindsorContainer container)
        {
            return GetHandlersFor(typeof(object), container);
        }

        private IHandler[] GetHandlersFor(Type type, IWindsorContainer container)
        {
            return container.Kernel.GetAssignableHandlers(type);
        }

        private Type[] GetImplementationTypesFor(Type type, IWindsorContainer container)
        {
            return GetHandlersFor(type, container)
                .Select(h => h.ComponentModel.Implementation)
                .OrderBy(t => t.Name)
                .ToArray();
        }

        private Type[] GetPublicClassesFromApplicationAssembly(Predicate<Type> where)
        {
            return typeof(HomeController).Assembly.GetExportedTypes()
                .Where(t => t.IsClass)
                .Where(t => t.IsAbstract == false)
                .Where(where.Invoke)
                .OrderBy(t => t.Name)
                .ToArray();
        }
    }
}
