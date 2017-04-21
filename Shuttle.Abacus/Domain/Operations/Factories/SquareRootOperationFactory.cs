namespace Shuttle.Abacus.Domain
{
    public class SquareRootOperationFactory : IOperationFactory
    {
        public FormulaOperation Create(IValueSource valueSource)
        {
            return new SquareRootOperation(valueSource);
        }

        public string Name
        {
            get { return "SquareRoot"; }
        }

        public string Text
        {
            get { return "Square Root"; }
        }
    }
}
