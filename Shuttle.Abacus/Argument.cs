using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Shuttle.Abacus.Events.Argument.v1;
using Shuttle.Core.Contract;

namespace Shuttle.Abacus
{
    //public enum DataTypeName
    //{
    //    Boolean = 0,
    //    Date = 1,
    //    Decimal = 2,
    //    Integer = 3,
    //    List = 4,
    //    Money = 5,
    //    Text = 6
    //}

    public class Argument
    {
        private readonly List<string> _values = new List<string>();

        public Argument(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; }

        public string Name { get; private set; }
        public string DataType { get; private set; }
        public bool Removed { get; private set; }

        public IEnumerable<string> Values => new ReadOnlyCollection<string>(_values);
        public bool HasValues => _values.Count > 0;

        public Registered Register(string name, string dataType)
        {
            Guard.AgainstNullOrEmptyString(name, nameof(name));
            Guard.AgainstNullOrEmptyString(dataType, nameof(dataType));

            return On(new Registered
            {
                Name = name,
                DataTypeName = dataType
            });
        }

        private Registered On(Registered registered)
        {
            Guard.AgainstNull(registered, nameof(registered));

            Name = registered.Name;
            DataType = registered.DataTypeName;

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

        private Removed On(Removed removed)
        {
            Guard.AgainstNull(removed, nameof(removed));

            Removed = true;

            return removed;
        }

        public bool IsNamed(string name)
        {
            Guard.AgainstNullOrEmptyString(name, nameof(name));

            return Name.Equals(name, StringComparison.InvariantCultureIgnoreCase);
        }

        public Renamed Rename(string name)
        {
            Guard.AgainstNullOrEmptyString(name, nameof(name));

            if (IsNamed(name))
            {
                throw new DomainException($"Already named '{name}'.");
            }

            return On(new Renamed
            {
                Name = name
            });
        }

        private Renamed On(Renamed renamed)
        {
            Guard.AgainstNull(renamed, nameof(renamed));

            Name = renamed.Name;

            return renamed;
        }

        public DataTypeNameSet SetDataTypeName(string dataType)
        {
            Guard.AgainstNullOrEmptyString(dataType, nameof(dataType));

            return On(new DataTypeNameSet
            {
                DataTypeName = dataType
            });
        }

        private DataTypeNameSet On(DataTypeNameSet dataTypeNameSet)
        {
            Guard.AgainstNull(dataTypeNameSet, nameof(dataTypeNameSet));

            DataType = dataTypeNameSet.DataTypeName;

            return dataTypeNameSet;
        }

        public ValueAdded AddValue(string value)
        {
            if (ContainsValue(value))
            {
                throw new DomainException($"Value '{value}' has already been added.");
            }

            return On(new ValueAdded
            {
                Value = value
            });
        }

        private ValueAdded On(ValueAdded valueAdded)
        {
            Guard.AgainstNull(valueAdded, nameof(valueAdded));

            _values.Add(valueAdded.Value);

            return valueAdded;
        }

        public bool ContainsValue(string value)
        {
            return _values.Contains(value);
        }

        public static string Key(string name)
        {
            return $"[argument]:name={name}";
        }

        public ValueRemoved RemoveValue(string value)
        {
            if (!ContainsValue(value))
            {
                throw new DomainException($"Cannot remove value '{value}' since it does not exist.");
            }

            return On(new ValueRemoved
            {
                Value = value
            });
        }

        private ValueRemoved On(ValueRemoved valueRemoved)
        {
            Guard.AgainstNull(valueRemoved, nameof(valueRemoved));

            _values.Remove(valueRemoved.Value);

            return valueRemoved;
        }
    }
}