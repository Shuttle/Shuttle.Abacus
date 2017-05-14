namespace Shuttle.Abacus.Domain
{
    public class RunningTotalValueSourceFactory : IValueSourceFactory
    {
        public string Name => "RunningTotal";

        public string Text => "Running Total";

        public string Type => "Derived";

        public IValueSource Create(string value)
        {
            return new RunningTotalValueSource();
        }
    }
}
