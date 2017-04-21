using System;

namespace Shuttle.Abacus.Infrastructure
{
    public class ReflectionService : IReflectionService
    {
        public bool ImplementsRawGeneric(Type generic, Type type)
        {
            foreach (var i in type.GetInterfaces())
            {
                if (i.IsGenericType && i.GetGenericTypeDefinition().IsAssignableFrom(generic))
                {
                    return true;
                }
            }

            return false;
        }

        public Type GetGenericParameterType(Type generic, Type type)
        {
            foreach (var i in type.GetInterfaces())
            {
                if (i.IsGenericType && i.GetGenericTypeDefinition().IsAssignableFrom(generic))
                {
                    return i.GetGenericArguments()[0];
                }
            }

            return null;
        }
    }
}
