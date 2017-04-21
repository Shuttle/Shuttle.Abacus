namespace Abacus.Validation
{
    public interface IValueTypeValidatorProvider
    {
        IValueTypeValidator Get(string type);
        bool Has(string type);
    }
}
