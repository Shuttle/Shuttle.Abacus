using System.Collections.Generic;
using Abacus.DTO;

namespace Abacus.Data
{
    public class ValueSourceTypeQuery : DataQuery, IValueSourceTypeQuery
    {
        private readonly IDataTableMapper<ValueSourceTypeDTO> valueSourceTypeDTOMapper;

        public ValueSourceTypeQuery(IDataTableMapper<ValueSourceTypeDTO> valueSourceTypeDTOMapper)
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
