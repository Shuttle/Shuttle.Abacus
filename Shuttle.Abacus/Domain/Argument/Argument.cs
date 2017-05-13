using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Shuttle.Abacus.Events.Argument.v1;
using Shuttle.Core.Infrastructure;

namespace Shuttle.Abacus.Domain
{
    public class Argument
    {
        private readonly List<string> _values = new List<string>();

        public Argument(Guid id)
        {
            Id = id;
        }

        public string Name { get; private set; }
        public string AnswerType { get; private set; }
        public bool Removed { get; private set; }

        public IEnumerable<string> Values => new ReadOnlyCollection<string>(_values);
        public bool HasValues => _values.Count > 0;

        public Guid Id { get; private set; }

        public Registered Register(string name, string answerType)
        {
            Guard.AgainstNullOrEmptyString(name, "name");
            Guard.AgainstNullOrEmptyString(answerType, "answerType");

            return On(new Registered
            {
                Name = name,
                AnswerType = answerType
            });
        }

        public Registered On(Registered registered)
        {
            Guard.AgainstNull(registered, "registered");

            Name = registered.Name;
            AnswerType = registered.AnswerType;

            return registered;
        }

        public Removed Remove()
        {
            if (Removed)
            {
                throw new DomainException("Already removed.");
            }

            return On(new Removed());
        }

        public Removed On(Removed removed)
        {
            Guard.AgainstNull(removed, "removed");

            Removed = true;

            return removed;
        }

        public bool IsNamed(string name)
        {
            Guard.AgainstNullOrEmptyString(name, "name");

            return Name.Equals(name, StringComparison.InvariantCultureIgnoreCase);
        }

        public Renamed Rename(string name)
        {
            Guard.AgainstNullOrEmptyString(name, "name");

            if (IsNamed(name))
            {
                throw new DomainException(string.Format("Already named '{0}'.", name));
            }

            return On(new Renamed
            {
                Name = name
            });
        }

        public Renamed On(Renamed renamed)
        {
            Guard.AgainstNull(renamed, "renamed");

            Name = renamed.Name;

            return renamed;
        }

        public AnswerTypeChanged ChangeAnswerType(string answerType)
        {
            Guard.AgainstNullOrEmptyString(answerType, "answerType");

            return On(new AnswerTypeChanged
            {
                AnswerType = answerType
            });
        }

        public AnswerTypeChanged On(AnswerTypeChanged answerTypeChanged)
        {
            Guard.AgainstNull(answerTypeChanged, "answerTypeChanged");

            AnswerType = answerTypeChanged.AnswerType;

            return answerTypeChanged;
        }

        public ValueAdded AddValue(string value)
        {
            if (ContainsValue(value))
            {
                throw new DomainException(string.Format("Value '{0}' has already been added.", value));
            }

            return On(new ValueAdded
            {
                Value = value
            });
        }

        public ValueAdded On(ValueAdded valueAdded)
        {
            Guard.AgainstNull(valueAdded, "valueAdded");

            _values.Add(valueAdded.Value);

            return valueAdded;
        }

        public bool ContainsValue(string value)
        {
            return _values.Contains(value);
        }

        public static string Key(string name)
        {
            return string.Format("[argument]:name={0}", name);
        }

        public ValueRemoved RemoveValue(string value)
        {
            if (ContainsValue(value))
            {
                throw new DomainException(string.Format("Cannot remove value '{0}' since it does not exist.", value));
            }

            return On(new ValueRemoved
            {
                Value = value
            });
        }

        public ValueRemoved On(ValueRemoved valueRemoved)
        {
            Guard.AgainstNull(valueRemoved, "valueRemoved");

            _values.Remove(valueRemoved.Value);

            return valueRemoved;
        }
    }
}
