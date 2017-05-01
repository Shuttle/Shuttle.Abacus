using System;
using Shuttle.Abacus.ApplicationService;
using Shuttle.Abacus.Domain;
using Shuttle.Abacus.DTO;
using Shuttle.Abacus.Infrastructure;
using Shuttle.Core.Data;
using Shuttle.Core.Infrastructure;
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
        private readonly ICalculationRepository _calculationRepository;
        private readonly IFactoryProvider<IConstraintFactory> _constraintFactoryProvider;
        private readonly ILimitRepository _limitRepository;
        private readonly IDatabaseContextFactory _databaseContextFactory;
        private readonly IMethodRepository _methodRepository;
        private readonly ITaskFactory _taskFactory;

        public CalculationHandler(
            IDatabaseContextFactory databaseContextFactory,
            IMethodRepository methodRepository,
            ICalculationRepository calculationRepository,
            IFactoryProvider<IConstraintFactory> constraintFactoryProvider,
            IFactoryProvider<IArgumentAnswerFactory> argumentAnswerFactoryProvider,
            ILimitRepository limitRepository,
            ITaskFactory taskFactory)
        {
            Guard.AgainstNull(databaseContextFactory, "databaseContextFactory");
            Guard.AgainstNull(methodRepository, "methodRepository");
            Guard.AgainstNull(calculationRepository, "calculationRepository");
            Guard.AgainstNull(constraintFactoryProvider, "constraintFactoryProvider");
            Guard.AgainstNull(argumentAnswerFactoryProvider, "argumentAnswerFactoryProvider");
            Guard.AgainstNull(limitRepository, "limitRepository");
            Guard.AgainstNull(taskFactory, "taskFactory");

            _databaseContextFactory = databaseContextFactory;
            _methodRepository = methodRepository;
            _limitRepository = limitRepository;
            _taskFactory = taskFactory;
            _calculationRepository = calculationRepository;
            _constraintFactoryProvider = constraintFactoryProvider;
            _argumentAnswerFactoryProvider = argumentAnswerFactoryProvider;
        }

        public void ProcessMessage(IHandlerContext<ChangeCalculationCommand> context)
        {
            var message = context.Message;

            using (_databaseContextFactory.Create())
            {
                var method = _methodRepository.Get(message.MethodId);

                method.ProcessCommand(message);

                var calculation = method.CalculationCollection.Get(message.CalculationId);

                calculation.ClearGraphNodeArguments();

                //TODO
                //message.GraphNodeArguments.ForEach(
                //    item => calculation.AddGraphNodeArgument(_argumentDTOMapper.MapFrom(item.DataRow), item.Format));

                _calculationRepository.Save(calculation);
            }
        }

        public void ProcessMessage(IHandlerContext<ChangeCalculationOrderCommand> context)
        {
            var message = context.Message;

            using (_databaseContextFactory.Create())
            {
                var method = _methodRepository.Get(message.MethodId);

                method.ProcessCommand(message);

                _calculationRepository.SaveOrdered(method);
            }
        }

        public void ProcessMessage(IHandlerContext<CreateCalculationCommand> context)
        {
            var message = context.Message;

            using (_databaseContextFactory.Create())
            {
                throw new NotImplementedException();
                //_calculationRepository.Add();
                //var calculation = _calculationFactoryProvider.Get(message.Type).Create(message.Name, message.Required);

                ////TODO
                //message.GraphNodeArguments.ForEach(
                //    item => calculation.AddGraphNodeArgument(_argumentDTOMapper.MapFrom(item.DataRow), item.Format));

                //var method = _methodRepository.Get(message.MethodId);

                //method.ProcessCommand(message, calculation);
            }
        }

        public void ProcessMessage(IHandlerContext<DeleteCalculationCommand> context)
        {
            var message = context.Message;

            using (_databaseContextFactory.Create())
            {
                var method = _methodRepository.Get(message.MethodId);
                var calculation = _calculationRepository.Get(message.CalculationId);

                method.ProcessCommand(message);

                foreach (var limit in method.Limits)
                {
                    _limitRepository.Remove(limit.Id);
                }

                _calculationRepository.Remove(calculation.Id);
            }
        }

        public void ProcessMessage(IHandlerContext<GrabCalculationsCommand> context)
        {
            var message = context.Message;

            using (_databaseContextFactory.Create())
            {
                var method = _methodRepository.Get(message.MethodId);

                method.ProcessCommand(message);

                _calculationRepository.SaveOwnershipGraph(method);
            }
        }

        public void ProcessMessage(IHandlerContext<SetCalculationConstraintsCommand> context)
        {
            var message = context.Message;

            throw new NotImplementedException();

            //using (_databaseContextFactory.Create())
            //{
            //    _taskFactory.Create<ISetCalculationConstraintsTask>().Execute(
            //        _calculationRepository
            //            .Get(message.CalculationId)
            //            .ProcessCommand(
            //                message,
            //                _constraintFactoryProvider,
            //                _argumentAnswerFactoryProvider, _argumentDTOMapper));
            //}
        }
    }
}