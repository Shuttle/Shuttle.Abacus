namespace Shuttle.Abacus.Domain
{
    public interface IValueTypeFactory
    {
        ValueType Create(string type, string value);
    }
}