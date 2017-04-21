using System.Drawing;

namespace Abacus.UI
{
    public interface IWorkItemBuilderPresenter : IWorkItem
    {
        IWorkItemBuilderPresenter WithModel<T>(T presenterModel) where T : class;
        IWorkItemBuilderPresenter AssignText(string presenterText);
        IWorkItemBuilderPresenter AssignImage(Image presenterImage);
    }
}
