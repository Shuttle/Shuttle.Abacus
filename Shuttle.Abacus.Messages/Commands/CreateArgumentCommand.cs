using System;
using System.Collections.Generic;
using Abacus.CommandMediators;
using Abacus.DTO;
using NServiceBus;

namespace Abacus.Messages
{
    public class CreateArgumentCommand : IMessage, ICreateArgumentCommand
    {
        public string Name { get; set; }
        public string AnswerType { get; set; }
        public List<ArgumentRestrictedAnswerDTO> Answers { get; set; }
    }
}
