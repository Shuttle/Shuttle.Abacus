using System.Collections.Generic;

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
            return QueryProcessor.Execute(OperationTypeQueryFactory.All());
        }

        public IEnumerable<OperationTypeDTO> AllDTOs()
        {
            return operationTypeDTOMapper.MapFrom(All().Table);
        }
    }
}
