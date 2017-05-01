namespace Shuttle.Abacus.Domain
{
    public class RoundingOperationFactory : IOperationFactory
    {
        public Operation Create(IValueSource valueSource)
        {
            return new RoundingOperation(valueSource);
        }

        public string Name
        {
            get { return "Rounding"; }
        }

        public string Text
        {
            get { return "Rounding"; }
        }
    }
}
