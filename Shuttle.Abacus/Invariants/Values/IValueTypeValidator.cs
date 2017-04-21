using Abacus.Infrastructure;

namespace Abacus.Validation
{
    public interface IValueTypeValidator
    {
        string Type{ get; }
        IResult Validate(string value);
    }
}
