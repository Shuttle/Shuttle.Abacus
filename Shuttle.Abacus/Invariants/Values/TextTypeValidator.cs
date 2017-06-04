using Shuttle.Abacus.Infrastructure;

namespace Shuttle.Abacus.Invariants.Values
{
    public class TextTypeValidator : IValueTypeValidator
    {
        public string Type => "Text";

        public IResult Validate(string value)
        {
            return Result.Create();
        }
    }
}