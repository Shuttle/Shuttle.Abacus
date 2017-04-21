using System.Collections.Generic;
using System.Data;
using Shuttle.Abacus.DataAccess.Definitions;

namespace Shuttle.Abacus.DataAccess.Mapping
{
    public class ConstraintTypeDTOMapper : IDataRowMapper<ConstraintTypeDTO>
    {
        public IEnumerable<ConstraintTypeDTO> MapFrom(DataTable input)
        {
            var result = new List<ConstraintTypeDTO>();

            foreach (DataRow row in input.Rows)
            {
                result.Add(new ConstraintTypeDTO
                           {
                               Name = ConstraintTypeColumns.Name.MapFrom(row), 
                               Text = ConstraintTypeColumns.Text.MapFrom(row),
                               EnabledForAnswerCatalog = ConstraintTypeColumns.EnabledForRestrictedAnswers.MapFrom(row)
                           });
            }

            return result;
        }
    }
}
