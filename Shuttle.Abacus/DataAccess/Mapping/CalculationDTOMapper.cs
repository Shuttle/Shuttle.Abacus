using System.Collections.Generic;
using System.Data;
using Abacus.DTO;

namespace Abacus.Data
{
    public class CalculationDTOMapper : IDataTableMapper<CalculationDTO>
    {
        public IEnumerable<CalculationDTO> MapFrom(DataTable input)
        {
            var result = new List<CalculationDTO>();

            foreach (DataRow row in input.Rows)
            {
                result.Add(new CalculationDTO
                           {
                               CalculationId = CalculationColumns.Id.MapFrom(row),
                               Name = CalculationColumns.Name.MapFrom(row)
                           });
            }

            return result;
        }
    }
}
