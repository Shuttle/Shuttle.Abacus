namespace Abacus.UI
{
    public interface IMessagePipe<T> where T : Message
    {
        T Execute(T message);
    }
}
