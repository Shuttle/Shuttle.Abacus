using System;
using System.Collections.Generic;

namespace Shuttle.Abacus
{
    public interface IDecimalValueRepository : IRepository<DecimalValue>
    {
        void Add(DecimalTable decimalTable, DecimalValue decimalValue);
        IEnumerable<DecimalValue> AllForDecimalTable(DecimalTable decimalTable);
        void RemoveAllForDecimalTable(Guid decimalTableId);
    }
}
