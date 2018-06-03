using Shuttle.Core.Contract;

namespace Shuttle.Abacus
{
    public class ExecutionServiceFactory : IExecutionServiceFactory
    {
        private readonly IArgumentRepository _argumentRepository;
        private readonly IConstraintComparison _constraintComparison;
        private readonly IFormulaRepository _formulaRepository;
        private readonly IMatrixRepository _matrixRepository;

        public ExecutionServiceFactory(IConstraintComparison constraintComparison, IFormulaRepository formulaRepository,
            IArgumentRepository argumentRepository, IMatrixRepository matrixRepository)
        {
            Guard.AgainstNull(constraintComparison, nameof(constraintComparison));
            Guard.AgainstNull(formulaRepository, nameof(formulaRepository));
            Guard.AgainstNull(argumentRepository, nameof(argumentRepository));
            Guard.AgainstNull(matrixRepository, nameof(matrixRepository));

            _constraintComparison = constraintComparison;
            _formulaRepository = formulaRepository;
            _argumentRepository = argumentRepository;
            _matrixRepository = matrixRepository;
        }

        public ExecutionService Create()
        {
            return new ExecutionService(_constraintComparison, _formulaRepository.All(), _argumentRepository.All(),
                _matrixRepository.All());
        }
    }
}