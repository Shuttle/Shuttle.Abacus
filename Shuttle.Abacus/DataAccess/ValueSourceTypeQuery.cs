using System.Collections.Generic;
using System.Data;
using Shuttle.Abacus.DTO;
using Shuttle.Core.Data;

namespace Shuttle.Abacus.DataAccess
{
    public class ValueSourceTypeQuery : IValueSourceTypeQuery
    {
        private readonly IDatabaseGateway _databaseGateway;
        private readonly IDataTableMapper<ValueSourceTypeDTO> _valueSourceTypeDTOMapper;
        private readonly IValueSourceTypeQueryFactory _valueSourceTypeQueryFactory;

        public ValueSourceTypeQuery(IDatabaseGateway databaseGateway,
            IValueSourceTypeQueryFactory valueSourceTypeQueryFactory,
            IDataTableMapper<ValueSourceTypeDTO> valueSourceTypeDTOMapper)
        {
            _databaseGateway = databaseGateway;
            _valueSourceTypeQueryFactory = valueSourceTypeQueryFactory;
            _valueSourceTypeDTOMapper = valueSourceTypeDTOMapper;
        }

        public IEnumerable<DataRow> All()
        {
            return _databaseGateway.GetRowsUsing(_valueSourceTypeQueryFactory.All());
        }

        public IEnumerable<ValueSourceTypeDTO> AllDTOs()
        {
            return _valueSourceTypeDTOMapper.MapFrom(All().CopyToDataTable());
        }
    }
}