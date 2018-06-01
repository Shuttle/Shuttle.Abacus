using System;
using Shuttle.Core.Contract;

namespace Shuttle.Abacus
{
    public class ConstraintComparison : IConstraintComparison
    {
        private static readonly char[] Separator = {','};

        private readonly IValueTypeFactory _valueTypeFactory;

        public ConstraintComparison(IValueTypeFactory valueTypeFactory)
        {
            Guard.AgainstNull(valueTypeFactory, "valueTypeFactory");

            _valueTypeFactory = valueTypeFactory;
        }

        public bool IsSatisfiedBy(string type, string argumentValue, string comparison, string constraintValue)
        {
            var result = true;

            foreach (var argumentValueItem in argumentValue.Split(Separator, StringSplitOptions.RemoveEmptyEntries))
            {
                var argumentValueType = _valueTypeFactory.Create(type, argumentValueItem);

                foreach (var constraintValueItem in constraintValue.Split(Separator,
                    StringSplitOptions.RemoveEmptyEntries))
                {
                    var comparisonValueType = _valueTypeFactory.Create(type, constraintValueItem);

                    var comparisionResult = argumentValueType.CompareTo(comparisonValueType);

                    switch (comparison.ToLowerInvariant())
                    {
                        case "==":
                        {
                            result = comparisionResult == 0;

                            break;
                        }
                        case "!=":
                        {
                            result = comparisionResult != 0;

                            break;
                        }
                        case ">=":
                        {
                            result = comparisionResult == 0 || comparisionResult == 1;

                            break;
                        }
                        case ">":
                        {
                            result = comparisionResult == 1;

                            break;
                        }
                        case "<=":
                        {
                            result = comparisionResult == -1 || comparisionResult == 0;

                            break;
                        }
                        case "<":
                        {
                            result = comparisionResult == -1;

                            break;
                        }
                        case "in":
                        {
                            throw new NotImplementedException();
                        }
                    }

                    if (!result)
                    {
                        break;
                    }
                }

                if (!result)
                {
                    break;
                }
            }

            return result;
        }
    }
}