namespace CurrencyRates.Tests.TestUtils
{
    using System;
    using System.Linq;

    using Castle.MicroKernel;
    using Castle.Windsor;

    public static class WindsorContainerExtension
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
