namespace Abacus.UI
{
    public interface IMessageHandler<T>
    {
        void HandleMessage(T message);
    }
}
