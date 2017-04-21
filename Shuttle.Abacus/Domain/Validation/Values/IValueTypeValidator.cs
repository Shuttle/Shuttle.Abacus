namespace Shuttle.Abacus
{
    public interface IValueTypeValidator
    {
        string Type{ get; }
        IResult Validate(string value);
    }
}
