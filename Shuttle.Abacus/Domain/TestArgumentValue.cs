using System;

namespace Shuttle.Abacus.Domain
{
    public class TestArgumentValue
    {
        public TestArgumentValue(Guid argumentId, string answer)
        {
            ArgumentId = argumentId;
            Answer = answer;
        }

        public Guid ArgumentId { get; private set; }
        public string Answer { get; private set; }
    }
}
