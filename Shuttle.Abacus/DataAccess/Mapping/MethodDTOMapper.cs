using System.Collections.Generic;
using System.Data;
using Shuttle.Abacus.DTO;

namespace Shuttle.Abacus.DataAccess
{
    public class MethodDTOMapper : IDataTableMapper<MethodDTO>
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
