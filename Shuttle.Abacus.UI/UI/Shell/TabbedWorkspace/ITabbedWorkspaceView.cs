using Shuttle.Abacus.UI.Core.Presentation;
using Shuttle.Abacus.UI.Core.WorkItem;

namespace Shuttle.Abacus.UI.UI.Shell.TabbedWorkspace
{
    public interface ITabbedWorkspaceView : IWorkspaceView
    {
        void RemoveTab(IWorkItem workItem);
        void SetTabText(IWorkItem workItem);
        IWorkItem SelectedWorkItem { get; }
        void SetTabWaiting(IWorkItem workItem);
        void SetTabReady(IWorkItem workItem);
        void Show(IWorkItem workItem);
    }
}
