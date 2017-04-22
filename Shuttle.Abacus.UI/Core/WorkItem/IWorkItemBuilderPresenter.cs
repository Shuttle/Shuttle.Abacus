using System.Drawing;

namespace Shuttle.Abacus.UI.Core.WorkItem
{
    public interface IWorkItemBuilderPresenter : IWorkItem
    {
        IWorkItemBuilderPresenter WithModel<T>(T presenterModel) where T : class;
        IWorkItemBuilderPresenter AssignText(string presenterText);
        IWorkItemBuilderPresenter AssignImage(Image presenterImage);
    }
}
