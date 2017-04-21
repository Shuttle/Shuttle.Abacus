using System.Collections;
using System.Collections.Generic;

namespace Shuttle.Abacus.Domain
{
    public class ArgumentAnswerCollection : IEnumerable<ArgumentAnswer>
    {
        private readonly Dictionary<string, ArgumentAnswer> answers = new Dictionary<string, ArgumentAnswer>();

        public ArgumentAnswer this[string argumentName]
        {
            get { return answers[argumentName.ToLower()]; }
        }

        public IEnumerator<ArgumentAnswer> GetEnumerator()
        {
            return answers.Values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Add(ArgumentAnswer argumentAnswer)
        {
            answers.Add(argumentAnswer.ArgumentName.ToLower(), argumentAnswer);
        }

        public bool Contains(string argumentName)
        {
            return answers.ContainsKey(argumentName.ToLower());
        }
    }
}