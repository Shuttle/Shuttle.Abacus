using System;
using System.Collections.Generic;
using Shuttle.Abacus.Domain;
using Shuttle.Core.Data;

namespace Shuttle.Abacus.DataAccess
{
    public class DecimalValueRepository : Repository<DecimalValue>, IDecimalValueRepository
    {
        private readonly IDatabaseGateway gateway;
        private readonly IDataRepository<DecimalValue> repository;

        public DecimalValueRepository(IDataRepository<DecimalValue> repository, IDatabaseGateway gateway)
        {
            this.repository = repository;
            this.gateway = gateway;
        }

        public override void Add(DecimalValue item)
        {
            throw new NotImplementedException();
        }

        public override void Remove(DecimalValue item)
        {
            throw new NotImplementedException();
        }

        public override DecimalValue Get(Guid id)
        {
            throw new NotImplementedException();
        }

        public void Add(DecimalTable decimalTable, DecimalValue decimalValue)
        {
            gateway.ExecuteUsing(DecimalValueQueryFactory.Add(decimalTable, decimalValue));
        }

        public IEnumerable<DecimalValue> AllForDecimalTable(DecimalTable decimalTable)
        {
            return repository.FetchAllUsing(DecimalValueQueryFactory.AllForDecimalTable(decimalTable.Id));
        }

        public void RemoveAllForDecimalTable(Guid decimalTableId)
        {
            gateway.ExecuteUsing(DecimalValueQueryFactory.RemoveConstraintsForDecimalTable(decimalTableId));
            gateway.ExecuteUsing(DecimalValueQueryFactory.RemoveAllForDecimalTable(decimalTableId));
        }
    }
}