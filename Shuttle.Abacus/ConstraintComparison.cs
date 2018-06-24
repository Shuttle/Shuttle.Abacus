using System;
using Shuttle.Core.Contract;

namespace Shuttle.Abacus
{
    public class ConstraintComparison : IConstraintComparison
    {
        private static readonly char[] Separator = {','};

        private readonly IDataTypeFactory _dataTypeFactory;

        public ConstraintComparison(IDataTypeFactory dataTypeFactory)
        {
            Guard.AgainstNull(dataTypeFactory, nameof(dataTypeFactory));

            _dataTypeFactory = dataTypeFactory;
        }

        public bool IsSatisfiedBy(string dataTypeName, string argumentValue, string comparison, string constraintValue)
        {
            var result = true;

            foreach (var argumentValueItem in argumentValue.Split(Separator, StringSplitOptions.RemoveEmptyEntries))
            {
                var argumentDataType = _dataTypeFactory.Create(dataTypeName, argumentValueItem);

                foreach (var constraintValueItem in constraintValue.Split(Separator, StringSplitOptions.RemoveEmptyEntries))
                {
                    var comparisonDataType = _dataTypeFactory.Create(dataTypeName, constraintValueItem);

                    var comparisionResult = argumentDataType.CompareTo(comparisonDataType);

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