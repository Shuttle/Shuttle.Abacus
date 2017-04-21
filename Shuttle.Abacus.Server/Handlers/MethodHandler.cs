using Abacus.Application;
using Abacus.Domain;
using Abacus.Messages;
using NServiceBus;

namespace Abacus.Server
{
    public class MethodHandler :
        MessageHandler,
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

        public void Handle(ChangeMethodCommand message)
        {
            Transacted(
                () =>
                TaskFactory.Create<IChangeMethodTask>().Execute(
                    methodRepository.Get(message.MethodId).ProcessCommand(message)));
        }

        public void Handle(CopyMethodCommand message)
        {
            Transacted(uow =>
                {
                    uow.WillUse<Method>();

                    uow.WillUseFullObjectGraph();

                    var method = methodRepository
                        .Get(message.MethodId)
                        .Copy()
                        .ProcessCommand(message);

                    TaskFactory.Create<IAddMethodGraphTask>().Execute(method);
                });
        }

        public void Handle(CreateMethodCommand message)
        {
            Transacted(uow => methodRepository.Add(new Method(message)));
        }

        public void Handle(DeleteMethodCommand message)
        {
            Transacted(uow =>
                {
                    var method = methodRepository.Get(message.MethodId);

                    foreach(var limit in method.Limits)
                    {
                        limitRepository.Remove(limit);
                    }

                    methodRepository.Remove(method);
                });
        }
    }
}
