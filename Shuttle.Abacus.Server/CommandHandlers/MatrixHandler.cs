using System;
using Shuttle.Abacus.Messages.v1;
using Shuttle.Core.Contract;
using Shuttle.Core.Data;
using Shuttle.Esb;
using Shuttle.Recall;

namespace Shuttle.Abacus.Server.CommandHandlers
{
    public class MatrixHandler :
        IMessageHandler<RegisterMatrixCommand>,
        IMessageHandler<RegisterMatrixConstraintCommand>
    {
        private readonly IDatabaseContextFactory _databaseContextFactory;
        private readonly IEventStore _eventStore;
        private readonly IKeyStore _keyStore;

        public MatrixHandler(IDatabaseContextFactory databaseContextFactory, IEventStore eventStore, IKeyStore keyStore)
        {
            Guard.AgainstNull(databaseContextFactory, nameof(databaseContextFactory));
            Guard.AgainstNull(eventStore, nameof(eventStore));
            Guard.AgainstNull(keyStore, nameof(keyStore));

            _databaseContextFactory = databaseContextFactory;
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

                if (message.Id.Equals(Guid.Empty))
                {
                    stream = _eventStore.CreateEventStream();
                    matrix = new Matrix(stream.Id);

                    _keyStore.Add(matrix.Id, key);
                }
                else
                {
                    stream = _eventStore.Get(message.Id);
                    matrix = new Matrix(stream.Id);

                    stream.Apply(matrix);

                    if (!matrix.IsNamed(message.Name) && !_keyStore.Contains(key))
                    {
                        _keyStore.Remove(Matrix.Key(message.Name));
                        _keyStore.Add(matrix.Id, key);
                    }
                }

                stream.AddEvent(matrix.Register(
                    message.Name,
                    message.RowArgumentId,
                    message.ColumnArgumentId,
                    message.DataTypeName));

                _eventStore.Save(stream);
            }
        }

        public void ProcessMessage(IHandlerContext<RegisterMatrixConstraintCommand> context)
        {
            var message = context.Message;

            using (_databaseContextFactory.Create())
            {
                var stream = _eventStore.Get(message.MatrixId);

                if (stream.IsEmpty)
                {
                    return;
                }

                var matrix = new Matrix(message.MatrixId);

                stream.Apply(matrix);

                if (matrix.Removed)
                {
                    return;
                }

                stream.AddEvent(matrix.AddConstraint(message.Id, message.Axis, message.Index, message.Comparison, message.Value));

                _eventStore.Save(stream);
            }
        }
    }
}