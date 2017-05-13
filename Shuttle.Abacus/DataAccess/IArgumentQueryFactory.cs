using System;
using Shuttle.Abacus.Domain;
using Shuttle.Abacus.Events.Argument.v1;
using Shuttle.Core.Data;
using Shuttle.Recall;

namespace Shuttle.Abacus.DataAccess
{
    public interface IArgumentQueryFactory
    {
        IQuery All();
        IQuery Get(Guid id);
        IQuery GetValues(Guid id);
        IQuery Add(Argument item);
        IQuery Remove(Guid id);
        IQuery GetDTO(Guid id);
        IQuery Save(Argument item);
        IQuery RemoveValues(Argument argument);
        IQuery SaveValue(Argument argument, string value);
        IQuery Get(string name);
        IQuery Registered(PrimitiveEvent primitiveEvent, Registered registered);
        IQuery Removed(PrimitiveEvent primitiveEvent, Removed removed);
        IQuery Renamed(PrimitiveEvent primitiveEvent, Renamed renamed);
        IQuery ValueAdded(PrimitiveEvent primitiveEvent, ValueAdded valueAdded);
        IQuery ValueRemoved(PrimitiveEvent primitiveEvent, ValueRemoved valueRemoved);
    }
}