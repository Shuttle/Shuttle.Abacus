using Shuttle.Abacus.Shell.Core.Messaging;
using Shuttle.Abacus.Shell.Messages.Core;

namespace Shuttle.Abacus.Shell.Core.Presentation
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
