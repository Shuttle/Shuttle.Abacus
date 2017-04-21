using System.Collections.Generic;
using System.Data;
using Abacus.DTO;

namespace Abacus.Data
{
    public class ValueTypeDTOMapper : IDataTableMapper<AnswerTypeDTO>
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
