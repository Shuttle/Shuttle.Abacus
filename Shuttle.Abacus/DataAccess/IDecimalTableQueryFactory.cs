using System;
using Shuttle.Abacus.Domain;
using Shuttle.Core.Data;

namespace Shuttle.Abacus.DataAccess
{
    public interface IDecimalTableQueryFactory
    {
        IQuery All();
        IQuery Get(Guid id);
        IQuery ConstrainedDecimalValues(Guid id);
        IQuery Name(Guid id);
        IQuery DecimalTableReport(Guid decimalTableId);
        IQuery Add(DecimalTable item);
        IQuery Remove(DecimalTable item);
        IQuery Save(DecimalTable item);
    }
}