using System;
using System.Collections.Generic;
using Abacus.Messages.DTO;

namespace Shuttle.Abacus
{
    public interface IChangeArgumentCommand
    {
        Guid ArgumentId { get; set; }
        string Name { get; set; }
        string AnswerType { get; set; }
        List<ArgumentRestrictedAnswerDTO> ArgumentAnswers { get; set; }
    }
}
