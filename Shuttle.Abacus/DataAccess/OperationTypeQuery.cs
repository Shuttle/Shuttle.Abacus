using System.Collections.Generic;
using Abacus.DTO;

namespace Abacus.Data
{
    public class OperationTypeQuery : DataQuery, IOperationTypeQuery
    {
        private readonly IDataTableMapper<OperationTypeDTO> operationTypeDTOMapper;

        public OperationTypeQuery(IDataTableMapper<OperationTypeDTO> operationTypeDTOMapper)
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
