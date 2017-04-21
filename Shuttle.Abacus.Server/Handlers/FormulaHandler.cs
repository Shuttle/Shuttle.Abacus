using Shuttle.Abacus.ApplicationService;
using Shuttle.Abacus.DataAccess.Definitions;
using Shuttle.Abacus.DataAccess.Query;
using Shuttle.Abacus.Domain;
using Shuttle.Abacus.DTO;
using Shuttle.Esb;

namespace Shuttle.Abacus.Server.Handlers
{
    public class FormulaHandler :
        IMessageHandler<CreateFormulaCommand>,
        IMessageHandler<ChangeFormulaCommand>,
        IMessageHandler<DeleteFormulaCommand>,
        IMessageHandler<ChangeFormulaOrderCommand>
    {
        private readonly IFactoryProvider<IConstraintFactory> constraintFactoryProvider;
        private readonly IFactoryProvider<IArgumentAnswerFactory> argumentAnswerFactoryProvider;
        private readonly IMapper<ArgumentDTO, Argument> argumentDTOMapper;
        private readonly IFormulaOwnerService formulaOwnerService;
        private readonly IFormulaQuery formulaQuery;
        private readonly IFormulaRepository formulaRepository;
        private readonly IFactoryProvider<IOperationFactory> operationFactoryProvider;
        private readonly IFactoryProvider<IValueSourceFactory> valueSourceFactoryProvider;

        public FormulaHandler
            (
            IFormulaQuery formulaQuery,
            IFormulaRepository formulaRepository,
            IFactoryProvider<IOperationFactory> operationFactoryProvider,
            IFactoryProvider<IValueSourceFactory> valueSourceFactoryProvider,
            IFactoryProvider<IConstraintFactory> constraintFactoryProvider,
            IFactoryProvider<IArgumentAnswerFactory> argumentAnswerFactoryProvider,
            IFormulaOwnerService formulaOwnerService,
            IMapper<ArgumentDTO, Argument> argumentDTOMapper)
        {
            this.formulaQuery = formulaQuery;
            this.formulaRepository = formulaRepository;
            this.formulaOwnerService = formulaOwnerService;
            this.argumentDTOMapper = argumentDTOMapper;
            this.operationFactoryProvider = operationFactoryProvider;
            this.valueSourceFactoryProvider = valueSourceFactoryProvider;
            this.constraintFactoryProvider = constraintFactoryProvider;
            this.argumentAnswerFactoryProvider = argumentAnswerFactoryProvider;
        }

        public void ProcessMessage(IHandlerContext<CreateFormulaCommand> context)
        {
            var message = context.Message;

            var owner =
                RepositoryProvider.Get(message.OwnerName).Get<IFormulaOwner>(
                    message.OwnerId);

            var formula = new Formula(message,
                                      operationFactoryProvider,
                                      valueSourceFactoryProvider,
                                      constraintFactoryProvider,
                                      argumentAnswerFactoryProvider);

            owner.AddFormula(formula);

            TaskFactory.Create<ICreateFormulaTask>().Execute(new OwnerModel(owner, formula));
        }

        public void ProcessMessage(IHandlerContext<ChangeFormulaCommand> context)
        {
            var message = context.Message;

            TaskFactory.Create<IChangeFormulaTask>().Execute(
                                             formulaRepository.Get(message.FormulaId).
                                                 ProcessCommand(
                                                 message,
                                                 operationFactoryProvider,
                                                 valueSourceFactoryProvider,
                                                 constraintFactoryProvider,
                                                 argumentAnswerFactoryProvider,
                                                 argumentDTOMapper))        }

        public void ProcessMessage(IHandlerContext<DeleteFormulaCommand> context)
        {
            var message = context.Message;

            var query = formulaQuery.Get(message.FormulaId);

            var owner =
                RepositoryProvider.Get(FormulaColumns.OwnerName.MapFrom(query.Row)).Get<IFormulaOwner>(
                    FormulaColumns.OwnerId.MapFrom(query.Row));

            owner.RemoveFormula(message.FormulaId);
        }

        public void ProcessMessage(IHandlerContext<ChangeFormulaOrderCommand> context)
        {
            var message = context.Message;

            var owner =
                RepositoryProvider.Get(message.OwnerName).Get<IFormulaOwner>(
                    message.OwnerId);

            owner.ProcessCommand(message, formulaOwnerService);
        }
    }
}
