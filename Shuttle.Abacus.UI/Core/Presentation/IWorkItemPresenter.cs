using Shuttle.Abacus.UI.Core.Messaging;
using Shuttle.Abacus.UI.Messages.Core;

namespace Shuttle.Abacus.UI.Core.Presentation
{
    public interface IWorkItemPresenter : 
        IPresenter,
        IMessageHandler<ShowPresenterMessage>
    {
        void AddPresenter(IPresenter presenter);
        void SelectPresenter(IPresenter presenter);
        
        void DisableMessage<T>();
        void EnableMessage<T>();
        bool IsMessageEnabled(Message message);
        void ResetChanges();
    }
}
