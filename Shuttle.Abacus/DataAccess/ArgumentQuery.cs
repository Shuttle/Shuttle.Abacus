using System;
using System.Collections.Generic;
using System.Data;
using Shuttle.Abacus.Events.Argument.v1;
using Shuttle.Core.Contract;
using Shuttle.Core.Data;
using Shuttle.Recall;

namespace Shuttle.Abacus.DataAccess
{
    public class ArgumentQuery : IArgumentQuery
    {
        private readonly IArgumentQueryFactory _argumentQueryFactory;
        private readonly IDatabaseGateway _databaseGateway;

        public ArgumentQuery(IDatabaseGateway databaseGateway, IArgumentQueryFactory argumentQueryFactory)
        {
            Guard.AgainstNull(databaseGateway, nameof(databaseGateway));
            Guard.AgainstNull(argumentQueryFactory, nameof(argumentQueryFactory));

            _databaseGateway = databaseGateway;
            _argumentQueryFactory = argumentQueryFactory;
        }

        public IEnumerable<DataRow> Search(ArgumentSearchSpecification specification)
        {
            return _databaseGateway.GetRowsUsing(_argumentQueryFactory.Search(specification));
        }

        public DataRow Get(Guid id)
        {
            var row = _databaseGateway.GetSingleRowUsing(_argumentQueryFactory.Get(id));

            if (row == null)
            {
                throw RecordNotFoundException.For("Argument", id);
            }

            return row;
        }

        public IEnumerable<DataRow> Values(Guid id)
        {
            return _databaseGateway.GetRowsUsing(_argumentQueryFactory.Values(id));
        }

        public void Registered(PrimitiveEvent primitiveEvent, Registered registered)
        {
            _databaseGateway.ExecuteUsing(_argumentQueryFactory.Registered(primitiveEvent, registered));
        }

        public void Removed(PrimitiveEvent primitiveEvent, Removed removed)
        {
            _databaseGateway.ExecuteUsing(_argumentQueryFactory.Removed(primitiveEvent, removed));
        }

        public void Renamed(PrimitiveEvent primitiveEvent, Renamed renamed)
        {
            _databaseGateway.ExecuteUsing(_argumentQueryFactory.Renamed(primitiveEvent, renamed));
        }

        public void ValueAdded(PrimitiveEvent primitiveEvent, ValueAdded valueAdded)
        {
            _databaseGateway.ExecuteUsing(_argumentQueryFactory.ValueAdded(primitiveEvent, valueAdded));
        }

        public void ValueRemoved(PrimitiveEvent primitiveEvent, ValueRemoved valueRemoved)
        {
            _databaseGateway.ExecuteUsing(_argumentQueryFactory.ValueRemoved(primitiveEvent, valueRemoved));
        }
    }
}