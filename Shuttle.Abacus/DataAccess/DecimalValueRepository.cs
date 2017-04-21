using System;
using System.Collections.Generic;
using Shuttle.Abacus.DataAccess.Definitions;
using Shuttle.Abacus.Domain;
using Shuttle.Core.Data;

namespace Shuttle.Abacus.DataAccess
{
    public class DecimalValueRepository : Repository<DecimalValue>, IDecimalValueRepository
    {
        private readonly IDatabaseGateway gateway;
        private readonly IDataRowRepository<DecimalValue> repository;

        public DecimalValueRepository(IDataRowRepository<DecimalValue> repository, IDatabaseGateway gateway)
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
            gateway.ExecuteUsing(DecimalValueTableAccess.Add(decimalTable, decimalValue));
        }

        public IEnumerable<DecimalValue> AllForDecimalTable(DecimalTable decimalTable)
        {
            return repository.FetchAllUsing(DecimalValueTableAccess.AllForDecimalTable(decimalTable.Id));
        }

        public void RemoveAllForDecimalTable(Guid decimalTableId)
        {
            gateway.ExecuteUsing(DecimalValueTableAccess.RemoveConstraintsForDecimalTable(decimalTableId));
            gateway.ExecuteUsing(DecimalValueTableAccess.RemoveAllForDecimalTable(decimalTableId));
        }
    }
}