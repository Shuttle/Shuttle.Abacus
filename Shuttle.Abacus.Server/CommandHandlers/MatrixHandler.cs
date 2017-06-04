using System;
using Shuttle.Abacus.DataAccess;
using Shuttle.Abacus.Domain;
using Shuttle.Abacus.Messages.v1;
using Shuttle.Core.Data;
using Shuttle.Core.Infrastructure;
using Shuttle.Esb;

namespace Shuttle.Abacus.Server.CommandHandlers
{
    public class MatrixHandler :
        IMessageHandler<RegisterMatrixCommand>
    {
        private readonly IDatabaseContextFactory _databaseContextFactory;
        private readonly IMatrixRepository _matrixRepository;
        private readonly IMatrixQuery _matrixQuery;

        public MatrixHandler(IDatabaseContextFactory databaseContextFactory, IMatrixRepository matrixRepository, IMatrixQuery matrixQuery)
        {
            Guard.AgainstNull(databaseContextFactory, "databaseContextFactory");
            Guard.AgainstNull(matrixRepository, "matrixRepository");
            Guard.AgainstNull(matrixQuery, "matrixQuery");

            _databaseContextFactory = databaseContextFactory;
            _matrixRepository = matrixRepository;
            _matrixQuery = matrixQuery;
        }

        public void ProcessMessage(IHandlerContext<RegisterMatrixCommand> context)
        {
            var message = context.Message;

            using (_databaseContextFactory.Create())
            {
                if (Guid.Empty.Equals(message.MatrixId) && _matrixQuery.Contains(message.MatrixName))
                {
                    return;
                }

                var table = new Matrix(Guid.NewGuid(), message.MatrixName, message.RowArgumentName,
                    message.ColumnArgumentName);

                _matrixRepository.Add(table);

                foreach (var value in table.Elements)
                {
                    _matrixElementRepository.Add(table, value);
                }
            }
        }
    }
}