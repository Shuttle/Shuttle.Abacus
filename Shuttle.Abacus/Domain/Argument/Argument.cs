using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Shuttle.Abacus.Domain
{
    public class Argument
    {
        private readonly List<string> _values = new List<string>();

        public Argument(Guid id, string name, string answerType)
        {
            Id = id;
            Name = name;
            AnswerType = answerType;
        }

        public string Name { get; private set; }
        public string AnswerType { get; private set; }

        public IEnumerable<string> Values => new ReadOnlyCollection<string>(_values);
        public bool HasValues => _values.Count > 0;

        public Guid Id { get; private set; }

        public Argument AddValue(string value)
        {
            if (!_values.Contains(value))
            {
                _values.Add(value);
            }

            return this;
        }

        public bool ContainsValue(string value)
        {
            return _values.Contains(value);
        }

        public Argument AddValues(IEnumerable<string> values)
        {
            foreach (var value in values)
            {
                AddValue(value);
            }

            return this;
        }
    }
}
