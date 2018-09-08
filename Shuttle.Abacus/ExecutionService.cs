using System;
using System.Collections.Generic;
using Shuttle.Core.Contract;

namespace Shuttle.Abacus
{
    public class ExecutionService : IExecutionService
    {
        private readonly object _lock = new object();
        private readonly IFormulaRepository _formulaRepository;
        private readonly IArgumentRepository _argumentRepository;
        private readonly IMatrixRepository _matrixRepository;
        private readonly Dictionary<Guid, Argument> _arguments = new Dictionary<Guid, Argument>();
        private readonly IConstraintComparison _constraintComparison;
        private readonly Dictionary<Guid, Formula> _formulas = new Dictionary<Guid, Formula>();
        private readonly Dictionary<Guid, Matrix> _matrices = new Dictionary<Guid, Matrix>();
        private bool _initialized;

        public ExecutionService(IConstraintComparison constraintComparison, IFormulaRepository formulaRepository,
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

        public IExecutionService Flush()
        {
            lock (_lock)
            {
                _formulas.Clear();
                _arguments.Clear();
                _matrices.Clear();

                _initialized = false;
            }

            return this;
        }

        public IExecutionService AddMatrix(Matrix matrix)
        {
            Guard.AgainstNull(matrix, nameof(matrix));

            lock (_lock)
            {
                if (!_matrices.ContainsKey(matrix.Id))
                {
                    _matrices.Add(matrix.Id, matrix);
                }
            }

            return this;
        }

        public IExecutionService AddArgument(Argument argument)
        {
            Guard.AgainstNull(argument, nameof(argument));

            lock (_lock)
            {
                if (!_arguments.ContainsKey(argument.Id))
                {
                    _arguments.Add(argument.Id, argument);
                }
            }

            return this;
        }

        public IExecutionService AddFormula(Formula formula)
        {
            Guard.AgainstNull(formula, nameof(formula));

            lock (_lock)
            {
                if (!_formulas.ContainsKey(formula.Id))
                {
                    _formulas.Add(formula.Id, formula);
                }
            }

            return this;
        }

        // perhaps something like this
        // e.g. FormulaValueSource would have all the formulas
        //public ExecutionEngine AddValueSource(IValueSource valueSource)
        //{
        //}

        public ExecutionContext Execute(Guid formulaId, IEnumerable<ArgumentValue> values, IContextLogger logger)
        {
            Guard.AgainstNull(values, nameof(values));
            Guard.AgainstNull(logger, nameof(logger));

            Initialize();

            var context = new ExecutionContext(values, logger);

            try
            {
                Execute(context, formulaId);
            }
            catch (Exception ex)
            {
                context.WithException(ex);
            }

            return context;
        }

        private void Initialize()
        {
            if (_initialized)
            {
                return;
            }

            lock (_lock)
            {
                foreach (var formula in _formulaRepository.All())
                {
                    AddFormula(formula);
                }

                foreach (var argument in _argumentRepository.All())
                {
                    AddArgument(argument);
                }

                foreach (var matrix in _matrixRepository.All())
                {
                    AddMatrix(matrix);
                }

                _initialized = true;
            }
        }

        private FormulaContext Execute(ExecutionContext executionContext, Guid formulaId)
        {
            var formula = GetFormula(formulaId);

            executionContext.CyclicInvariant(formula.Name);

            using (var formulaContext = executionContext.FormulaContext(formula.Name))
            {
                foreach (var constraint in formula.Constraints)
                {
                    var argument = GetArgument(constraint.ArgumentId);
                    var argumentValue = executionContext.GetArgumentValue(constraint.ArgumentId);

                    if (!_constraintComparison.IsSatisfiedBy(argument.DataType, argumentValue, constraint.Comparison,
                        constraint.Value))
                    {
                        return formulaContext.Disqualified(constraint.ArgumentId, argumentValue,
                            constraint.Comparison,
                            constraint.Value);
                    }
                }

                foreach (var operation in formula.Operations)
                {
                    decimal value = 0;

                    switch (operation.ValueProviderName.ToLower())
                    {
                        case "decimal":
                        {
                            value = Convert.ToDecimal(operation.InputParameter);

                            break;
                        }
                        case "argument":
                        {
                            value = Convert.ToDecimal(executionContext.GetArgumentValue(new Guid(operation.InputParameter)));

                            break;
                        }
                        case "matrix":
                        {
                            var matrix = GetMatrix(new Guid(operation.InputParameter));

                            value =
                                Convert.ToDecimal(matrix.GetValue(_constraintComparison, executionContext,
                                    GetArgument(matrix.RowArgumentId),
                                    matrix.ColumnArgumentId.HasValue ? GetArgument(matrix.ColumnArgumentId.Value) : null));

                            break;
                        }
                        case "formula":
                        {
                            value = Execute(executionContext, new Guid(operation.InputParameter)).Result;

                            break;
                        }
                    }

                    operation.Perform(formulaContext, value);
                }

                return formulaContext;
            }
        }

        private Matrix GetMatrix(Guid id)
        {
            lock (_lock)
            {
                if (!_matrices.ContainsKey(id))
                {
                    throw new InvalidOperationException($"There is no matrix with id '{id}'.");
                }

                return _matrices[id];
            }
        }

        private Formula GetFormula(Guid id)
        {
            lock (_lock)
            {
                if (!_formulas.ContainsKey(id))
                {
                    throw new InvalidOperationException($"There is no formula with id '{id}'.");
                }

                return _formulas[id];
            }
        }

        private Argument GetArgument(Guid id)
        {
            lock (_lock)
            {
                if (!_arguments.ContainsKey(id))
                {
                    throw new InvalidOperationException($"There is no argument with name '{id}'.");
                }

                return _arguments[id];
            }
        }
    }
}