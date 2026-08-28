using Shuttle.Contract;

namespace Shuttle.Abacus;

public class ValueComparer(IDataTypeFactory dataTypeFactory) : IValueComparer
{
    private static readonly char[] Separator = [','];

    private readonly IDataTypeFactory _dataTypeFactory = Guard.AgainstNull(dataTypeFactory);

    public bool IsSatisfiedBy(string dataTypeName, string value, string comparison, string comparisonValue)
    {
        var result = true;

        foreach (var argumentValueItem in value.Split(Separator, StringSplitOptions.RemoveEmptyEntries))
        {
            var argumentDataType = _dataTypeFactory.Create(dataTypeName, argumentValueItem);

            foreach (var constraintValueItem in comparisonValue.Split(Separator, StringSplitOptions.RemoveEmptyEntries))
            {
                var comparisonDataType = _dataTypeFactory.Create(dataTypeName, constraintValueItem);

                var comparisonResult = argumentDataType.CompareTo(comparisonDataType);

                result = comparison.ToLowerInvariant() switch
                {
                    "==" => comparisonResult == 0,
                    "!=" => comparisonResult != 0,
                    ">=" => comparisonResult is 0 or 1,
                    ">" => comparisonResult == 1,
                    "<=" => comparisonResult is -1 or 0,
                    "<" => comparisonResult == -1,
                    "in" => throw new NotImplementedException(),
                    _ => result
                };

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
