using Shuttle.Abacus.Domain;

namespace Shuttle.Abacus.Invariants.Values
{
    public interface IValueTypeValidator
    {
        string Type{ get; }
        IResult Validate(string value);
    }
}
