namespace Shuttle.Abacus.Domain
{
    public class DateTimeArgumentAnswerFactory : IArgumentAnswerFactory
    {
        public string Name
        {
            get { return "DateTime"; }
        }

        public string Text
        {
            get { return "Date/Time"; }
        }

        public ArgumentAnswer Create(string name, string answer)
        {
            return new DateTimeArgumentAnswer(name, answer);
        }
    }
}