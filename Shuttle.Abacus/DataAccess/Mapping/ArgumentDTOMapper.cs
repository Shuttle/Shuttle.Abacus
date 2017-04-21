using System.Collections.Generic;
using System.Data;
using Shuttle.Abacus.DataAccess.Definitions;
using Shuttle.Abacus.DTO;
using Shuttle.Core.Data;

namespace Shuttle.Abacus.DataAccess.Mapping
{
    public class ArgumentDTOMapper : IDataRowMapper<ArgumentDTO>
    {
        public IEnumerable<ArgumentDTO> MapFrom(DataTable input)
        {
            var dto = new ArgumentDTO();

            var result = new List<ArgumentDTO>();

            foreach (DataRow row in input.Rows)
            {
                var name = ArgumentColumns.Name.MapFrom(row);
                var type = ArgumentColumns.AnswerType.MapFrom(row);

                if (!name.Equals(dto.Name))
                {
                    if (!string.IsNullOrEmpty(dto.Name))
                    {
                        result.Add(dto);
                    }

                    dto = new ArgumentDTO
                          {
                              Id = ArgumentColumns.Id.MapFrom(row),
                              Name = name,
                              AnswerType = type
                          };
                }

                var answer = ArgumentColumns.RestrictedAnswerColumns.Answer.MapFrom(row);

                if (!string.IsNullOrEmpty(answer))
                {
                    dto.Answers.Add(new ArgumentRestrictedAnswerDTO(answer));
                }
            }

            if (!string.IsNullOrEmpty(dto.Name))
            {
                result.Add(dto);
            }

            return result;
        }
    }
}
