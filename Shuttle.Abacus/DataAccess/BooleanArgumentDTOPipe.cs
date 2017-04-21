using Shuttle.Abacus.DTO;
using Shuttle.Abacus.Infrastructure;

namespace Shuttle.Abacus.DataAccess.Query
{
    public class BooleanArgumentDTOPipe : IPipe<ArgumentDTO>
    {
        public void Handle(ArgumentDTO item)
        {
            if (item.AnswerType.ToLower() != "boolean")
            {
                return;
            }

            if (!item.HasArgumentName("True"))
            {
                item.Answers.Add(new ArgumentRestrictedAnswerDTO("True"));
            }

            if (!item.HasArgumentName("False"))
            {
                item.Answers.Add(new ArgumentRestrictedAnswerDTO("False"));
            }
        }
    }
}
