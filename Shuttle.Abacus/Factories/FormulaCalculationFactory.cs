namespace Shuttle.Abacus
{
    public class FormulaCalculationFactory : ICalculationFactory
    {
        public Calculation Create(string name, bool required)
        {
            return new FormulaCalculation(name, required);
        }

        public string Name
        {
            get { return "Formula"; }
        }
    }
}
