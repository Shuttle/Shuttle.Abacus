using Abacus.Domain;

namespace Abacus.Data
{
    public abstract class DataQuery
    {
        public IUnitOfWorkProvider UnitOfWorkProvider { get; set; }
        public IQueryProcessor QueryProcessor { get; set; }
    }
}
