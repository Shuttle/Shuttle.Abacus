namespace Shuttle.Abacus
{
    public interface IValueTypeValidatorProvider
    {
        IValueTypeValidator Get(string type);
        bool Has(string type);
    }
}
