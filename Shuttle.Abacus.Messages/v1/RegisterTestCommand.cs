using System.Collections.Generic;
using Shuttle.Abacus.Messages.v1.TransferObjects;

namespace Shuttle.Abacus.Messages.v1
{
    public class RegisterTestCommand
    {
        public RegisterTestCommand()
        {
            ArgumentAnswers = new List<ArgumentAnswer>();
        }

        public string Name { get; set; }
        public string ExpectedResult { get; set; }

        public List<ArgumentAnswer> ArgumentAnswers { get; private set; }
        public string ExpectedResultType { get; set; }
        public string Comparison { get; set; }
    }
}
