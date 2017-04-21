namespace Shuttle.Abacus.Domain
{
    public class CollectionCalculationFactory : ICalculationFactory
    {
        public Calculation Create(string name, bool required)
        {
            return new CalculationCollection(name);
        }

        public string Name
        {
            get { return "Collection"; }
        }
    }
}
