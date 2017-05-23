using Shuttle.Core.Infrastructure;

namespace Shuttle.Abacus
{
    public class ExecutionContextFactory
    {
        private readonly ICachedArgumentProvider _argumentProvider;

        public ExecutionContextFactory(ICachedArgumentProvider argumentProvider)
        {
            Guard.AgainstNull(argumentProvider, "argumentProvider");

            _argumentProvider = argumentProvider;
        }
    }
}