using System;
using System.Collections.Generic;
using Abacus.CommandMediators;
using Abacus.DTO;
using NServiceBus;

namespace Abacus.Messages
{
    public class CreateMethodTestCommand : IMessage, ICreateMethodTestCommand
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
