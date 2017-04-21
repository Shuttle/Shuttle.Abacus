namespace Shuttle.Abacus
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
