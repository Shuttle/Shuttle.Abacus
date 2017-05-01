using System;
using Shuttle.Abacus.Domain;
using Shuttle.Core.Data;
using Shuttle.Core.Infrastructure;

namespace Shuttle.Abacus.DataAccess
{
    public class MethodRepository : Repository<Method>, IMethodRepository
    {
        private readonly IDatabaseGateway _databaseGateway;
        private readonly IMethodQueryFactory _methodQueryFactory;
        private readonly ICalculationQuery _calculationQuery;
        private readonly ILimitQuery _limitQuery;
        private readonly IMethodTestQuery _methodTestQuery;

        public MethodRepository(IDatabaseGateway databaseGateway, IMethodQueryFactory methodQueryFactory,
            ICalculationQuery calculationQuery, ILimitQuery limitQuery, IMethodTestQuery methodTestQuery)
        {
            Guard.AgainstNull(databaseGateway, "databaseGateway");
            Guard.AgainstNull(methodQueryFactory, "methodQueryFactory");
            Guard.AgainstNull(calculationQuery, "calculationQuery");
            Guard.AgainstNull(limitQuery, "limitQuery");
            Guard.AgainstNull(methodTestQuery, "methodTestQuery");

            _databaseGateway = databaseGateway;
            _methodQueryFactory = methodQueryFactory;
            _calculationQuery = calculationQuery;
            _limitQuery = limitQuery;
            _methodTestQuery = methodTestQuery;
        }

        public override void Add(Method item)
        {
            _databaseGateway.ExecuteUsing(_methodQueryFactory.Add(item));
        }

        public override void Remove(Guid id)
        {
            _databaseGateway.ExecuteUsing(_methodQueryFactory.Remove(id));
        }

        public override Method Get(Guid id)
        {
            return Guarded.Entity(Get(_methodQueryFactory.Get(id)), id);
        }

        private Method Get(IQuery query)
        {
            var methodRow = _databaseGateway.GetSingleRowUsing(query);

            if (methodRow == null)
            {
                return null;
            }

            var result = new Method(MethodColumns.Id.MapFrom(methodRow), MethodColumns.Name.MapFrom(methodRow));

            _calculationQuery.PopulateOwner(result);
            _limitQuery.PopulateOwner(result);

            foreach (var row in _methodTestQuery.FetchForMethodId(result.Id))
            {
                result.AddMethodTest(new MethodTestItem(
                    MethodTestColumns.Id.MapFrom(row),
                    MethodTestColumns.ExpectedResult.MapFrom(row),
                    MethodTestColumns.Description.MapFrom(row)));
            }

            return result;
        }

        public void Save(Method item)
        {
            _databaseGateway.ExecuteUsing(_methodQueryFactory.Save(item));
        }

        public Method Get(string methodName)
        {
            var result = Get(_methodQueryFactory.Get(methodName));

            if (result == null)
            {
                throw new MissingEntityException(string.Format("Could not find entity 'Method' with name of '{0}'.", methodName));
            }

            return result;
        }
    }
}