using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Shuttle.Abacus.Events.Test.v1;
using Shuttle.Core.Contract;

namespace Shuttle.Abacus
{
    public class Test
    {
        private readonly List<ArgumentValue> _values = new List<ArgumentValue>();

        public Test(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; }
        public string Name { get; private set; }
        public string FormulaName { get; private set; }
        public string ExpectedResult { get; private set; }
        public string ExpectedResultType { get; private set; }
        public string Comparison { get; private set; }
        public bool Removed { get; private set; }

        public Registered Register(string name, string formulaName, string expectedResult, string expectedResultType,
            string comparison)
        {
            Guard.AgainstNullOrEmptyString(name, nameof(name));
            Guard.AgainstNullOrEmptyString(formulaName, nameof(formulaName));
            Guard.AgainstNullOrEmptyString(expectedResult, nameof(expectedResult));
            Guard.AgainstNullOrEmptyString(expectedResultType, nameof(expectedResultType));
            Guard.AgainstNullOrEmptyString(comparison, nameof(comparison));

            return On(new Registered
            {
                Name = name,
                FormulaName = formulaName,
                ExpectedResult = expectedResult,
                ExpectedResultType = expectedResultType,
                Comparison = comparison
            });
        }

        private Registered On(Registered registered)
        {
            Guard.AgainstNull(registered, nameof(registered));

            Name = registered.Name;
            FormulaName = registered.Name;
            ExpectedResult = registered.ExpectedResult;
            ExpectedResultType = registered.ExpectedResultType;
            Comparison = registered.Comparison;

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

        public static string Key(string name)
        {
            return $"[argument]:name={name}";
        }

        public ArgumentSet SetArgument(Guid argumentId, string value)
        {
            Guard.AgainstNullOrEmptyString(value, nameof(value));

            return On(new ArgumentSet
            {
                ArgumentId = argumentId,
                Value = value
            });
        }

        private ArgumentSet On(ArgumentSet argumentSet)
        {
            Guard.AgainstNull(argumentSet, nameof(argumentSet));

            _values.Remove(FindValue(argumentSet.ArgumentId));
            _values.Add(new ArgumentValue(argumentSet.ArgumentId, argumentSet.Value));

            return argumentSet;
        }

        private ArgumentValue FindValue(Guid argumentId)
        {
            return _values.Find(argumentValue => argumentValue.Id.Equals(argumentId));
        }

        public ArgumentRemoved RemoveArgument(Guid argumentId)
        {
            return On(new ArgumentRemoved
            {
                ArgumentId = argumentId
            });
        }

        private ArgumentRemoved On(ArgumentRemoved argumentRemoved)
        {
            Guard.AgainstNull(argumentRemoved, nameof(argumentRemoved));

            _values.Remove(FindValue(argumentRemoved.ArgumentId));

            return argumentRemoved;
        }

        public IEnumerable<ArgumentValue> ArgumentValues()
        {
            return new ReadOnlyCollection<ArgumentValue>(_values);
        }
    }
}