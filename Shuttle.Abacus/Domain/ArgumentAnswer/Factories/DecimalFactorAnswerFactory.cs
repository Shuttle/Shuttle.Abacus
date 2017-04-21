namespace Shuttle.Abacus
{
    public class DecimalArgumentAnswerFactory : IArgumentAnswerFactory
    {
        public string Text
        {
            get { return "Decimal"; }
        }

        public ArgumentAnswer Create(string name, string answer)
        {
            return new DecimalArgumentAnswer(name, answer);
        }

        public string Name
        {
            get { return "Decimal"; }
        }
    }
}