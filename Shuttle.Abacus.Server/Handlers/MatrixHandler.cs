using System;
using Shuttle.Abacus.Domain;
using Shuttle.Abacus.Messages.v1;
using Shuttle.Core.Data;
using Shuttle.Core.Infrastructure;
using Shuttle.Esb;

namespace Shuttle.Abacus.Server.Handlers
{
    public class MatrixHandler :
        IMessageHandler<CreateMatrixCommand>,
        IMessageHandler<UpdateMatrixCommand>
    {
        private readonly IConstraintRepository _constraintRepository;
        private readonly IDatabaseContextFactory _databaseContextFactory;
        private readonly IMatrixRepository _matrixRepository;
        private readonly IMatrixElementRepository _matrixElementRepository;

        public MatrixHandler(IDatabaseContextFactory databaseContextFactory, IMatrixRepository matrixRepository,
            IMatrixElementRepository matrixElementRepository, IConstraintRepository constraintRepository)
        {
            Guard.AgainstNull(databaseContextFactory, "databaseContextFactory");
            Guard.AgainstNull(matrixRepository, "matrixRepository");
            Guard.AgainstNull(matrixElementRepository, "matrixElementRepository");
            Guard.AgainstNull(constraintRepository, "constraintRepository");

            _databaseContextFactory = databaseContextFactory;
            _matrixRepository = matrixRepository;
            _matrixElementRepository = matrixElementRepository;
            _constraintRepository = constraintRepository;
        }

        public void ProcessMessage(IHandlerContext<CreateMatrixCommand> context)
        {
            var message = context.Message;

            using (_databaseContextFactory.Create())
            {
                var table = new Matrix(Guid.NewGuid(), message.DecimalTableName,message.RowArgumentId,message.ColumnArgumentId);

                _matrixRepository.Add(table);

                foreach (var value in table.DecimalValues)
                {
                    _matrixElementRepository.Add(table, value);
                }
            }
        }

        public void ProcessMessage(IHandlerContext<UpdateMatrixCommand> context)
        {
            var message = context.Message;

            using (_databaseContextFactory.Create())
            {
                var table = new Matrix(message.DecimalTableId, message.DecimalTableName, message.RowArgumentId, message.ColumnArgumentId);

                _matrixElementRepository.RemoveAllForDecimalTable(message.DecimalTableId);

                _matrixRepository.Save(table);

                foreach (var value in table.DecimalValues)
                {
                    _matrixElementRepository.Add(table, value);
                }
            }
        }
    }
}