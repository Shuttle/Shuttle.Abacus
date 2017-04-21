using System;
using System.Collections.Generic;
using Abacus.CommandMediators;
using Abacus.DTO;
using NServiceBus;

namespace Abacus.Messages
{
    public class ChangeArgumentCommand : IMessage, IChangeArgumentCommand
    {
        public Guid ArgumentId { get; set; }
        public string Name { get; set; }
        public string AnswerType { get; set; }
        public List<ArgumentRestrictedAnswerDTO> ArgumentAnswers { get; set; }
    }
}
