using Shuttle.Abacus.Infrastructure;

namespace Shuttle.Abacus.Invariants.Values
{
    public class IntegerValueTypeValidator : IValueTypeValidator
    {
        public string Type => "Integer";

        public IResult Validate(string value)
        {
            var result = Result.Create();

            int i;

            if (!int.TryParse(value, out i))
            {
                result.AddFailureMessage("Please enter an integer value.");
            }

            return result;
        }
    }
}