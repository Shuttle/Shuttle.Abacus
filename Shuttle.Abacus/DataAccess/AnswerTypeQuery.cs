using System.Collections.Generic;
using Abacus.DTO;

namespace Abacus.Data
{
    public class AnswerTypeQuery : DataQuery, IAnswerTypeQuery
    {
        private readonly IDataTableMapper<AnswerTypeDTO> answerTypeDTOMapper;

        public AnswerTypeQuery(IDataTableMapper<AnswerTypeDTO> answerTypeDTOMapper)
        {
            this.answerTypeDTOMapper = answerTypeDTOMapper;
        }

        public IQueryResult All()
        {
            return QueryProcessor.Execute(AnswerTypeQueries.All());
        }

        public IEnumerable<AnswerTypeDTO> AllDTOs()
        {
            return answerTypeDTOMapper.MapFrom(All().Table);
        }
    }
}
