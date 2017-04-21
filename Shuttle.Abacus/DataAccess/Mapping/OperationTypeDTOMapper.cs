using System.Collections.Generic;
using System.Data;
using Abacus.DTO;

namespace Abacus.Data
{
    public class OperationTypeDTOMapper : IDataTableMapper<OperationTypeDTO>
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
