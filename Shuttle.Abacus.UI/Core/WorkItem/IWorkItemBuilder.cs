using Shuttle.Abacus.Shell.Core.Presentation;

namespace Shuttle.Abacus.Shell.Core.WorkItem
{
    public interface IWorkItemBuilderController
    {
        IWorkItemBuilder ControlledBy<T>() where T : IWorkItemController;
    }

    public interface IWorkItemBuilder
    {
        IWorkItem ShowIn<T>() where T : IWorkItemPresenter;
    }
}
