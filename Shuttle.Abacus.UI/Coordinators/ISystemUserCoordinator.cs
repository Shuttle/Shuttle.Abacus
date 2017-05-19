using Shuttle.Abacus.Shell.Core.Messaging;
using Shuttle.Abacus.Shell.Core.Presentation;
using Shuttle.Abacus.Shell.Messages.SystemUser;

namespace Shuttle.Abacus.Shell.Coordinators
{
    public interface ISystemUserCoordinator :
        ICoordinator,
        IMessageHandler<ListSystemUserMessage>,
        IMessageHandler<NewSystemUserMessage>,
        IMessageHandler<EditLoginNameMessage>,
        IMessageHandler<EditPermissionsMessage>
    {
    }
}