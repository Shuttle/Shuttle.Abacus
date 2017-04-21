using Shuttle.Abacus.ApplicationService;
using Shuttle.Abacus.Domain;
using Shuttle.Abacus.DTO;
using Shuttle.Esb;

namespace Shuttle.Abacus.Server.Handlers
{
    public class CalculationHandler :
        IMessageHandler<CreateCalculationCommand>,
        IMessageHandler<ChangeCalculationOrderCommand>,
        IMessageHandler<SetCalculationConstraintsCommand>,
        IMessageHandler<ChangeCalculationCommand>,
        IMessageHandler<DeleteCalculationCommand>,
        IMessageHandler<GrabCalculationsCommand>
    {
        private readonly IFactoryProvider<IArgumentAnswerFactory> argumentAnswerFactoryProvider;
        private readonly IMapper<ArgumentDTO, Argument> argumentDTOMapper;
        private readonly IFactoryProvider<ICalculationFactory> calculationFactoryProvider;
        private readonly ICalculationRepository calculationRepository;
        private readonly IFactoryProvider<IConstraintFactory> constraintFactoryProvider;
        private readonly ILimitRepository limitRepository;
        private readonly IMethodRepository methodRepository;

        public CalculationHandler(
            IMethodRepository methodRepository,
            IFactoryProvider<ICalculationFactory> calculationFactoryProvider,
            ICalculationRepository calculationRepository,
            IFactoryProvider<IConstraintFactory> constraintFactoryProvider,
            IFactoryProvider<IArgumentAnswerFactory> argumentAnswerFactoryProvider,
            IMapper<ArgumentDTO, Argument> argumentDTOMapper,
            ILimitRepository limitRepository)
        {
            this.methodRepository = methodRepository;
            this.argumentDTOMapper = argumentDTOMapper;
            this.limitRepository = limitRepository;
            this.calculationFactoryProvider = calculationFactoryProvider;
            this.calculationRepository = calculationRepository;
            this.constraintFactoryProvider = constraintFactoryProvider;
            this.argumentAnswerFactoryProvider = argumentAnswerFactoryProvider;
        }

        public void ProcessMessage(IHandlerContext<ChangeCalculationCommand> context)
        {
            var message = context.Message;

            var method = methodRepository.Get(message.MethodId);

            method.ProcessCommand(message);

            var calculation = method.CalculationCollection.Get(message.CalculationId);

            calculation.ClearGraphNodeArguments();

            message.GraphNodeArguments.ForEach(
                item => calculation.AddGraphNodeArgument(argumentDTOMapper.MapFrom(item.ArgumentDTO), item.Format));

            calculationRepository.Save(calculation);
        }

        public void ProcessMessage(IHandlerContext<ChangeCalculationOrderCommand> context)
        {
            var message = context.Message;

            var method = methodRepository.Get(message.MethodId);

            method.ProcessCommand(message);

            calculationRepository.SaveOrdered(method);
        }

        public void ProcessMessage(IHandlerContext<CreateCalculationCommand> context)
        {
            var message = context.Message;

            var calculation = calculationFactoryProvider.Get(message.Type).Create(message.Name, message.Required);


            message.GraphNodeArguments.ForEach(
                item => calculation.AddGraphNodeArgument(argumentDTOMapper.MapFrom(item.ArgumentDTO), item.Format));

            var method = methodRepository.Get(message.MethodId);

            method.ProcessCommand(message, calculation);
        }

        public void ProcessMessage(IHandlerContext<DeleteCalculationCommand> context)
        {
            var message = context.Message;

            var method = methodRepository.Get(message.MethodId);
            var calculation = calculationRepository.Get(message.CalculationId);

            method.ProcessCommand(message);

            foreach (var limit in method.Limits)
            {
                limitRepository.Remove(limit);
            }

            calculationRepository.Remove(calculation);
        }

        public void ProcessMessage(IHandlerContext<GrabCalculationsCommand> context)
        {
            var message = context.Message;

            var method = methodRepository.Get(message.MethodId);

            method.ProcessCommand(message);

            calculationRepository.SaveOwnershipGraph(method);
        }

        public void ProcessMessage(IHandlerContext<SetCalculationConstraintsCommand> context)
        {
            var message = context.Message;

            TaskFactory.Create<ISetCalculationConstraintsTask>().Execute(
                calculationRepository
                    .Get(message.CalculationId)
                    .ProcessCommand(
                        message,
                        constraintFactoryProvider,
                        argumentAnswerFactoryProvider, argumentDTOMapper)))
            ;
        }
    }
}