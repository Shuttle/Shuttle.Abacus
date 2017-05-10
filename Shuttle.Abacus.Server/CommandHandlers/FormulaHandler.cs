using System;
using Shuttle.Abacus.Domain;
using Shuttle.Abacus.Messages.v1;
using Shuttle.Core.Data;
using Shuttle.Core.Infrastructure;
using Shuttle.Esb;
using Shuttle.Recall;

namespace Shuttle.Abacus.Server.CommandHandlers
{
    public class FormulaHandler :
        IMessageHandler<RegisterFormulaCommand>,
        IMessageHandler<ChangeFormulaCommand>,
        IMessageHandler<RemoveFormulaCommand>
    {
        private readonly IDatabaseContextFactory _databaseContextFactory;
        private readonly IEventStore _eventStore;
        private readonly IFormulaRepository _formulaRepository;

        public FormulaHandler(IDatabaseContextFactory databaseContextFactory, IEventStore eventStore,
            IFormulaRepository formulaRepository)
        {
            Guard.AgainstNull(databaseContextFactory, "databaseContextFactory");
            Guard.AgainstNull(formulaRepository, "formulaRepository");

            _databaseContextFactory = databaseContextFactory;
            _eventStore = eventStore;
            _formulaRepository = formulaRepository;
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

        public void ProcessMessage(IHandlerContext<RegisterFormulaCommand> context)
        {
            var message = context.Message;

            using (_databaseContextFactory.Create())
            {
                var id = Guid.NewGuid();
                var stream = new EventStream(id);
                var formula = new Formula(id);

                stream.AddEvent(formula.Register(message.Name, message.MaximumFormulaName, message.MinimumFormulaName));

                _eventStore.Save(stream);
            }

            context.ReplyOK();
        }

        public void ProcessMessage(IHandlerContext<RemoveFormulaCommand> context)
        {
            var message = context.Message;

            using (_databaseContextFactory.Create())
            {
                var stream = _eventStore.Get(message.FormulaId);
                var formula = new Formula(message.FormulaId);

                stream.Apply(formula);

                if (!formula.Removed)
                {
                    stream.AddEvent(formula.Remove());

                    _eventStore.Save(stream);
                }
            }

            context.ReplyOK();
        }
    }
}