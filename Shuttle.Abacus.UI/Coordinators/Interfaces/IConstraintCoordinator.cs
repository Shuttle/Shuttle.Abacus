namespace Abacus.UI
{
    public interface IConstraintCoordinator :
        IMessageHandler<PopulateResourceMessage>,
        IMessageHandler<ResourceMenuRequestMessage>,
        IMessageHandler<ManageCalculationConstraintsMessage>
    {
    }
}
