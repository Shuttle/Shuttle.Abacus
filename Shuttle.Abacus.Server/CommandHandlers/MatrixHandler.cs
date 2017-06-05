using System;
using System.Dynamic;
using Shuttle.Abacus.DataAccess;
using Shuttle.Abacus.Domain;
using Shuttle.Abacus.Messages.v1;
using Shuttle.Core.Data;
using Shuttle.Core.Infrastructure;
using Shuttle.Esb;
using Shuttle.Recall;

namespace Shuttle.Abacus.Server.CommandHandlers
{
    public class MatrixHandler :
        IMessageHandler<RegisterMatrixCommand>
    {
        private readonly IDatabaseContextFactory _databaseContextFactory;
        private readonly IMatrixRepository _matrixRepository;
        private readonly IMatrixQuery _matrixQuery;
        private readonly IEventStore _eventStore;
        private readonly IKeyStore _keyStore;

        public MatrixHandler(IDatabaseContextFactory databaseContextFactory, IMatrixRepository matrixRepository, IMatrixQuery matrixQuery, IEventStore eventStore, IKeyStore keyStore)
        {
            Guard.AgainstNull(databaseContextFactory, "databaseContextFactory");
            Guard.AgainstNull(matrixRepository, "matrixRepository");
            Guard.AgainstNull(matrixQuery, "matrixQuery");
            Guard.AgainstNull(eventStore, "eventStore");
            Guard.AgainstNull(keyStore, "keyStore");

            _databaseContextFactory = databaseContextFactory;
            _matrixRepository = matrixRepository;
            _matrixQuery = matrixQuery;
            _eventStore = eventStore;
            _keyStore = keyStore;
        }

        public void ProcessMessage(IHandlerContext<RegisterMatrixCommand> context)
        {
            var message = context.Message;

            using (_databaseContextFactory.Create())
            {
                Matrix matrix;

                var key = Matrix.Key(message.Name);
                EventStream stream;

                if (message.MatrixId.Equals(Guid.Empty))
                {
                    stream = _eventStore.Get(message.MatrixId);
                    matrix = _matrixRepository.Get(message.MatrixId);

                    if (!matrix.IsNamed(message.Name) && !_keyStore.Contains(key))
                    {
                        _keyStore.Remove(Matrix.Key(message.Name));
                        _keyStore.Add(message.MatrixId, key);
                    }
                }
                else
                {
                    stream = _eventStore.CreateEventStream();
                    matrix = new Matrix(stream.Id);
                }

                stream.AddEvent(matrix.Register(
                    message.Name,
                    message.RowArgumentName,
                    message.ColumnArgumentName,
                    message.ValueType));

                foreach (var constraint in message.Constraints)
                {
                    stream.AddEvent(matrix.AddConstraint(constraint.Axis, constraint.SequenceNumber,
                        constraint.Comparison, constraint.Value));
                }
            }
        }
    }
}