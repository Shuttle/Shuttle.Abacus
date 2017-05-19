using Shuttle.Abacus.Shell.Core.Messaging;
using Shuttle.Abacus.Shell.Core.Presentation;

namespace Shuttle.Abacus.Shell.UI.WorkItem.ContextToolbar
{
    public interface IContextToolbarPresenter : IWorkItemPresenter
    {
        void Close();
        void InvokeMessage(Message message);
        void PresenterSelected(IPresenter presenter);
        void NoPresenterSelected();
    }
}
