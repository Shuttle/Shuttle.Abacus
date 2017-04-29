using System;
using Shuttle.Abacus.Domain;
using Shuttle.Core.Data;

namespace Shuttle.Abacus.DataAccess
{
    public interface ICalculationQueryFactory
    {
        IQuery AllForOwner(Guid ownerId);
        IQuery AllForMethod(Guid methodId);
        IQuery AllBeforeCalculation(Guid methodId, Guid calculationId);
        IQuery Name(Guid id);
        IQuery AllForMethod(Guid methodId, Guid grabberCalculationId);
        IQuery GraphNodeArguments(Guid calculationId);
        IQuery Add(Method method, ICalculationOwner owner, Calculation item);
        IQuery Remove(Guid item);
        IQuery Get(Guid id);
        IQuery Save(Calculation item);
        IQuery SetSequenceNumber(Guid id, int sequenceNumber);
        IQuery Save(Calculation calculation, string ownerName, Guid ownerId, int sequence);
    }
}