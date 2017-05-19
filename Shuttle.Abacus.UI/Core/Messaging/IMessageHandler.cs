namespace Shuttle.Abacus.Shell.Core.Messaging
{
    public interface IMessageHandler<T> where T : class
    {
        void HandleMessage(T message);
    }
}
