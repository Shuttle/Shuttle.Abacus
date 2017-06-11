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
        private readonly IMatrixRepository _matrixRepository;

        public ExecutionServiceFactory(IConstraintComparison constraintComparison, IFormulaRepository formulaRepository, IArgumentRepository argumentRepository, IMatrixRepository matrixRepository)
        {
            Guard.AgainstNull(constraintComparison, "constraintComparison");
            Guard.AgainstNull(formulaRepository, "formulaRepository");
            Guard.AgainstNull(argumentRepository, "argumentRepository");
            Guard.AgainstNull(matrixRepository, "matrixRepository");

            _constraintComparison = constraintComparison;
            _formulaRepository = formulaRepository;
            _argumentRepository = argumentRepository;
            _matrixRepository = matrixRepository;
        }

        public ExecutionService Create()
        {
            return new ExecutionService(_constraintComparison, _formulaRepository.All(), _argumentRepository.All(), _matrixRepository.All());
        }
    }
}