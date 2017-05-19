using Shuttle.Abacus.Shell.Core.Messaging;
using Shuttle.Abacus.Shell.Core.Presentation;
using Shuttle.Abacus.Shell.Messages.SystemUser;

namespace Shuttle.Abacus.Shell.UI.SystemUser
{
    public interface ISystemUserPresenter :
        IPresenter,
        IMessageHandler<EditLoginNameMessage>
    {
        void LoginNameExited();
    }
}
