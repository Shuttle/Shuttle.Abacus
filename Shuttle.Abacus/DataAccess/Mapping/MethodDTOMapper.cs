using System.Collections.Generic;
using System.Data;
using Shuttle.Abacus.DataAccess.Definitions;

namespace Shuttle.Abacus.DataAccess.Mapping
{
    public class MethodDTOMapper : IDataRowMapper<MethodDTO>
    {
        public IEnumerable<MethodDTO> MapFrom(DataTable input)
        {
            var result = new List<MethodDTO>();

            foreach (DataRow row in input.Rows)
            {
                result.Add(new MethodDTO
                           {
                               MethodId = MethodColumns.Id.MapFrom(row),
                               MethodName = MethodColumns.Name.MapFrom(row)
                           });
            }

            return result;
        }
    }
}
