namespace Shuttle.Abacus
{
    public class MaximumLimitFactory : ILimitFactory
    {
        public string Name
        {
            get { return "Maximum"; }
        }

        public Limit Create(string name)
        {
            return new MaximumLimit(name);
        }
    }
}
