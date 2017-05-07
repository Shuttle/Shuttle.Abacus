namespace Shuttle.Abacus.Domain
{
    public interface ICalculationDisplay<T>
    {
        IGraphNode GraphNode { get; }

        T AssignGraphNode(IGraphNode item);

        void PopulateGraphNode(decimal total, decimal subTotal);
    }
}
