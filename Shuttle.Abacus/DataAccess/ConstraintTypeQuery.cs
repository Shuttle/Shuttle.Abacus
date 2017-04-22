using System.Collections.Generic;
using Shuttle.Abacus.DTO;
using Shuttle.Core.Data;

namespace Shuttle.Abacus.DataAccess
{
    public class ConstraintTypeQuery :IConstraintTypeQuery
    {
        private readonly IDatabaseGateway _databaseGateway;
        private readonly IConstraintTypeQueryFactory _constraintTypeQueryFactory;
        private readonly IDataTableMapper<ConstraintTypeDTO> _constraintTypeDTOMapper;

        public ConstraintTypeQuery(IDatabaseGateway databaseGateway, IConstraintTypeQueryFactory constraintTypeQueryFactory, IDataTableMapper<ConstraintTypeDTO> constraintTypeDtoMapper)
        {
            _databaseGateway = databaseGateway;
            _constraintTypeQueryFactory = constraintTypeQueryFactory;
            _constraintTypeDTOMapper = constraintTypeDtoMapper;
        }
        
        public IEnumerable<ConstraintTypeDTO> All()
        {
            return _constraintTypeDTOMapper.MapFrom(_databaseGateway.GetDataTableFor(_constraintTypeQueryFactory.All()));
        }
    }
}
