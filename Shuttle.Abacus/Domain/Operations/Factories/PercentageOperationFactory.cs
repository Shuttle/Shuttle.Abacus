namespace Shuttle.Abacus.Domain
{
    public class PercentageOperationFactory : IOperationFactory
    {
        public Operation Create(IValueSource valueSource)
        {
            return new PercentageOperation(valueSource);
        }

        public string Name
        {
            get { return "Percentage"; }
        }

        public string Text
        {
            get { return "Percentage"; }
        }
    }
}
