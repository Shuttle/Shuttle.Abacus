namespace Shuttle.Abacus.Domain
{
    public class DivisionOperationFactory : IOperationFactory
    {
        public FormulaOperation Create(IValueSource valueSource)
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
