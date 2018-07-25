using System;
using System.Collections.Generic;
using Shuttle.Core.Contract;

namespace Shuttle.Abacus
{
    public class ExecutionService
    {
        private readonly Dictionary<Guid, Argument> _arguments = new Dictionary<Guid, Argument>();
        private readonly IConstraintComparison _constraintComparison;
        private readonly Dictionary<string, Formula> _formulas = new Dictionary<string, Formula>();
        private readonly Dictionary<string, Matrix> _matrixes = new Dictionary<string, Matrix>();

        public ExecutionService(IConstraintComparison constraintComparison, IEnumerable<Formula> formulas,
            IEnumerable<Argument> arguments, IEnumerable<Matrix> matrixes)
        {
            Guard.AgainstNull(constraintComparison, nameof(constraintComparison));
            Guard.AgainstNull(formulas, nameof(formulas));
            Guard.AgainstNull(arguments, nameof(arguments));

            _constraintComparison = constraintComparison;

            foreach (var formula in formulas)
            {
                if (_formulas.ContainsKey(formula.Name))
                {
                    throw new ArgumentException(
                        $"There is already a formula with name '{formula.Name}' registered.");
                }

                _formulas.Add(formula.Name, formula);
            }

            foreach (var argument in arguments)
            {
                if (_arguments.ContainsKey(argument.Id))
                {
                    throw new ArgumentException(
                        $"There is already an argument with name '{argument.Name}' registered.");
                }

                _arguments.Add(argument.Id, argument);
            }

            foreach (var matrix in matrixes)
            {
                if (_matrixes.ContainsKey(matrix.Name))
                {
                    throw new ArgumentException(
                        $"There is already an matrix with name '{matrix.Name}' registered.");
                }

                _matrixes.Add(matrix.Name, matrix);
            }
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
            if (!_matrixes.ContainsKey(matrixName))
            {
                throw new InvalidOperationException($"There is no matrix with name '{matrixName}'.");
            }

            return _matrixes[matrixName];
        }

        private Formula GetFormula(string formulaName)
        {
            if (!_formulas.ContainsKey(formulaName))
            {
                throw new InvalidOperationException($"There is no formula with name '{formulaName}'.");
            }

            return _formulas[formulaName];
        }

        private Argument GetArgument(Guid id)
        {
            if (!_arguments.ContainsKey(id))
            {
                throw new InvalidOperationException($"There is no argument with name '{id}'.");
            }

            return _arguments[id];
        }
    }
}