namespace Shuttle.Abacus.UI.Core.Messaging
{
    public interface IMessageHandler<T>
    {
        void HandleMessage(T message);
    }
}
