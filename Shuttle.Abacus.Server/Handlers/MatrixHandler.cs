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
        private readonly IDatabaseContextFactory _databaseContextFactory;
        private readonly IMatrixElementRepository _matrixElementRepository;
        private readonly IMatrixRepository _matrixRepository;

        public MatrixHandler(IDatabaseContextFactory databaseContextFactory, IMatrixRepository matrixRepository,
            IMatrixElementRepository matrixElementRepository)
        {
            Guard.AgainstNull(databaseContextFactory, "databaseContextFactory");
            Guard.AgainstNull(matrixRepository, "matrixRepository");
            Guard.AgainstNull(matrixElementRepository, "matrixElementRepository");

            _databaseContextFactory = databaseContextFactory;
            _matrixRepository = matrixRepository;
            _matrixElementRepository = matrixElementRepository;
        }

        public void ProcessMessage(IHandlerContext<CreateMatrixCommand> context)
        {
            var message = context.Message;

            using (_databaseContextFactory.Create())
            {
                var table = new Matrix(Guid.NewGuid(), message.DecimalTableName, message.RowArgumentName,
                    message.ColumnArgumentName);

                _matrixRepository.Add(table);

                foreach (var value in table.Elements)
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
                var table = new Matrix(message.MatrixId, message.DecimalTableName, message.RowArgumentName,
                    message.ColumnArgumentName);

                _matrixElementRepository.RemoveAllForDecimalTable(message.MatrixId);

                _matrixRepository.Save(table);

                foreach (var value in table.Elements)
                {
                    _matrixElementRepository.Add(table, value);
                }
            }
        }
    }
}