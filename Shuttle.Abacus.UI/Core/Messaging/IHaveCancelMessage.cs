namespace Shuttle.Abacus.Shell.Core.Messaging
{
    public interface IHaveCancelMessage
    {
        Message CancelMessage { get; }
        bool HasCancelMessage { get; }
    }
}
