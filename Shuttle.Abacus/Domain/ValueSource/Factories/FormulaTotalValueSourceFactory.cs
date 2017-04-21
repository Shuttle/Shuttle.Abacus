namespace Shuttle.Abacus.Domain
{
    public class FormulaTotalValueSourceFactory : IValueSourceFactory
    {
        public string Name
        {
            get { return "FormulaTotal"; }
        }

        public string Text
        {
            get { return "Formula Total"; }
        }

        public string Type
        {
            get { return "Derived"; }
        }

        public IValueSource Create(string value)
        {
            return new FormulaTotalValueSource();
        }
    }
}
