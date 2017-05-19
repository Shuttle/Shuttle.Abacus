namespace Shuttle.Abacus.Shell.Core.State
{
    public class StateKey
    {
        public string Key { get; private set; }

        public StateKey(string key)
        {
            Key = key;
        }
    }
}
