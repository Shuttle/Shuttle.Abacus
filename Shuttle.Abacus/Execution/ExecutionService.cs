using System;
using System.Collections.Generic;
using Shuttle.Abacus.Domain;
using Shuttle.Abacus.Invariants;
using Shuttle.Core.Infrastructure;

namespace Shuttle.Abacus
{
    public class ExecutionService
    {
        private readonly IConstraintComparison _constraintComparison;
        private readonly Dictionary<string, Formula> _formulas = new Dictionary<string, Formula>();
        private readonly Dictionary<string, Argument> _arguments = new Dictionary<string, Argument>();

        public ExecutionService(IConstraintComparison constraintComparison, IEnumerable<Formula> formulas, IEnumerable<Argument> arguments)
        {
            Guard.AgainstNull(constraintComparison, "comparer");
            Guard.AgainstNull(formulas, "formulas");
            Guard.AgainstNull(arguments, "arguments");

            _constraintComparison = constraintComparison;

            foreach (var formula in formulas)
            {
                if (_formulas.ContainsKey(formula.Name))
                {
                    throw new DuplicateEntryException(string.Format("There is already a formula with name '{0}' registered.", formula.Name));
                }

                _formulas.Add(formula.Name, formula);
            }

            foreach (var argument in arguments)
            {
                if (_arguments.ContainsKey(argument.Name))
                {
                    throw new DuplicateEntryException(string.Format("There is already an argument with name '{0}' registered.", argument.Name));
                }

                _arguments.Add(argument.Name, argument);
            }
        }

        // perhaps something like this
        //public ExecutionEngine AddValueSource(IValueSource valueSource)
        //{
        //}

        public ExecutionContext Execute(string formulaName, IEnumerable<ArgumentValue> values)
        {
            Guard.AgainstNullOrEmptyString(formulaName, "formulaName");
            Guard.AgainstNull(values, "values");

            var context = new ExecutionContext(_arguments.Values, values);

            try
            {
                Execute(context, formulaName);
            }
            catch (Exception ex)
            {
                context.Exception(ex);
            }

            return context;
        }

        private decimal Execute(ExecutionContext executionContext, string formulaName)
        {
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
                        //TODO: log exclusion
                        return 0;
                    }
                }

                //if (!formula.IsSatisfiedBy(executionContext))
                //{
                //    return 0;
                //}

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
                        case "formula":
                        {
                            value = Execute(executionContext, operation.ValueSelection);

                            break;
                        }
                    }

                    operation.Perform(formulaContext, value);
                }

                return formulaContext.Result;
            }
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