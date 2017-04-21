namespace Shuttle.Abacus.Domain
{
    public interface IValueTypeValidator
    {
        string Type{ get; }
        IResult Validate(string value);
    }
}
