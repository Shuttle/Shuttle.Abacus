namespace Shuttle.Abacus.Domain
{
    public interface IValueSource
    {
        string Name { get; }
        object Text { get; }

        decimal Operand(IMethodContext methodContext, ICalculationContext calculationContext);

        string Description(decimal operand, IMethodContext methodContext);

        IValueSource Copy();
    }
}
