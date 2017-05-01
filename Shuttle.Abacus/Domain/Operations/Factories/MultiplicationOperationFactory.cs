namespace Shuttle.Abacus.Domain
{
    public class MultiplicationOperationFactory : IOperationFactory
    {
        public Operation Create(IValueSource valueSource)
        {
            return new MultiplicationOperation(valueSource);
        }

        public string Name
        {
            get { return "Multiplication"; }
        }

        public string Text
        {
            get { return "Multiplication"; }
        }
    }
}
