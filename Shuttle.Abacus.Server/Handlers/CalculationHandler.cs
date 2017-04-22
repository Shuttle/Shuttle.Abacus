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
        private readonly IFactoryProvider<IArgumentAnswerFactory> _argumentAnswerFactoryProvider;
        private readonly Infrastructure.IMapper<ArgumentDTO, Argument> _argumentDTOMapper;
        private readonly IFactoryProvider<ICalculationFactory> _calculationFactoryProvider;
        private readonly ICalculationRepository _calculationRepository;
        private readonly IFactoryProvider<IConstraintFactory> _constraintFactoryProvider;
        private readonly ILimitRepository _limitRepository;
        private readonly ITaskFactory _taskFactory;
        private readonly IMethodRepository _methodRepository;

        public CalculationHandler(
            IMethodRepository methodRepository,
            IFactoryProvider<ICalculationFactory> calculationFactoryProvider,
            ICalculationRepository calculationRepository,
            IFactoryProvider<IConstraintFactory> constraintFactoryProvider,
            IFactoryProvider<IArgumentAnswerFactory> argumentAnswerFactoryProvider, Infrastructure.IMapper<ArgumentDTO, Argument> argumentDTOMapper,
            ILimitRepository limitRepository,
            ITaskFactory taskFactory)
        {
            this._methodRepository = methodRepository;
            this._argumentDTOMapper = argumentDTOMapper;
            this._limitRepository = limitRepository;
            _taskFactory = taskFactory;
            this._calculationFactoryProvider = calculationFactoryProvider;
            this._calculationRepository = calculationRepository;
            this._constraintFactoryProvider = constraintFactoryProvider;
            this._argumentAnswerFactoryProvider = argumentAnswerFactoryProvider;
        }

        public void ProcessMessage(IHandlerContext<ChangeCalculationCommand> context)
        {
            var message = context.Message;

            var method = _methodRepository.Get(message.MethodId);

            method.ProcessCommand(message);

            var calculation = method.CalculationCollection.Get(message.CalculationId);

            calculation.ClearGraphNodeArguments();

            message.GraphNodeArguments.ForEach(
                item => calculation.AddGraphNodeArgument(_argumentDTOMapper.MapFrom(item.ArgumentDTO), item.Format));

            _calculationRepository.Save(calculation);
        }

        public void ProcessMessage(IHandlerContext<ChangeCalculationOrderCommand> context)
        {
            var message = context.Message;

            var method = _methodRepository.Get(message.MethodId);

            method.ProcessCommand(message);

            _calculationRepository.SaveOrdered(method);
        }

        public void ProcessMessage(IHandlerContext<CreateCalculationCommand> context)
        {
            var message = context.Message;

            var calculation = _calculationFactoryProvider.Get(message.Type).Create(message.Name, message.Required);


            message.GraphNodeArguments.ForEach(
                item => calculation.AddGraphNodeArgument(_argumentDTOMapper.MapFrom(item.ArgumentDTO), item.Format));

            var method = _methodRepository.Get(message.MethodId);

            method.ProcessCommand(message, calculation);
        }

        public void ProcessMessage(IHandlerContext<DeleteCalculationCommand> context)
        {
            var message = context.Message;

            var method = _methodRepository.Get(message.MethodId);
            var calculation = _calculationRepository.Get(message.CalculationId);

            method.ProcessCommand(message);

            foreach (var limit in method.Limits)
            {
                _limitRepository.Remove(limit);
            }

            _calculationRepository.Remove(calculation);
        }

        public void ProcessMessage(IHandlerContext<GrabCalculationsCommand> context)
        {
            var message = context.Message;

            var method = _methodRepository.Get(message.MethodId);

            method.ProcessCommand(message);

            _calculationRepository.SaveOwnershipGraph(method);
        }

        public void ProcessMessage(IHandlerContext<SetCalculationConstraintsCommand> context)
        {
            var message = context.Message;

            _taskFactory.Create<ISetCalculationConstraintsTask>().Execute(
                _calculationRepository
                    .Get(message.CalculationId)
                    .ProcessCommand(
                        message,
                        _constraintFactoryProvider,
                        _argumentAnswerFactoryProvider, _argumentDTOMapper));
            ;
        }
    }
}