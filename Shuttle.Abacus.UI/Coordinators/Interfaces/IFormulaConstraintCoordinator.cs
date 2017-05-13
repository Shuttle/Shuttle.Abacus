using Shuttle.Abacus.UI.Core.Messaging;
using Shuttle.Abacus.UI.Messages.Formula;
using Shuttle.Abacus.UI.Messages.Resources;

namespace Shuttle.Abacus.UI.Coordinators.Interfaces
{
    public interface IFormulaConstraintCoordinator :
        IMessageHandler<PopulateResourceMessage>,
        IMessageHandler<ResourceMenuRequestMessage>,
        IMessageHandler<ManageFormulaConstraintsMessage>
    {
    }
}
