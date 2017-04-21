namespace Shuttle.Abacus
{
    public class AdditionOperationFactory : IOperationFactory
    {
        public FormulaOperation Create(IValueSource valueSource)
        {
            return new AdditionOperation(valueSource);
        }

        public string Name
        {
            get { return "Addition"; }
        }

        public string Text
        {
            get { return "Addition"; }
        }
    }
}
