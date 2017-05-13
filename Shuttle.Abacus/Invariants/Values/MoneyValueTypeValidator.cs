using Shuttle.Abacus.Domain;
using Shuttle.Abacus.Infrastructure;

namespace Shuttle.Abacus.Invariants.Values
{
    public class MoneyValueTypeValidator : IValueTypeValidator
    {
        public static string TypeName = "Money";

        public string Type => TypeName;

        public IResult Validate(string value)
        {
            var result = Result.Create();

            decimal d;

            if (!decimal.TryParse(value, out d))
            {
                result.AddFailureMessage("Please enter a decimal value.");
            }

            return result;
        }
    }
}
