using Shuttle.Abacus.UI.Core.Messaging;
using Shuttle.Abacus.UI.Core.Presentation;
using Shuttle.Abacus.UI.Messages.SystemUser;

namespace Shuttle.Abacus.UI.UI.SystemUser
{
    public interface ISystemUserPresenter :
        IPresenter,
        IMessageHandler<EditLoginNameMessage>
    {
        void LoginNameExited();
    }
}
