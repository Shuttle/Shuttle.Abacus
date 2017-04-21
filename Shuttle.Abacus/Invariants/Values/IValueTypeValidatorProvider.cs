namespace Shuttle.Abacus.Invariants.Values
{
    public interface IValueTypeValidatorProvider
    {
        IValueTypeValidator Get(string type);
        bool Has(string type);
    }
}
