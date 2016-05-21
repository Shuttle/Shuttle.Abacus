using System.Collections.Generic;

namespace Shuttle.Abacus
{
    public class CreateArgumentCommand : ICreateArgumentCommand
    {
        public string Name { get; set; }
        public string AnswerType { get; set; }
        public List<ArgumentRestrictedAnswerDTO> Answers { get; set; }
    }
}
