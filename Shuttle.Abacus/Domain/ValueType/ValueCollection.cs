using System.Collections;
using System.Collections.Generic;

namespace Shuttle.Abacus.Domain
{
    public class ValueCollection : IEnumerable<ValueType>
    {
        private readonly Dictionary<string, ValueType> answers = new Dictionary<string, ValueType>();

        public ValueType this[string argumentName] => answers[argumentName.ToLower()];

        public IEnumerator<ValueType> GetEnumerator()
        {
            return answers.Values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Add(ValueType valueType)
        {
            answers.Add(valueType.ArgumentName.ToLower(), valueType);
        }

        public bool Contains(string argumentName)
        {
            return answers.ContainsKey(argumentName.ToLower());
        }
    }
}