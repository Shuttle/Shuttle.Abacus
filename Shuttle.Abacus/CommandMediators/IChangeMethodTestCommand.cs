using System;
using System.Collections.Generic;
using Abacus.DTO;

namespace Abacus.CommandMediators
{
    public interface IChangeMethodTestCommand
    {
        Guid MethodTestId { get; set; }
        Guid MethodId { get; set; }
        string Description { get; set; }
        decimal ExpectedResult { get; set; }
        List<ArgumentAnswerDTO> ArgumentAnswers { get; }
    }
}
