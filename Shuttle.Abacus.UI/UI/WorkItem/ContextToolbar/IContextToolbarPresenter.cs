using Shuttle.Abacus.UI.Core.Messaging;
using Shuttle.Abacus.UI.Core.Presentation;

namespace Shuttle.Abacus.UI.UI.WorkItem.ContextToolbar
{
    public interface IContextToolbarPresenter : IWorkItemPresenter
    {
        void Close();
        void InvokeMessage(Message message);
        void PresenterSelected(IPresenter presenter);
        void NoPresenterSelected();
    }
}
