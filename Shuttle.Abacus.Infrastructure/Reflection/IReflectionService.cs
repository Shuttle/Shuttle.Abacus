using System;

namespace Shuttle.Abacus.Infrastructure
{
    public interface IReflectionService
    {
        bool ImplementsRawGeneric(Type generic, Type type);
        Type GetGenericParameterType(Type generic, Type type);
    }
}
