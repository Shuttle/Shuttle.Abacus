using System.Collections.Generic;

namespace Shuttle.Abacus.Domain
{
    public class CreateArgumentCommand
    {
        public CreateArgumentCommand()
        {
            Answers = new List<string>();
        }

        public string Name { get; set; }
        public string AnswerType { get; set; }
        public List<string> Answers { get; set; }
    }
}