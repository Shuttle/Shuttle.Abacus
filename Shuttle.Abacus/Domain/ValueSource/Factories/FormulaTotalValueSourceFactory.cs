namespace Shuttle.Abacus.Domain
{
    public class FormulaTotalValueSourceFactory : IValueSourceFactory
    {
        public string Name => "FormulaTotal";

        public string Text => "Formula Total";

        public string Type => "Derived";

        public IValueSource Create(string value)
        {
            return new FormulaTotalValueSource();
        }
    }
}
