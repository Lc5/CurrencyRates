using Castle.Core;
using Castle.MicroKernel;
using Castle.MicroKernel.ModelBuilder;
using Castle.Windsor;
using CurrencyRates.Model;
using CurrencyRates.Web.Installers;
using NUnit.Framework;

namespace CurrencyRates.Tests.Web.Installers
{
    class ContextInstallerTest
    {
        IWindsorContainer ContainerWithContext;

        class LifestyleInspector : IContributeComponentModelConstruction
        {
            public void ProcessModel(IKernel kernel, ComponentModel model)
            {
                Assert.That(model.LifestyleType, Is.EqualTo(LifestyleType.PerWebRequest));

                //LifestyleType.PerWebRequest is not available outside .NET MVC
                model.LifestyleType = LifestyleType.Undefined;
            }
        }
     
        [SetUp]
        public void SetUp()
        {
            ContainerWithContext = new WindsorContainer();
            ContainerWithContext.Kernel.ComponentModelBuilder.AddContributor(new LifestyleInspector());
            ContainerWithContext.Install(new ContextInstaller());    
        }

        [Test]
        public void TestContextIsRegistered()
        {
            ContainerWithContext.Resolve<Context>();
        }  
    }
}
