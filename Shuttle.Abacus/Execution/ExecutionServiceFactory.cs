using Shuttle.Abacus.DataAccess;
using Shuttle.Core.Infrastructure;

namespace Shuttle.Abacus
{
    public class ExecutionServiceFactory
    {
        private readonly IFormulaRepository _formulaRepository;
        private readonly IArgumentRepository _argumentRepository;

        public ExecutionServiceFactory(IFormulaRepository formulaRepository, IArgumentRepository argumentRepository)
        {
            Guard.AgainstNull(formulaRepository, "formulaRepository");
            Guard.AgainstNull(argumentRepository, "argumentRepository");

            _formulaRepository = formulaRepository;
            _argumentRepository = argumentRepository;
        }

        public ExecutionService Create()
        {
            return new ExecutionService(_formulaRepository.All(), _argumentRepository.All());
        }
    }
}