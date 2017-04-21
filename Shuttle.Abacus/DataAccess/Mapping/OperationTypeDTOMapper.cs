using System.Collections.Generic;
using System.Data;
using Shuttle.Abacus.DataAccess.Definitions;

namespace Shuttle.Abacus.DataAccess.Mapping
{
    public class OperationTypeDTOMapper : IDataRowMapper<OperationTypeDTO>
    {
        public IEnumerable<OperationTypeDTO> MapFrom(DataTable input)
        {
            var result = new List<OperationTypeDTO>();

            foreach (DataRow row in input.Rows)
            {
                result.Add(new OperationTypeDTO
                           {
                               Name = OperationTypeColumns.Name.MapFrom(row), 
                               Text = OperationTypeColumns.Text.MapFrom(row)
                           });
            }

            return result;
        }
    }
}
