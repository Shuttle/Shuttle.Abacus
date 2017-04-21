namespace Shuttle.Abacus.Domain
{
    public class IntegerArgumentAnswerFactory : IArgumentAnswerFactory
    {
        public string Text
        {
            get { return "Integer"; }
        }

        public ArgumentAnswer Create(string name, string answer)
        {
            return new IntegerArgumentAnswer(name, answer);
        }

        public string Name
        {
            get { return "Integer"; }
        }
    }
}