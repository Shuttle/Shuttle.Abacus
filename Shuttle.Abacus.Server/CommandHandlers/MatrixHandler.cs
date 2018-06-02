using System;
using Shuttle.Abacus.Messages.v1;
using Shuttle.Core.Contract;
using Shuttle.Core.Data;
using Shuttle.Esb;
using Shuttle.Recall;

namespace Shuttle.Abacus.Server.CommandHandlers
{
    public class MatrixHandler :
        IMessageHandler<RegisterMatrixCommand>
    {
        private readonly IDatabaseContextFactory _databaseContextFactory;
        private readonly IEventStore _eventStore;
        private readonly IKeyStore _keyStore;

        public MatrixHandler(IDatabaseContextFactory databaseContextFactory, IEventStore eventStore, IKeyStore keyStore)
        {
            Guard.AgainstNull(databaseContextFactory, "databaseContextFactory");
            Guard.AgainstNull(eventStore, "eventStore");
            Guard.AgainstNull(keyStore, "keyStore");

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

                if (message.MatrixId.Equals(Guid.Empty))
                {
                    stream = _eventStore.CreateEventStream();
                    matrix = new Matrix(stream.Id);

                    _keyStore.Add(matrix.Id, key);
                }
                else
                {
                    stream = _eventStore.Get(message.MatrixId);
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
                    message.RowArgumentName,
                    message.ColumnArgumentName,
                    message.ValueType));

                foreach (var constraint in message.Constraints)
                {
                    stream.AddEvent(matrix.AddConstraint(constraint.Axis, constraint.SequenceNumber,
                        constraint.Comparison, constraint.Value));
                }

                foreach (var element in message.Elements)
                {
                    stream.AddEvent(matrix.AddElement(element.Row, element.Column, element.Value));
                }

                _eventStore.Save(stream);
            }
        }
    }
}