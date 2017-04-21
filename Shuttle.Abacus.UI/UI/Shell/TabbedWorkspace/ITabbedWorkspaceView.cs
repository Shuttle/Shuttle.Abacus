namespace Abacus.UI
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
