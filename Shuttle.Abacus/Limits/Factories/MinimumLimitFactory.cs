namespace Shuttle.Abacus
{
    public class MinimumLimitFactory : ILimitFactory
    {
        public string Name
        {
            get { return "Minimum"; }
        }

        public Limit Create(string name)
        {
            return new MinimumLimit(name);
        }
    }
}
