using System.Collections.Generic;
using Shuttle.Core.Infrastructure;

namespace Shuttle.Abacus.Shell.Core.State
{
    public class State<T>
    {
        private readonly T owner;
        private readonly Dictionary<string, object> state = new Dictionary<string, object>();

        public State(T owner)
        {
            this.owner = owner;
        }

        public T Add(object value)
        {
            Guard.AgainstNull(value, "value");

            state.Add(value.GetType().FullName, value);

            return owner;
        }

        public T Add(StateKey key, object value)
        {
            Guard.AgainstNull(key, "key");

            state.Add(key.Key, value);

            return owner;
        }

        public TGet Get<TGet>(StateKey key)
        {
            Guard.AgainstNull(key, "key");

            if (!Contains(key))
            {
                throw new KeyNotFoundException(string.Format(Localisation.Resources.KeyNotFoundException, key, "State"));
            }

            return (TGet) state[key.Key];
        }

        public bool Contains(StateKey key)
        {
            Guard.AgainstNull(key, "key");

            return state.ContainsKey(key.Key);
        }

        public TGet Get<TGet>()
        {
            var key = new StateKey(typeof (TGet).FullName);

            if (!Contains(key))
            {
                throw new KeyNotFoundException(string.Format(Localisation.Resources.KeyNotFoundException, key, "State"));
            }

            return (TGet) state[key.Key];
        }
    }

    public class State : State<object>
    {
        public State() : base(null)
        {
        }
    }
}
