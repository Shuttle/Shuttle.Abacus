namespace Shuttle.Abacus.Domain
{
    public interface IOperationFactory 
    {
        Operation Create(string name, IValueSource valueSource);
    }
}
