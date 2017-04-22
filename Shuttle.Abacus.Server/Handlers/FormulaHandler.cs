using Shuttle.Abacus.ApplicationService;
using Shuttle.Abacus.DataAccess;
using Shuttle.Abacus.Domain;
using Shuttle.Abacus.DTO;
using Shuttle.Abacus.Infrastructure;
using Shuttle.Esb;

namespace Shuttle.Abacus.Server.Handlers
{
    public class FormulaHandler :
        IMessageHandler<CreateFormulaCommand>,
        IMessageHandler<ChangeFormulaCommand>,
        IMessageHandler<DeleteFormulaCommand>,
        IMessageHandler<ChangeFormulaOrderCommand>
    {
        private readonly Domain.IFactoryProvider<IConstraintFactory> constraintFactoryProvider;
        private readonly Domain.IFactoryProvider<IArgumentAnswerFactory> argumentAnswerFactoryProvider;
        private readonly IMapper<ArgumentDTO, Argument> argumentDTOMapper;
        private readonly ITaskFactory _taskFactory;
        private readonly IRepositoryProvider _repositoryProvider;
        private readonly IFormulaOwnerService formulaOwnerService;
        private readonly IFormulaQuery formulaQuery;
        private readonly IFormulaRepository formulaRepository;
        private readonly Domain.IFactoryProvider<IOperationFactory> operationFactoryProvider;
        private readonly Domain.IFactoryProvider<IValueSourceFactory> valueSourceFactoryProvider;

        public FormulaHandler
            (
            IFormulaQuery formulaQuery,
            IFormulaRepository formulaRepository, Domain.IFactoryProvider<IOperationFactory> operationFactoryProvider, Domain.IFactoryProvider<IValueSourceFactory> valueSourceFactoryProvider, Domain.IFactoryProvider<IConstraintFactory> constraintFactoryProvider, Domain.IFactoryProvider<IArgumentAnswerFactory> argumentAnswerFactoryProvider,
            IFormulaOwnerService formulaOwnerService,
            IMapper<ArgumentDTO, Argument> argumentDTOMapper,
            ITaskFactory taskFactory,
            IRepositoryProvider repositoryProvider)
        {
            this.formulaQuery = formulaQuery;
            this.formulaRepository = formulaRepository;
            this.formulaOwnerService = formulaOwnerService;
            this.argumentDTOMapper = argumentDTOMapper;
            _taskFactory = taskFactory;
            _repositoryProvider = repositoryProvider;
            this.operationFactoryProvider = operationFactoryProvider;
            this.valueSourceFactoryProvider = valueSourceFactoryProvider;
            this.constraintFactoryProvider = constraintFactoryProvider;
            this.argumentAnswerFactoryProvider = argumentAnswerFactoryProvider;
        }

        public void ProcessMessage(IHandlerContext<CreateFormulaCommand> context)
        {
            var message = context.Message;

            var owner =
                _repositoryProvider.Get(message.OwnerName).Get<IFormulaOwner>(message.OwnerId);

            var formula = new Formula(message,
                                      operationFactoryProvider,
                                      valueSourceFactoryProvider,
                                      constraintFactoryProvider,
                                      argumentAnswerFactoryProvider);

            owner.AddFormula(formula);

            _taskFactory.Create<ICreateFormulaTask>().Execute(new OwnerModel(owner, formula));
        }

        public void ProcessMessage(IHandlerContext<ChangeFormulaCommand> context)
        {
            var message = context.Message;

            _taskFactory.Create<IChangeFormulaTask>().Execute(
                formulaRepository.Get(message.FormulaId).
                    ProcessCommand(
                        message,
                        operationFactoryProvider,
                        valueSourceFactoryProvider,
                        constraintFactoryProvider,
                        argumentAnswerFactoryProvider,
                        argumentDTOMapper));
        }

        public void ProcessMessage(IHandlerContext<DeleteFormulaCommand> context)
        {
            var message = context.Message;

            var query = formulaQuery.Get(message.FormulaId);

            var owner =
                _repositoryProvider.Get(FormulaColumns.OwnerName.MapFrom(query.Row)).Get<IFormulaOwner>(
                    FormulaColumns.OwnerId.MapFrom(query.Row));

            owner.RemoveFormula(message.FormulaId);
        }

        public void ProcessMessage(IHandlerContext<ChangeFormulaOrderCommand> context)
        {
            var message = context.Message;

            var owner =
                _repositoryProvider.Get(message.OwnerName).Get<IFormulaOwner>(
                    message.OwnerId);

            owner.ProcessCommand(message, formulaOwnerService);
        }
    }
}
