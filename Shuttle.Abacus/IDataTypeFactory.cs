namespace Shuttle.Abacus
{
    public interface IDataTypeFactory
    {
        DataType Create(string name, string value);
    }
}