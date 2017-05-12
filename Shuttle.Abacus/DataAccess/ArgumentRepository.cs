using System;
using Shuttle.Abacus.Domain;
using Shuttle.Abacus.Infrastructure;
using Shuttle.Core.Data;
using Shuttle.Core.Infrastructure;

namespace Shuttle.Abacus.DataAccess
{
    public class ArgumentRepository : Repository<Argument>, IArgumentRepository
    {
        private readonly IDatabaseGateway _databaseGateway;
        private readonly IArgumentQueryFactory _queryFactory;

        public ArgumentRepository(IDatabaseGateway databaseGateway, IArgumentQueryFactory queryFactory)
        {
            Guard.AgainstNull(databaseGateway, "databaseGateway");
            Guard.AgainstNull(queryFactory, "queryFactory");

            _databaseGateway = databaseGateway;
            _queryFactory = queryFactory;
        }

        public override void Add(Argument item)
        {
            _databaseGateway.ExecuteUsing(_queryFactory.Add(item));
        }

        public override void Remove(Guid id)
        {
            _databaseGateway.ExecuteUsing(_queryFactory.Remove(id));
        }

        public override Argument Get(Guid id)
        {
            return Guarded.Entity(Find(id), id);
        }

        public void Save(Argument argument)
        {
            _databaseGateway.ExecuteUsing(_queryFactory.Save(argument));

            _databaseGateway.ExecuteUsing(_queryFactory.RemoveValues(argument));

            argument.Values.ForEach(
                mapping => _databaseGateway.ExecuteUsing(_queryFactory.SaveValue(argument, mapping)));
        }

        public Argument Find(Guid id)
        {
            var row = _databaseGateway.GetSingleRowUsing(_queryFactory.Get(id));

            if (row == null)
            {
                return null;
            }

            var result = new Argument(id);

            foreach (var valueRow in _databaseGateway.GetRowsUsing(_queryFactory.GetValues(id)))
            {
                result.AddValue(ArgumentColumns.ValueColumns.Value.MapFrom(valueRow));
            }

            return result;
        }
    }
}