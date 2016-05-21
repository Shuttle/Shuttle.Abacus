namespace Shuttle.Abacus
{
    public interface IOperationFactory : IFactory
    {
        FormulaOperation Create(IValueSource valueSource);
        string Text { get; }
    }
}
