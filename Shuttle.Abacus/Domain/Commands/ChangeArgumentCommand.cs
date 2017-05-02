using System;
using System.Collections.Generic;
using Shuttle.Abacus.DTO;

namespace Shuttle.Abacus.Domain
{
    public class ChangeArgumentCommand
    {
        public Guid ArgumentId { get; set; }
        public string Name { get; set; }
        public string AnswerType { get; set; }
        //TODO
        //public List<ArgumentRestrictedAnswerDTO> Answers { get; set; }
    }
}
