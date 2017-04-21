namespace Shuttle.Abacus.Infrastructure
{
    public class NullCache : ICache
    {
        public void Flush()
        {
            
        }

        public void Add(string key, object item)
        {
            
        }

        public T Get<T>(string key)
        {
            return default(T);
        }
    }
}
