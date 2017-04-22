using System.Collections.Generic;

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
            return QueryProcessor.Execute(ValueSourceTypeQueryFactory.All());
        }

        public IEnumerable<ValueSourceTypeDTO> AllDTOs()
        {
            return valueSourceTypeDTOMapper.MapFrom(All().Table);
        }
    }
}
