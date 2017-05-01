using System;
using Shuttle.Abacus.ApplicationService;
using Shuttle.Abacus.DataAccess;
using Shuttle.Abacus.Domain;
using Shuttle.Abacus.DTO;
using Shuttle.Abacus.Infrastructure;
using Shuttle.Core.Data;
using Shuttle.Core.Infrastructure;
using Shuttle.Esb;

namespace Shuttle.Abacus.Server.Handlers
{
    public class FormulaHandler :
        IMessageHandler<CreateFormulaCommand>,
        IMessageHandler<ChangeFormulaCommand>,
        IMessageHandler<DeleteFormulaCommand>,
        IMessageHandler<ChangeFormulaOrderCommand>
    {
        private readonly IFactoryProvider<IArgumentAnswerFactory> _argumentAnswerFactoryProvider;
        private readonly IFactoryProvider<IConstraintFactory> _constraintFactoryProvider;
        private readonly IDatabaseContextFactory _databaseContextFactory;
        private readonly IFormulaOwnerService _formulaOwnerService;
        private readonly IFormulaQuery _formulaQuery;
        private readonly IFormulaRepository _formulaRepository;
        private readonly IFactoryProvider<IOperationFactory> _operationFactoryProvider;
        private readonly IRepositoryProvider _repositoryProvider;
        private readonly ITaskFactory _taskFactory;
        private readonly IFactoryProvider<IValueSourceFactory> _valueSourceFactoryProvider;

        public FormulaHandler
        (
            IDatabaseContextFactory databaseContextFactory,
            IFormulaQuery formulaQuery,
            IFormulaRepository formulaRepository, IFactoryProvider<IOperationFactory> operationFactoryProvider,
            IFactoryProvider<IValueSourceFactory> valueSourceFactoryProvider,
            IFactoryProvider<IConstraintFactory> constraintFactoryProvider,
            IFactoryProvider<IArgumentAnswerFactory> argumentAnswerFactoryProvider,
            IFormulaOwnerService formulaOwnerService,
            ITaskFactory taskFactory,
            IRepositoryProvider repositoryProvider)
        {
            Guard.AgainstNull(databaseContextFactory, "databaseContextFactory");
            Guard.AgainstNull(formulaQuery, "formulaQuery");
            Guard.AgainstNull(formulaRepository, "formulaRepository");
            Guard.AgainstNull(formulaOwnerService, "formulaOwnerService");
            Guard.AgainstNull(taskFactory, "taskFactory");
            Guard.AgainstNull(repositoryProvider, "repositoryProvider");
            Guard.AgainstNull(operationFactoryProvider, "operationFactoryProvider");
            Guard.AgainstNull(valueSourceFactoryProvider, "valueSourceFactoryProvider");
            Guard.AgainstNull(constraintFactoryProvider, "constraintFactoryProvider");
            Guard.AgainstNull(argumentAnswerFactoryProvider, "argumentAnswerFactoryProvider");

            _databaseContextFactory = databaseContextFactory;
            _formulaQuery = formulaQuery;
            _formulaRepository = formulaRepository;
            _formulaOwnerService = formulaOwnerService;
            _taskFactory = taskFactory;
            _repositoryProvider = repositoryProvider;
            _operationFactoryProvider = operationFactoryProvider;
            _valueSourceFactoryProvider = valueSourceFactoryProvider;
            _constraintFactoryProvider = constraintFactoryProvider;
            _argumentAnswerFactoryProvider = argumentAnswerFactoryProvider;
        }

        public void ProcessMessage(IHandlerContext<ChangeFormulaCommand> context)
        {
            var message = context.Message;

            using (_databaseContextFactory.Create())
            {
                throw new NotImplementedException();
                //_taskFactory.Create<IChangeFormulaTask>().Execute(
                //    _formulaRepository.Get(message.FormulaId).
                //        ProcessCommand(
                //            message,
                //            _operationFactoryProvider,
                //            _valueSourceFactoryProvider,
                //            _constraintFactoryProvider,
                //            _argumentAnswerFactoryProvider,
                //            _argumentDTOMapper));
            }
        }

        public void ProcessMessage(IHandlerContext<ChangeFormulaOrderCommand> context)
        {
            var message = context.Message;

            using (_databaseContextFactory.Create())
            {
                var owner =
                    _repositoryProvider.Get(message.OwnerName).Get<IFormulaOwner>(
                        message.OwnerId);

                owner.ProcessCommand(message, _formulaOwnerService);
            }
        }

        public void ProcessMessage(IHandlerContext<CreateFormulaCommand> context)
        {
            var message = context.Message;

            using (_databaseContextFactory.Create())
            {
                var formula = new Formula();

                foreach (var constraint in message.Constraints)
                {
                    formula.AddConstraint(constraint);
                }

                foreach (var operation in message.Operations)
                {
                    formula.AddOperation(operation);
                }

                _formulaRepository.Add(message.OwnerName, message.OwnerId, formula);

                //var owner =
                //    _repositoryProvider.Get(message.OwnerName).Get<IFormulaOwner>(message.OwnerId);

                //var formula = new Formula(message,
                //    _operationFactoryProvider,
                //    _valueSourceFactoryProvider,
                //    _constraintFactoryProvider,
                //    _argumentAnswerFactoryProvider);

                //owner.AddFormula(formula);

                //_taskFactory.Create<ICreateFormulaTask>().Execute(new OwnerModel(owner, formula));
            }
        }

        public void ProcessMessage(IHandlerContext<DeleteFormulaCommand> context)
        {
            var message = context.Message;

            using (_databaseContextFactory.Create())
            {
                var row = _formulaQuery.Get(message.FormulaId);

                var owner =
                    _repositoryProvider.Get(FormulaColumns.OwnerName.MapFrom(row)).Get<IFormulaOwner>(
                        FormulaColumns.OwnerId.MapFrom(row));

                owner.RemoveFormula(message.FormulaId);
            }
        }
    }
}