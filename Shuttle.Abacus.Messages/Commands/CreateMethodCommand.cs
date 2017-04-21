using Abacus.CommandMediators;
using NServiceBus;

namespace Abacus.Messages
{
    public class CreateMethodCommand : IMessage, ICreateMethodCommand
    {
        public string MethodName { get; set; }
    }
}