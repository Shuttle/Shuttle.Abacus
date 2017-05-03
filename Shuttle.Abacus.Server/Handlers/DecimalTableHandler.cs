using System;
using Shuttle.Abacus.Domain;
using Shuttle.Abacus.Messages.v1;
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
        private readonly IDecimalTableRepository _decimalTableRepository;
        private readonly IDecimalValueRepository _decimalValueRepository;

        public DecimalTableHandler(IDatabaseContextFactory databaseContextFactory, IDecimalTableRepository decimalTableRepository,
            IDecimalValueRepository decimalValueRepository, IConstraintRepository constraintRepository)
        {
            Guard.AgainstNull(databaseContextFactory, "databaseContextFactory");
            Guard.AgainstNull(decimalTableRepository, "decimalTableRepository");
            Guard.AgainstNull(decimalValueRepository, "decimalValueRepository");
            Guard.AgainstNull(constraintRepository, "constraintRepository");

            _databaseContextFactory = databaseContextFactory;
            _decimalTableRepository = decimalTableRepository;
            _decimalValueRepository = decimalValueRepository;
            _constraintRepository = constraintRepository;
        }

        public void ProcessMessage(IHandlerContext<CreateDecimalTableCommand> context)
        {
            var message = context.Message;

            using (_databaseContextFactory.Create())
            {
                var table = new DecimalTable(Guid.NewGuid(), message.DecimalTableName,message.RowArgumentId,message.ColumnArgumentId);

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
                var table = new DecimalTable(message.DecimalTableId, message.DecimalTableName, message.RowArgumentId, message.ColumnArgumentId);

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