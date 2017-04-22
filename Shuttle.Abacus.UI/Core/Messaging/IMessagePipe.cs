namespace Shuttle.Abacus.UI.Core.Messaging
{
    public interface IMessagePipe<T> where T : Message
    {
        T Execute(T message);
    }
}
