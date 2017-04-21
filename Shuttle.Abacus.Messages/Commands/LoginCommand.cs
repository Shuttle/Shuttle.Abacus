using NServiceBus;

namespace Abacus.Messages
{
    public class LoginCommand : IMessage
    {
        public string LoginName { get; set; }
    }
}
