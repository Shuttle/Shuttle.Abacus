namespace Shuttle.Abacus
{
    public interface IGraphNodeContainer
    {
        GraphNodeCollection GraphNodes { get; }

        void AddGraphNode(IGraphNode item);

        IGraphNode AddGraphNode(string name);
    }
}
