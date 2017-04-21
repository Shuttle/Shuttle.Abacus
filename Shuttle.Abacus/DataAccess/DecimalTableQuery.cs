using System;
using System.Collections.Generic;
using System.Data;
using Shuttle.Abacus.DataAccess.Definitions;
using Shuttle.Abacus.DataAccess.Query;
using Shuttle.Abacus.DTO;
using Shuttle.Core.Data;

namespace Shuttle.Abacus.DataAccess
{
    public class DecimalTableQuery :IDecimalTableQuery
    {
        private readonly IDatabaseGateway gateway;
        private readonly IDataRowMapper<DecimalTableDTO> decimalTableDTOMapper;


        public DecimalTableQuery(IDatabaseGateway gateway, IDataRowMapper<DecimalTableDTO> decimalTableDTOMapper)
        {
            this.gateway = gateway;
            this.decimalTableDTOMapper = decimalTableDTOMapper;
        }

        public IQueryResult All()
        {
            return QueryProcessor.Execute(DecimalTableQueryFactory.All());
        }

        public IEnumerable<DecimalTableDTO> AllDTOs()
        {
            return decimalTableDTOMapper.MapFrom(All().Table);
        }

        public IQueryResult Get(Guid id)
        {
            return QueryProcessor.Execute(DecimalTableQueryFactory.Get(id));
        }

        public DataTable ConstrainedDecimalValues(Guid id)
        {
            using (UnitOfWorkProvider.Create())
            {
                return gateway.GetDataTableFor(DecimalTableQueryFactory.ConstrainedDecimalValues(id));
            }
        }

        public IQueryResult Name(Guid id)
        {
            return QueryProcessor.Execute(DecimalTableQueryFactory.Name(id));
        }

        public DataTable QueryDecimalTable(Guid decimalTableId)
        {
            using (UnitOfWorkProvider.Create())
            {
                return gateway.GetDataTableFor(DecimalTableQueryFactory.DecimalTableReport(decimalTableId));
            }
        }
    }
}
