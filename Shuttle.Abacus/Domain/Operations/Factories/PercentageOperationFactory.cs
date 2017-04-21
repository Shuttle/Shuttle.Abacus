namespace Shuttle.Abacus
{
    public class PercentageOperationFactory : IOperationFactory
    {
        public FormulaOperation Create(IValueSource valueSource)
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
