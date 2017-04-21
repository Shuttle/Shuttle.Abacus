using System.Collections.Generic;
using Shuttle.Abacus.DataAccess.Definitions;
using Shuttle.Abacus.DataAccess.Query;

namespace Shuttle.Abacus.DataAccess
{
    public class OperationTypeQuery :IOperationTypeQuery
    {
        private readonly IDataRowMapper<OperationTypeDTO> operationTypeDTOMapper;

        public OperationTypeQuery(IDataRowMapper<OperationTypeDTO> operationTypeDTOMapper)
        {
            this.operationTypeDTOMapper = operationTypeDTOMapper;
        }

        public IQueryResult All()
        {
            return QueryProcessor.Execute(OperationTypeQueries.All());
        }

        public IEnumerable<OperationTypeDTO> AllDTOs()
        {
            return operationTypeDTOMapper.MapFrom(All().Table);
        }
    }
}
