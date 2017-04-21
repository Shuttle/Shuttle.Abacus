using System.Collections.Generic;
using Abacus.DTO;

namespace Abacus.CommandMediators
{
    public interface ICreateArgumentCommand
    {
        string Name { get; set; }
        string AnswerType { get; set; }
        List<ArgumentRestrictedAnswerDTO> Answers { get; set; }
    }
}