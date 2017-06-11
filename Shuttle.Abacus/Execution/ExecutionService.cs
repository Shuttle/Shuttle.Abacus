using System;
using System.Collections.Generic;
using Shuttle.Abacus.Domain;
using Shuttle.Core.Infrastructure;

namespace Shuttle.Abacus
{
    public class ExecutionService
    {
        private readonly Dictionary<string, Argument> _arguments = new Dictionary<string, Argument>();
        private readonly IConstraintComparison _constraintComparison;
        private readonly Dictionary<string, Formula> _formulas = new Dictionary<string, Formula>();
        private readonly Dictionary<string, Matrix> _matrixes = new Dictionary<string, Matrix>();

        public ExecutionService(IConstraintComparison constraintComparison, IEnumerable<Formula> formulas,
            IEnumerable<Argument> arguments, IEnumerable<Matrix> matrixes)
        {
            Guard.AgainstNull(constraintComparison, "comparer");
            Guard.AgainstNull(formulas, "formulas");
            Guard.AgainstNull(arguments, "arguments");

            _constraintComparison = constraintComparison;

            foreach (var formula in formulas)
            {
                if (_formulas.ContainsKey(formula.Name))
                {
                    throw new DuplicateEntryException(
                        string.Format("There is already a formula with name '{0}' registered.", formula.Name));
                }

                _formulas.Add(formula.Name, formula);
            }

            foreach (var argument in arguments)
            {
                if (_arguments.ContainsKey(argument.Name))
                {
                    throw new DuplicateEntryException(
                        string.Format("There is already an argument with name '{0}' registered.", argument.Name));
                }

                _arguments.Add(argument.Name, argument);
            }

            foreach (var matrix in matrixes)
            {
                if (_matrixes.ContainsKey(matrix.Name))
                {
                    throw new DuplicateEntryException(
                        string.Format("There is already an matrix with name '{0}' registered.", matrix.Name));
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
            Guard.AgainstNullOrEmptyString(formulaName, "formulaName");
            Guard.AgainstNull(values, "values");
            Guard.AgainstNull(logger, "logger");

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
                    var argument = GetArgument(constraint.ArgumentName);
                    var argumentValue = executionContext.GetArgumentValue(constraint.ArgumentName);

                    if (!_constraintComparison.IsSatisfiedBy(argument.ValueType, argumentValue, constraint.Comparison,
                        constraint.Value))
                    {
                        return formulaContext.Disqualified(constraint.ArgumentName, argumentValue, constraint.Comparison,
                            constraint.Value);
                    }
                }

                foreach (var operation in formula.Operations)
                {
                    decimal value = 0;

                    switch (operation.ValueSource.ToLower())
                    {
                        case "constant":
                        {
                            value = Convert.ToDecimal(operation.ValueSelection);

                            break;
                        }
                        case "argument":
                        {
                            value = Convert.ToDecimal(executionContext.GetArgumentValue(operation.ValueSelection));

                            break;
                        }
                        case "matrix":
                        {
                            var matrix = GetMatrix(operation.ValueSelection);

                            value =
                                Convert.ToDecimal(matrix.GetValue(_constraintComparison, executionContext,
                                    GetArgument(matrix.RowArgumentName),
                                    matrix.HasColumnArgument ? GetArgument(matrix.ColumnArgumentName) : null));

                            break;
                        }
                        case "formula":
                        {
                            value = Execute(executionContext, operation.ValueSelection).Result;

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
                throw new InvalidOperationException(string.Format("There is no matrix with name '{0}'.", matrixName));
            }

            return _matrixes[matrixName];
        }

        private Formula GetFormula(string formulaName)
        {
            if (!_formulas.ContainsKey(formulaName))
            {
                throw new InvalidOperationException(string.Format("There is no formula with name '{0}'.", formulaName));
            }

            return _formulas[formulaName];
        }

        private Argument GetArgument(string name)
        {
            if (!_arguments.ContainsKey(name))
            {
                throw new InvalidOperationException(string.Format("There is no argument with name '{0}'.", name));
            }

            return _arguments[name];
        }
    }
}