using System;
using System.Collections.Generic;

namespace Shuttle.Abacus.Messages.v1
{
    public class ChangeArgumentCommand
    {
        public ChangeArgumentCommand()
        {
            Answers = new List<string>();
        }

        public Guid ArgumentId { get; set; }
        public string Name { get; set; }
        public string AnswerType { get; set; }
        public List<string> Answers { get; set; }
    }
}
