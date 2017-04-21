namespace Shuttle.Abacus.Domain
{
    public interface IValueTypeValidatorProvider
    {
        IValueTypeValidator Get(string type);
        bool Has(string type);
    }
}
