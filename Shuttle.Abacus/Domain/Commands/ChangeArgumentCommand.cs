using System;
using System.Collections.Generic;

namespace Shuttle.Abacus
{
    public class ChangeArgumentCommand : IChangeArgumentCommand
    {
        public Guid ArgumentId { get; set; }
        public string Name { get; set; }
        public string AnswerType { get; set; }
        public List<ArgumentRestrictedAnswerDTO> ArgumentAnswers { get; set; }
    }
}
