using System;
using System.Collections.Generic;
using System.Data;
using Abacus.DTO;

namespace Abacus.Data
{
    public class DecimalTableQuery : DataQuery, IDecimalTableQuery
    {
        private readonly IDatabaseGateway gateway;
        private readonly IDataTableMapper<DecimalTableDTO> decimalTableDTOMapper;


        public DecimalTableQuery(IDatabaseGateway gateway, IDataTableMapper<DecimalTableDTO> decimalTableDTOMapper)
        {
            this.gateway = gateway;
            this.decimalTableDTOMapper = decimalTableDTOMapper;
        }

        public IQueryResult All()
        {
            return QueryProcessor.Execute(DecimalTableQueries.All());
        }

        public IEnumerable<DecimalTableDTO> AllDTOs()
        {
            return decimalTableDTOMapper.MapFrom(All().Table);
        }

        public IQueryResult Get(Guid id)
        {
            return QueryProcessor.Execute(DecimalTableQueries.Get(id));
        }

        public DataTable ConstrainedDecimalValues(Guid id)
        {
            using (UnitOfWorkProvider.Create())
            {
                return gateway.GetDataTableFor(DecimalTableQueries.ConstrainedDecimalValues(id));
            }
        }

        public IQueryResult Name(Guid id)
        {
            return QueryProcessor.Execute(DecimalTableQueries.Name(id));
        }

        public DataTable QueryDecimalTable(Guid decimalTableId)
        {
            using (UnitOfWorkProvider.Create())
            {
                return gateway.GetDataTableFor(DecimalTableQueries.DecimalTableReport(decimalTableId));
            }
        }
    }
}
