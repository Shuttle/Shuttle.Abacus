using Shuttle.Abacus.Shell.Core.WorkItem;

namespace Shuttle.Abacus.Shell.Core.Presentation
{
    public interface IWorkspaceView : IView
    {
        void Add(IWorkItem workItem);
    }
}
