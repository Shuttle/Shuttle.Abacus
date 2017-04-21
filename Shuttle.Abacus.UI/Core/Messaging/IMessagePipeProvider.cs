namespace Abacus.UI
{
    public interface IMessagePipeProvider
    {
        void Add<T>(IMessagePipe<T> pipe) where T : Message;
        IMessagePipe<T> Get<T>() where T : Message;
    }
}
