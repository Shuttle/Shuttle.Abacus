using Shuttle.Abacus.DataAccess;
using Shuttle.Abacus.UI.Core.Messaging;
using Shuttle.Abacus.UI.Core.Presentation;
using Shuttle.Abacus.UI.Messages.SystemUser;

namespace Shuttle.Abacus.UI.Coordinators.Interfaces
{
    public interface ISystemUserCoordinator :
        ICoordinator,
        IMessageHandler<ListSystemUserMessage>,
        IMessageHandler<NewSystemUserMessage>,
        IMessageHandler<EditLoginNameMessage>,
        IMessageHandler<EditPermissionsMessage>
    {
        ISystemUserQuery SystemUserQuery { get; set; }
    }
}
