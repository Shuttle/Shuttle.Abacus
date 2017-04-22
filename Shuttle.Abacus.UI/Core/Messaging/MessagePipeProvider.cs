using System.Collections.Generic;

namespace Shuttle.Abacus.UI.Core.Messaging
{
    public class MessagePipeProvider : IMessagePipeProvider
    {
        private static readonly Dictionary<string, object> providers = new Dictionary<string, object>();

        public void Add<T>(IMessagePipe<T> pipe) where T : Message
        {
            Guard.AgainstNull(pipe, "pipe");

            providers.Add(typeof(T).FullName, pipe);
        }

        public IMessagePipe<T> Get<T>() where T : Message
        {
            return (IMessagePipe<T>)providers[typeof(T).FullName];
        }
    }
}
