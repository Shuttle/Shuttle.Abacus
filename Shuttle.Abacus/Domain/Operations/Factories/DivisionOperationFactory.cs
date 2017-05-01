namespace Shuttle.Abacus.Domain
{
    public class DivisionOperationFactory : IOperationFactory
    {
        public Operation Create(IValueSource valueSource)
        {
            return new DivisionOperation(valueSource);
        }

        public string Name
        {
            get { return "Division"; }
        }

        public string Text
        {
            get { return "Division"; }
        }
    }
}
