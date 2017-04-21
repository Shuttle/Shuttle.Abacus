using System;
using System.Collections.Generic;
using Abacus.DTO;

namespace Abacus.CommandMediators
{
    public interface IChangeArgumentCommand
    {
        Guid ArgumentId { get; set; }
        string Name { get; set; }
        string AnswerType { get; set; }
        List<ArgumentRestrictedAnswerDTO> ArgumentAnswers { get; set; }
    }
}
