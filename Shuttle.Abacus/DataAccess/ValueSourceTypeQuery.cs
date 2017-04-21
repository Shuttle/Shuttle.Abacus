using System.Collections.Generic;
using Shuttle.Abacus.DataAccess.Definitions;
using Shuttle.Abacus.DataAccess.Query;

namespace Shuttle.Abacus.DataAccess
{
    public class ValueSourceTypeQuery :IValueSourceTypeQuery
    {
        private readonly IDataRowMapper<ValueSourceTypeDTO> valueSourceTypeDTOMapper;

        public ValueSourceTypeQuery(IDataRowMapper<ValueSourceTypeDTO> valueSourceTypeDTOMapper)
        {
            this.valueSourceTypeDTOMapper = valueSourceTypeDTOMapper;
        }

        public IQueryResult All()
        {
            return QueryProcessor.Execute(ValueSourceTypeQueries.All());
        }

        public IEnumerable<ValueSourceTypeDTO> AllDTOs()
        {
            return valueSourceTypeDTOMapper.MapFrom(All().Table);
        }
    }
}
