using System.Collections.Generic;
using System.Data;
using Shuttle.Abacus.DataAccess.Definitions;

namespace Shuttle.Abacus.DataAccess.Mapping
{
    public class CalculationDTOMapper : IDataRowMapper<CalculationDTO>
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
