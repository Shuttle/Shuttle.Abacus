using System;
using Shuttle.Abacus.Domain;
using Shuttle.Core.Data;

namespace Shuttle.Abacus.DataAccess
{
    public interface IMatrixQueryFactory
    {
        IQuery All();
        IQuery Get(Guid id);
        IQuery ConstrainedDecimalValues(Guid id);
        IQuery Name(Guid id);
        IQuery Report(Guid decimalTableId);
        IQuery Add(Matrix item);
        IQuery Remove(Guid id);
        IQuery Save(Matrix item);
    }
}