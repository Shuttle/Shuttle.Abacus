using System.Collections.Generic;

namespace Shuttle.Abacus
{
    public interface IDecimalTableRepository : IRepository<DecimalTable>
    {
        IEnumerable<DecimalTable> All();
        void Save(DecimalTable decimalTable);
    }
}
