namespace Shuttle.Abacus
{
    public class TextArgumentAnswerFactory : IArgumentAnswerFactory
    {
        public string Name
        {
            get { return "Text"; }
        }

        public string Text
        {
            get { return "Text"; }
        }

        public ArgumentAnswer Create(string name, string answer)
        {
            return new TextArgumentAnswer(name, answer);
        }
    }
}