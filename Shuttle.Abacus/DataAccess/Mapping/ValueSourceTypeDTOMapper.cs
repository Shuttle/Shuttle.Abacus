using System.Collections.Generic;
using System.Data;
using Shuttle.Abacus.DTO;

namespace Shuttle.Abacus.DataAccess
{
    public class ValueSourceTypeDTOMapper : IDataTableMapper<ValueSourceTypeDTO>
    {
        public IEnumerable<ValueSourceTypeDTO> MapFrom(DataTable input)
        {
            var result = new List<ValueSourceTypeDTO>();

            foreach (DataRow row in input.Rows)
            {
                result.Add(new ValueSourceTypeDTO
                           {
                               Name = ValueSourceTypeColumns.Name.MapFrom(row), 
                               Text = ValueSourceTypeColumns.Text.MapFrom(row),
                               Type = ValueSourceTypeColumns.Type.MapFrom(row)
                           });
            }

            return result;
        }
    }
}
