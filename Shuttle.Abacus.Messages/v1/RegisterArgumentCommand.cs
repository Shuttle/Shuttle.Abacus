using System.Collections.Generic;

namespace Shuttle.Abacus.Messages.v1
{
    public class RegisterArgumentCommand
    {
        public RegisterArgumentCommand()
        {
            new List<string>();
        }

        public string Name { get; set; }
        public string AnswerType { get; set; }
    }
}