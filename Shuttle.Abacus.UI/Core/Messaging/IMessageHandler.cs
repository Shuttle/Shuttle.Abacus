namespace Shuttle.Abacus.UI.Core.Messaging
{
    public interface IMessageHandler<T> where T : class
    {
        void HandleMessage(T message);
    }
}
