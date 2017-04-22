using Shuttle.Abacus.UI.Core.Messaging;
using Shuttle.Abacus.UI.Messages.Calculation;
using Shuttle.Abacus.UI.Messages.Resources;

namespace Shuttle.Abacus.UI.Coordinators.Interfaces
{
    public interface IConstraintCoordinator :
        IMessageHandler<PopulateResourceMessage>,
        IMessageHandler<ResourceMenuRequestMessage>,
        IMessageHandler<ManageCalculationConstraintsMessage>
    {
    }
}
