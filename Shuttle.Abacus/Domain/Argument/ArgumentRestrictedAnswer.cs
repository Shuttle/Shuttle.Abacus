namespace Shuttle.Abacus.Domain
{
    public class ArgumentRestrictedAnswer
    {
        public ArgumentRestrictedAnswer(string answer)
        {
            Answer = answer;
        }

        public string Answer { get; private set; }
    }
}
