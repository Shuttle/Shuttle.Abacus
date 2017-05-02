using System.Collections.Generic;
using Shuttle.Abacus.DTO;

namespace Shuttle.Abacus.Domain
{
    public class CreateArgumentCommand 
    {
        public string Name { get; set; }
        public string AnswerType { get; set; }
        //TODO
        //public List<ArgumentRestrictedAnswerDTO> Answers { get; set; }
    }
}
