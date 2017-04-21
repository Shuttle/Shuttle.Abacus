using System;
using System.Collections.Generic;
using Shuttle.Abacus.DTO;

namespace Shuttle.Abacus.Domain
{
    public class CreateMethodTestCommand
    {
        public CreateMethodTestCommand()
        {
            ArgumentAnswers = new List<ArgumentAnswerDTO>();
        }

        public Guid MethodTestId { get; set; }
        public Guid MethodId { get; set; }
        public string Description { get; set; }
        public decimal ExpectedResult { get; set; }

        public List<ArgumentAnswerDTO> ArgumentAnswers { get; private set; }
    }
}
