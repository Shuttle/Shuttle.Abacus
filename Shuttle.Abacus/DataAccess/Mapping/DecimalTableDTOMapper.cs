using System.Collections.Generic;
using System.Data;
using Shuttle.Abacus.DataAccess.Definitions;

namespace Shuttle.Abacus.DataAccess.Mapping
{
    public class DecimalTableDTOMapper : IDataRowMapper<DecimalTableDTO>
    {
        public IEnumerable<DecimalTableDTO> MapFrom(DataTable input)
        {
            var result = new List<DecimalTableDTO>();

            foreach (DataRow row in input.Rows)
            {
                result.Add(new DecimalTableDTO
                           {
                               DecimalTableId = DecimalTableColumns.Id.MapFrom(row), 
                               Name = DecimalTableColumns.Name.MapFrom(row)
                           });
            }

            return result;
        }
    }
}
