namespace Shuttle.Abacus
{
    public interface IValueTypeFactory
    {
        ValueType Create(string type, string value);
    }
}