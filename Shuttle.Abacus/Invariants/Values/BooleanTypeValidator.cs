using Shuttle.Abacus.Infrastructure;

namespace Shuttle.Abacus.Invariants.Values
{
    public class BooleanTypeValidator : IValueTypeValidator
    {
        public string Type => "Boolean";

        public IResult Validate(string value)
        {
            var result = Result.Create();

            bool b;

            if (!bool.TryParse(value, out b))
            {
                result.AddFailureMessage("Please enter a boolean value (true / false).");
            }

            return result;
        }
    }
}