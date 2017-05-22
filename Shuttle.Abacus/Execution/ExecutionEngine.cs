using System.Collections.Generic;
using Shuttle.Abacus.Domain;
using Shuttle.Abacus.Invariants;
using Shuttle.Core.Infrastructure;

namespace Shuttle.Abacus
{
    public class ExecutionEngine
    {
        private readonly Dictionary<string, Formula> _formulas = new Dictionary<string, Formula>();
        private readonly Dictionary<string, Argument> _arguments = new Dictionary<string, Argument>();
        private readonly Dictionary<string, Matrix> _matrixes = new Dictionary<string, Matrix>();

        public ExecutionEngine(IEnumerable<Formula> formulas, IEnumerable<Argument> arguments, IEnumerable<Matrix> matrixes)
        {
            Guard.AgainstNull(formulas, "formulas");
            Guard.AgainstNull(arguments, "arguments");
            Guard.AgainstNull(matrixes, "matrixes");

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

            foreach (var matrix in matrixes)
            {
                if (_matrixes.ContainsKey(matrix.Name))
                {
                    throw new DuplicateEntryException(string.Format("There is already a matrix with name '{0}' registered.", matrix.Name));
                }

                _matrixes.Add(matrix.Name, matrix);
            }
        }

        // rather something like this
        //public ExecutionEngine AddValueSource(IValueSource valueSource)
        //{
        //}

        public ExecutionResult Execute(string formulaName, IEnumerable<ArgumentValue> values)
        {
            Guard.AgainstNullOrEmptyString(formulaName, "formulaName");
            Guard.AgainstNull(values, "values");

            var result = new ExecutionResult();

            var formula = _formulas[formulaName];

            foreach (var operation in formula.Operations)
            {
                operation.
            }

            return result;
        }
    }
}