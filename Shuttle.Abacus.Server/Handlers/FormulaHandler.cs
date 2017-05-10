using System;
using Shuttle.Abacus.ApplicationService;
using Shuttle.Abacus.DataAccess;
using Shuttle.Abacus.Domain;
using Shuttle.Abacus.Infrastructure;
using Shuttle.Abacus.Messages.v1;
using Shuttle.Core.Data;
using Shuttle.Core.Infrastructure;
using Shuttle.Esb;

namespace Shuttle.Abacus.Server.Handlers
{
    public class FormulaHandler :
        IMessageHandler<CreateFormulaCommand>,
        IMessageHandler<ChangeFormulaCommand>,
        IMessageHandler<DeleteFormulaCommand>
    {
        private readonly IArgumentAnswerFactory _argumentAnswerFactoryProvider;
        private readonly IConstraintFactory _constraintFactoryProvider;
        private readonly IDatabaseContextFactory _databaseContextFactory;
        private readonly IFormulaQuery _formulaQuery;
        private readonly IFormulaRepository _formulaRepository;
        private readonly IOperationFactory _operationFactoryProvider;
        private readonly IRepositoryProvider _repositoryProvider;
        private readonly ITaskFactory _taskFactory;
        private readonly IFactoryProvider<IValueSourceFactory> _valueSourceFactoryProvider;

        public FormulaHandler
        (
            IDatabaseContextFactory databaseContextFactory,
            IFormulaQuery formulaQuery,
            IFormulaRepository formulaRepository, IOperationFactory operationFactoryProvider,
            IFactoryProvider<IValueSourceFactory> valueSourceFactoryProvider,
            IConstraintFactory constraintFactoryProvider,
            IArgumentAnswerFactory argumentAnswerFactoryProvider,
            ITaskFactory taskFactory,
            IRepositoryProvider repositoryProvider)
        {
            Guard.AgainstNull(databaseContextFactory, "databaseContextFactory");
            Guard.AgainstNull(formulaQuery, "formulaQuery");
            Guard.AgainstNull(formulaRepository, "formulaRepository");
            Guard.AgainstNull(taskFactory, "taskFactory");
            Guard.AgainstNull(repositoryProvider, "repositoryProvider");
            Guard.AgainstNull(operationFactoryProvider, "operationFactoryProvider");
            Guard.AgainstNull(valueSourceFactoryProvider, "valueSourceFactoryProvider");
            Guard.AgainstNull(constraintFactoryProvider, "constraintFactoryProvider");
            Guard.AgainstNull(argumentAnswerFactoryProvider, "argumentAnswerFactoryProvider");

            _databaseContextFactory = databaseContextFactory;
            _formulaQuery = formulaQuery;
            _formulaRepository = formulaRepository;
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

        public void ProcessMessage(IHandlerContext<CreateFormulaCommand> context)
        {
            var message = context.Message;

            using (_databaseContextFactory.Create())
            {
                var formula = new Formula(Guid.NewGuid(), message.Name, message.ExecutionType);

                if (!string.IsNullOrEmpty(message.MaximumFormulaName))
                {
                    formula.WithMaximum(message.MaximumFormulaName);
                }

                if (!string.IsNullOrEmpty(message.MinimumFormulaName))
                {
                    formula.WithMaximum(message.MinimumFormulaName);
                }

                _formulaRepository.Add(formula);
            }

            context.ReplyOK();
        }

        public void ProcessMessage(IHandlerContext<DeleteFormulaCommand> context)
        {
            var message = context.Message;

            using (_databaseContextFactory.Create())
            {
                _formulaRepository.Remove(message.FormulaId);
            }

            context.ReplyOK();
        }
    }
}