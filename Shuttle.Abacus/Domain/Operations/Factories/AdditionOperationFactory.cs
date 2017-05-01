namespace Shuttle.Abacus.Domain
{
    public class AdditionOperationFactory : IOperationFactory
    {
        public Operation Create(IValueSource valueSource)
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
