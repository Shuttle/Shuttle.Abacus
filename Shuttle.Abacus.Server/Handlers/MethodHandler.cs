using Shuttle.Abacus.ApplicationService;
using Shuttle.Abacus.Domain;
using Shuttle.Esb;

namespace Shuttle.Abacus.Server.Handlers
{
    public class MethodHandler :
        IMessageHandler<CreateMethodCommand>,
        IMessageHandler<CopyMethodCommand>,
        IMessageHandler<ChangeMethodCommand>,
        IMessageHandler<DeleteMethodCommand>
    {
        private readonly ILimitRepository limitRepository;
        private readonly IMethodRepository methodRepository;

        public MethodHandler(IMethodRepository methodRepository, ILimitRepository limitRepository)
        {
            this.methodRepository = methodRepository;
            this.limitRepository = limitRepository;
        }

        public void ProcessMessage(IHandlerContext<CreateMethodCommand> context)
        {
            methodRepository.Add(new Method(context.Message));
        }

        public void ProcessMessage(IHandlerContext<CopyMethodCommand> context)
        {
            var message = context.Message;

            var method = methodRepository
                .Get(message.MethodId)
                .Copy()
                .ProcessCommand(message);

            TaskFactory.Create<IAddMethodGraphTask>().Execute(method);
        }

        public void ProcessMessage(IHandlerContext<ChangeMethodCommand> context)
        {
            var message = context.Message;

            TaskFactory.Create<IChangeMethodTask>().Execute(
                methodRepository.Get(message.MethodId).ProcessCommand(message));
        }

        public void ProcessMessage(IHandlerContext<DeleteMethodCommand> context)
        {
            var message = context.Message;

            var method = methodRepository.Get(message.MethodId);

            foreach (var limit in method.Limits)
            {
                limitRepository.Remove(limit);
            }

            methodRepository.Remove(method);
        }
    }
}
