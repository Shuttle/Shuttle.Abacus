namespace Shuttle.Abacus.UI.Core.Messaging
{
    public interface IHaveCancelMessage
    {
        Message CancelMessage { get; }
        bool HasCancelMessage { get; }
    }
}
