using Abacus.Domain;
using Abacus.Infrastructure;

namespace Abacus.Data
{
    public abstract class AbstractMapper
    {
        private readonly IUnitOfWorkProvider unitOfWorkProvider;

        protected AbstractMapper()
        {
            unitOfWorkProvider = DependencyResolver.Resolve<IUnitOfWorkProvider>();
        }

        protected IUnitOfWork UnitOfWork
        {
            get { return unitOfWorkProvider.Current; }
        }

    }
}
