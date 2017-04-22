using Shuttle.Abacus.DTO;
using Shuttle.Abacus.Infrastructure;

namespace Shuttle.Abacus.Domain
{
    public class ArgumentDTOMapper : IMapper<ArgumentDTO, Argument>
    {
        public Argument MapFrom(ArgumentDTO input)
        {
            var argument = new Argument(input.Id)
                         {
                             Name = input.Name,
                             AnswerType = input.AnswerType
                         };

            foreach (var mapping in input.Answers)
            {
                argument.AddArgumentAnswer(mapping.Answer);
            }

            return argument;
        }
    }
}
