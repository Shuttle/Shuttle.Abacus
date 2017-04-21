namespace Shuttle.Abacus
{
    public class MultiplicationOperationFactory : IOperationFactory
    {
        public FormulaOperation Create(IValueSource valueSource)
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
