namespace Shuttle.Abacus.Domain
{
    public class BooleanArgumentAnswerFactory : IArgumentAnswerFactory
    {
        public string Text
        {
            get { return "Boolean"; }
        }

        public ArgumentAnswer Create(string name, string answer)
        {
            return new BooleanArgumentAnswer(name, answer);
        }

        public string Name
        {
            get { return "Boolean"; }
        }
    }
}