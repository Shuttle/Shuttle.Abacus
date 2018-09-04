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
        private readonly Dictionary<string, Formula> _formulas = new Dictionary<string, Formula>();
        private readonly Dictionary<string, Matrix> _matrices = new Dictionary<string, Matrix>();

        public ExecutionService(IConstraintComparison constraintComparison)
        {
            Guard.AgainstNull(constraintComparison, nameof(constraintComparison));

            _constraintComparison = constraintComparison;
        }

        public ExecutionService(IConstraintComparison constraintComparison, IFormulaRepository formulaRepository,
            IArgumentRepository argumentRepository, IMatrixRepository matrixRepository)
            :this(constraintComparison)
        {
            Guard.AgainstNull(formulaRepository, nameof(formulaRepository));
            Guard.AgainstNull(argumentRepository, nameof(argumentRepository));
            Guard.AgainstNull(matrixRepository, nameof(matrixRepository));

            _formulaRepository = formulaRepository;
            _argumentRepository = argumentRepository;
            _matrixRepository = matrixRepository;

            Flush();
        }

        public IExecutionService Flush()
        {
            lock (_lock)
            {
                _formulas.Clear();

                foreach (var formula in _formulaRepository.All())
                {
                    AddFormula(formula);
                }

                _arguments.Clear();

                foreach (var argument in _argumentRepository.All())
                {
                    AddArgument(argument);
                }

                _matrices.Clear();

                foreach (var matrix in _matrixRepository.All())
                {
                    AddMatrix(matrix);
                }
            }

            return this;
        }

        public IExecutionService AddMatrix(Matrix matrix)
        {
            lock (_lock)
            {
                if (_matrices.ContainsKey(matrix.Name))
                {
                    throw new ArgumentException(
                        $"There is already an matrix with name '{matrix.Name}' registered.");
                }

                _matrices.Add(matrix.Name, matrix);
            }

            return this;
        }

        public IExecutionService AddArgument(Argument argument)
        {
            lock (_lock)
            {
                if (_arguments.ContainsKey(argument.Id))
                {
                    throw new ArgumentException(
                        $"There is already an argument with name '{argument.Name}' registered.");
                }

                _arguments.Add(argument.Id, argument);
            }

            return this;
        }

        public IExecutionService AddFormula(Formula formula)
        {
            lock (_lock)
            {
                if (_formulas.ContainsKey(formula.Name))
                {
                    throw new ArgumentException(
                        $"There is already a formula with name '{formula.Name}' registered.");
                }

                _formulas.Add(formula.Name, formula);
            }

            return this;
        }

        // perhaps something like this
        // e.g. FormulaValueSource would have all the formulas
        //public ExecutionEngine AddValueSource(IValueSource valueSource)
        //{
        //}

        public ExecutionContext Execute(string formulaName, IEnumerable<ArgumentValue> values, IContextLogger logger)
        {
            Guard.AgainstNullOrEmptyString(formulaName, nameof(formulaName));
            Guard.AgainstNull(values, nameof(values));
            Guard.AgainstNull(logger, nameof(logger));

            var context = new ExecutionContext(values, logger);

            try
            {
                Execute(context, formulaName);
            }
            catch (Exception ex)
            {
                context.WithException(ex);
            }

            return context;
        }

        private FormulaContext Execute(ExecutionContext executionContext, string formulaName)
        {
            executionContext.CyclicInvariant(formulaName);

            using (var formulaContext = executionContext.FormulaContext(formulaName))
            {
                var formula = GetFormula(formulaName);

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
                            var matrix = GetMatrix(operation.InputParameter);

                            value =
                                Convert.ToDecimal(matrix.GetValue(_constraintComparison, executionContext,
                                    GetArgument(matrix.RowArgumentId),
                                    matrix.ColumnArgumentId.HasValue ? GetArgument(matrix.ColumnArgumentId.Value) : null));

                            break;
                        }
                        case "formula":
                        {
                            value = Execute(executionContext, operation.InputParameter).Result;

                            break;
                        }
                    }

                    operation.Perform(formulaContext, value);
                }

                return formulaContext;
            }
        }

        private Matrix GetMatrix(string matrixName)
        {
            lock (_lock)
            {
                if (!_matrices.ContainsKey(matrixName))
                {
                    throw new InvalidOperationException($"There is no matrix with name '{matrixName}'.");
                }

                return _matrices[matrixName];
            }
        }

        private Formula GetFormula(string formulaName)
        {
            lock (_lock)
            {
                if (!_formulas.ContainsKey(formulaName))
                {
                    throw new InvalidOperationException($"There is no formula with name '{formulaName}'.");
                }

                return _formulas[formulaName];
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