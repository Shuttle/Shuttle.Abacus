using System.Collections.Generic;
using System.Data;
using Shuttle.Abacus.DTO;
using Shuttle.Core.Data;

namespace Shuttle.Abacus.DataAccess
{
    public class OperationTypeQuery :IOperationTypeQuery
    {
        private readonly IDatabaseGateway _databaseGateway;
        private readonly IOperationTypeQueryFactory _operationTypeQueryFactory;
        private readonly IDataTableMapper<OperationTypeDTO> _operationTypeDTOMapper;

        public OperationTypeQuery(IDatabaseGateway databaseGateway, IOperationTypeQueryFactory operationTypeQueryFactory, IDataTableMapper<OperationTypeDTO> operationTypeDTOMapper)
        {
            _databaseGateway = databaseGateway;
            _operationTypeQueryFactory = operationTypeQueryFactory;
            _operationTypeDTOMapper = operationTypeDTOMapper;
        }

        public IEnumerable<DataRow> All()
        {
            return _databaseGateway.GetRowsUsing(_operationTypeQueryFactory.All());
        }

        public IEnumerable<OperationTypeDTO> AllDTOs()
        {
            return _operationTypeDTOMapper.MapFrom(All().CopyToDataTable());
        }
    }
}
