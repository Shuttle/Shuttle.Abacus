namespace Shuttle.Abacus.Domain
{
    public interface IGraphNodeContainer
    {
        GraphNodeCollection GraphNodes { get; }

        void AddGraphNode(IGraphNode item);

        IGraphNode AddGraphNode(string name);
    }
}
