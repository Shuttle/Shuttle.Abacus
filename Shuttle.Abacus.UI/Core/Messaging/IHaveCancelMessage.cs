namespace Abacus.UI
{
    public interface IHaveCancelMessage
    {
        Message CancelMessage { get; }
        bool HasCancelMessage { get; }
    }
}
