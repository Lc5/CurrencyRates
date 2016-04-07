﻿namespace CurrencyRates.Tests.Web.Installers
{
    using Castle.Core;
    using Castle.MicroKernel;
    using Castle.MicroKernel.ModelBuilder;
    using Castle.Windsor;

    using CurrencyRates.Model;
    using CurrencyRates.Web.Installers;

    using NUnit.Framework;

    [TestFixture]
    public class ContextInstallerTest
    {
        private IWindsorContainer containerWithContext;

        [SetUp]
        public void SetUp()
        {
            this.containerWithContext = new WindsorContainer();
            this.containerWithContext.Kernel.ComponentModelBuilder.AddContributor(new LifestyleInspector());
            this.containerWithContext.Install(new ContextInstaller());
        }       

        [Test]
        public void TestContextIsRegistered()
        {
            this.containerWithContext.Resolve<Context>();
        }

        private class LifestyleInspector : IContributeComponentModelConstruction
        {
            public void ProcessModel(IKernel kernel, ComponentModel model)
            {
                Assert.That(model.LifestyleType, Is.EqualTo(LifestyleType.PerWebRequest));

                // LifestyleType.PerWebRequest is not available outside .NET MVC
                model.LifestyleType = LifestyleType.Undefined;
            }
        }
    }
}
