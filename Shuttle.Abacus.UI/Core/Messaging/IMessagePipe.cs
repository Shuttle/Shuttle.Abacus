namespace Shuttle.Abacus.Shell.Core.Messaging
{
    public interface IMessagePipe<T> where T : Message
    {
        T Execute(T message);
    }
}
