using System.Collections.Generic;

namespace Shuttle.Abacus
{
    public interface ICreateArgumentCommand
    {
        string Name { get; set; }
        string AnswerType { get; set; }
        List<ArgumentRestrictedAnswerDTO> Answers { get; set; }
    }
}