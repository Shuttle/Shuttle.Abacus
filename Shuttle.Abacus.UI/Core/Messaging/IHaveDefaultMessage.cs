namespace Shuttle.Abacus.Shell.Core.Messaging
{
    public interface IHaveDefaultMessage
    {
        Message DefaultMessage { get; }
        bool HasDefaultMessage { get; }
    }
}
