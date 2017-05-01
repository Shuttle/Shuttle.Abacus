using System;
using Shuttle.Abacus.Domain;
using Shuttle.Core.Data;
using Shuttle.Core.Infrastructure;
using Shuttle.Esb;

namespace Shuttle.Abacus.Server.Handlers
{
    public class DecimalTableHandler :
        IMessageHandler<CreateDecimalTableCommand>,
        IMessageHandler<UpdateDecimalTableCommand>
    {
        private readonly IConstraintRepository _constraintRepository;
        private readonly IDatabaseContextFactory _databaseContextFactory;
        private readonly IDecimalTableFactory _decimalTableFactory;
        private readonly IDecimalTableRepository _decimalTableRepository;
        private readonly IDecimalValueRepository _decimalValueRepository;

        public DecimalTableHandler(IDatabaseContextFactory databaseContextFactory,
            IDecimalTableFactory decimalTableFactory, IDecimalTableRepository decimalTableRepository,
            IDecimalValueRepository decimalValueRepository, IConstraintRepository constraintRepository)
        {
            Guard.AgainstNull(databaseContextFactory, "databaseContextFactory");
            Guard.AgainstNull(decimalTableFactory, "decimalTableFactory");
            Guard.AgainstNull(decimalTableRepository, "decimalTableRepository");
            Guard.AgainstNull(decimalValueRepository, "decimalValueRepository");
            Guard.AgainstNull(constraintRepository, "constraintRepository");

            _databaseContextFactory = databaseContextFactory;
            _decimalTableFactory = decimalTableFactory;
            _decimalTableRepository = decimalTableRepository;
            _decimalValueRepository = decimalValueRepository;
            _constraintRepository = constraintRepository;
        }

        public void ProcessMessage(IHandlerContext<CreateDecimalTableCommand> context)
        {
            var message = context.Message;

            using (_databaseContextFactory.Create())
            {
                var table = _decimalTableFactory.Create(Guid.NewGuid(), message);

                _decimalTableRepository.Add(table);

                foreach (var value in table.DecimalValues)
                {
                    _decimalValueRepository.Add(table, value);
                }
            }
        }

        public void ProcessMessage(IHandlerContext<UpdateDecimalTableCommand> context)
        {
            var message = context.Message;

            using (_databaseContextFactory.Create())
            {
                var table = _decimalTableFactory.Create(message.DecimalTableId, message);

                _decimalValueRepository.RemoveAllForDecimalTable(message.DecimalTableId);

                _decimalTableRepository.Save(table);

                foreach (var value in table.DecimalValues)
                {
                    _decimalValueRepository.Add(table, value);
                }
            }
        }
    }
}