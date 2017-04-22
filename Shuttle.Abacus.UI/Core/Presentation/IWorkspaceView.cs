using Shuttle.Abacus.UI.Core.WorkItem;

namespace Shuttle.Abacus.UI.Core.Presentation
{
    public interface IWorkspaceView : IView
    {
        void Add(IWorkItem workItem);
    }
}
