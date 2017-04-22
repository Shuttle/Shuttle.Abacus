using System.Collections.Generic;
using System.Data;
using Shuttle.Abacus.DTO;

namespace Shuttle.Abacus.DataAccess
{
    public class MethodTestDTOMapper : IMethodTestDTOMapper
    {
        public IEnumerable<MethodTestDTO> MapFrom(DataTable input)
        {
            var dto = new MethodTestDTO();

            var result = new List<MethodTestDTO>();

            foreach (DataRow row in input.Rows)
            {
                var description = MethodTestColumns.Description.MapFrom(row);

                if (!description.Equals(dto.Description))
                {
                    if (!string.IsNullOrEmpty(dto.Description))
                    {
                        result.Add(dto);
                    }

                    dto = new MethodTestDTO
                          {
                              MethodTestId = MethodTestColumns.Id.MapFrom(row),
                              Description = description,
                              ExpectedResult = MethodTestColumns.ExpectedResult.MapFrom(row),
                              MethodId = MethodTestColumns.MethodId.MapFrom(row)
                          };
                }

                dto.InputList.Add(MethodTestColumns.ArgumentAnswerColumns.ArgumentName.MapFrom(row),
                                  MethodTestColumns.ArgumentAnswerColumns.Answer.MapFrom(row));
            }

            if (!string.IsNullOrEmpty(dto.Description))
            {
                result.Add(dto);
            }

            return result;
        }
    }
}
