using System;
using System.Collections.Generic;
using Shuttle.Abacus.Domain;
using Shuttle.Abacus.Invariants;
using Shuttle.Core.Infrastructure;

namespace Shuttle.Abacus
{
    public class ExecutionEngine
    {
        private readonly Dictionary<string, Formula> _formulas = new Dictionary<string, Formula>();

        public ExecutionEngine(IEnumerable<Formula> formulas)
        {
            Guard.AgainstNull(formulas, "formulas");

            foreach (var formula in formulas)
            {
                if (_formulas.ContainsKey(formula.Name))
                {
                    throw new DuplicateEntryException(string.Format("There is already a formula with name '{0}' registered.", formula.Name));
                }

                _formulas.Add(formula.Name, formula);
            }
        }

        // perhaps something like this
        //public ExecutionEngine AddValueSource(IValueSource valueSource)
        //{
        //}

        public ExecutionResult Execute(string formulaName, IEnumerable<ArgumentValue> values)
        {
            Guard.AgainstNullOrEmptyString(formulaName, "formulaName");
            Guard.AgainstNull(values, "values");

            var result = new ExecutionResult(values);

            Execute(formulaName, result, result.FormulaContext());

            return result;
        }

        private void Execute(string formulaName, ExecutionResult result, FormulaContext formulaContext)
        {
            var formula = GetFormula(formulaName);

            foreach (var operation in formula.Operations)
            {
                var value = string.Empty;

                switch (operation.ValueSource.ToLower())
                {
                    case "argument":
                    {
                        value = result.GetArgumentValue(operation.ValueSelection);

                        break;
                    }
                    case "formula":
                    {
                        break;
                    }
                }

                operation.Perform(formulaContext, value);
            }
        }

        private Formula GetFormula(string formulaName)
        {
            if (!_formulas.ContainsKey(formulaName))
            {
                throw new InvalidOperationException(string.Format("There is not formula with name '{0}'.", formulaName));
            }

            return _formulas[formulaName];
        }
    }
}