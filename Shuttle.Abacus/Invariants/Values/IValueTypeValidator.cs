using Shuttle.Abacus.Infrastructure;

namespace Shuttle.Abacus.Invariants.Values
{
    public interface IValueTypeValidator
    {
        string Type { get; }
        IResult Validate(string value);
    }
}