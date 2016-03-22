using Castle.MicroKernel;
using Castle.Windsor;
using System;
using System.Linq;

namespace CurrencyRates.Tests.TestUtils
{
    internal static class WindsorContainerExtension
    {
        public static IHandler[] GetAllHandlers(this IWindsorContainer container)
        {
            return GetHandlersFor(container, typeof(object));
        }

        public static IHandler[] GetHandlersFor(this IWindsorContainer container, Type type)
        {
            return container.Kernel.GetAssignableHandlers(type);
        }

        public static Type[] GetImplementationTypesFor(this IWindsorContainer container, Type type)
        {
            return GetHandlersFor(container, type)
                .Select(h => h.ComponentModel.Implementation)
                .OrderBy(t => t.Name)
                .ToArray();
        }       
    }
}
