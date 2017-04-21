using System.Collections.Generic;
using System.Data;
using Shuttle.Abacus.DataAccess.Definitions;

namespace Shuttle.Abacus.DataAccess.Mapping
{
    public class ValueTypeDTOMapper : IDataRowMapper<AnswerTypeDTO>
    {
        public IEnumerable<AnswerTypeDTO> MapFrom(DataTable input)
        {
            var result = new List<AnswerTypeDTO>();

            foreach (DataRow row in input.Rows)
            {
                result.Add(new AnswerTypeDTO
                           {
                               Name = AnswerTypeColumns.Name.MapFrom(row), 
                               Text = AnswerTypeColumns.Text.MapFrom(row)
                           });
            }

            return result;
        }
    }
}
