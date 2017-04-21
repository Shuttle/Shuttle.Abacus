using System;
using Shuttle.Abacus.Localisation;

namespace Shuttle.Abacus.Infrastructure
{
    public class DependencyResolver
    {
        public static IDependencyResolver Resolver { get; private set; }

        public static void InitializeWith(IDependencyResolver resolver)
        {
            Resolver = resolver;
        }

        public static TInterface Resolve<TInterface>()
        {
            try
            {
                return Resolver.Resolve<TInterface>();
            }
            catch (Exception e)
            {
                throw new InterfaceResolutionException(e, typeof (TInterface));
            }
        }

        public static TInterface Resolve<TInterface>(string name)
        {
            try
            {
                return Resolver.Resolve<TInterface>(name);
            }
            catch (InvalidCastException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new InterfaceResolutionException(ex, typeof (TInterface));
            }
        }

        public class InterfaceResolutionException : Exception
        {
            public InterfaceResolutionException(Exception innerException,
                                                Type interfaceThatCouldNotBeResolvedForSomeReason)
                : base(
                    string.Format(Resources.InterfaceResolutionException,
                                  interfaceThatCouldNotBeResolvedForSomeReason.FullName), innerException)
            {
            }
        }
    }
}
