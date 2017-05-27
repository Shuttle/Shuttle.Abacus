using Shuttle.Abacus.DataAccess;
using Shuttle.Abacus.Domain;
using Shuttle.Core.Infrastructure;

namespace Shuttle.Abacus
{
    public class ExecutionServiceFactory : IExecutionServiceFactory
    {
        private readonly IConstraintComparison _constraintComparison;
        private readonly IFormulaRepository _formulaRepository;
        private readonly IArgumentRepository _argumentRepository;

        public ExecutionServiceFactory(IConstraintComparison constraintComparison, IFormulaRepository formulaRepository, IArgumentRepository argumentRepository)
        {
            Guard.AgainstNull(constraintComparison, "constraintComparison");
            Guard.AgainstNull(formulaRepository, "formulaRepository");
            Guard.AgainstNull(argumentRepository, "argumentRepository");

            _constraintComparison = constraintComparison;
            _formulaRepository = formulaRepository;
            _argumentRepository = argumentRepository;
        }

        public ExecutionService Create()
        {
            return new ExecutionService(_constraintComparison, _formulaRepository.All(), _argumentRepository.All());
        }
    }
}