namespace Shuttle.Abacus.UI.Core.Messaging
{
    public interface IHaveDefaultMessage
    {
        Message DefaultMessage { get; }
        bool HasDefaultMessage { get; }
    }
}
