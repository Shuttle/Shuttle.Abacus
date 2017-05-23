using System;
using System.Collections.Generic;
using Shuttle.Abacus.Domain;
using Shuttle.Abacus.Invariants;
using Shuttle.Core.Infrastructure;

namespace Shuttle.Abacus
{
    public class ExecutionService
    {
        private readonly Dictionary<string, Formula> _formulas = new Dictionary<string, Formula>();

        public ExecutionService(IExecConteFac IEnumerable<Formula> formulas)
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

        public ExecutionContext Execute(string formulaName, IEnumerable<ArgumentValue> values)
        {
            Guard.AgainstNullOrEmptyString(formulaName, "formulaName");
            Guard.AgainstNull(values, "values");

            var context = new ExecutionContext(values);

            Execute(formulaName, context, context.FormulaContext());

            return context;
        }

        private decimal Execute(string formulaName, ExecutionContext executionContext, FormulaContext formulaContext)
        {
            var formula = GetFormula(formulaName);

            if (!formula.IsSatisfiedBy(executionContext))
            {
                return 0;
            }

            foreach (var operation in formula.Operations)
            {
                object value = null;

                switch (operation.ValueSource.ToLower())
                {
                    case "argument":
                    {
                        value = executionContext.GetArgumentValue(operation.ValueSelection);

                        break;
                    }
                    case "formula":
                    {
                        value = Execute(operation.ValueSelection, executionContext, executionContext.FormulaContext());

                        break;
                    }
                }

                operation.Perform(formulaContext, value);
            }

            return formulaContext.Result;
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