using Shuttle.Abacus.UI.Core.Presentation;

namespace Shuttle.Abacus.UI.Core.WorkItem
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
