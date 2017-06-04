using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Shuttle.Abacus.Events.Test.v1;
using Shuttle.Core.Infrastructure;

namespace Shuttle.Abacus.Domain
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
            Guard.AgainstNullOrEmptyString(name, "name");
            Guard.AgainstNullOrEmptyString(formulaName, "formulaName");
            Guard.AgainstNullOrEmptyString(expectedResult, "expectedResult");
            Guard.AgainstNullOrEmptyString(expectedResultType, "expectedResultType");
            Guard.AgainstNullOrEmptyString(comparison, "compaprison");

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
            Guard.AgainstNull(registered, "registered");

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

        private Renamed On(Renamed renamed)
        {
            Guard.AgainstNull(renamed, "renamed");

            Name = renamed.Name;

            return renamed;
        }

        public static string Key(string name)
        {
            return string.Format("[argument]:name={0}", name);
        }

        public ArgumentSet SetArgument(string argumentName, string value)
        {
            Guard.AgainstNullOrEmptyString(argumentName, "argumentName");
            Guard.AgainstNullOrEmptyString(value, "value");

            return On(new ArgumentSet
            {
                ArgumentName = argumentName,
                Value = value
            });
        }

        private ArgumentSet On(ArgumentSet argumentSet)
        {
            Guard.AgainstNull(argumentSet, "argumentValueSet");

            _values.Remove(FindValue(argumentSet.ArgumentName));
            _values.Add(new ArgumentValue(argumentSet.ArgumentName, argumentSet.Value));

            return argumentSet;
        }

        private ArgumentValue FindValue(string argumentName)
        {
            return _values.Find(argumentValue => argumentValue.Name.Equals(argumentName, StringComparison.InvariantCultureIgnoreCase));
        }

        public ArgumentRemoved RemoveArgument(string argumentName)
        {
            Guard.AgainstNullOrEmptyString(argumentName, "argumentName");

            return On(new ArgumentRemoved
            {
                ArgumentName = argumentName
            });
        }

        private ArgumentRemoved On(ArgumentRemoved argumentRemoved)
        {
            Guard.AgainstNull(argumentRemoved, "argumentRemoved");

            _values.Remove(FindValue(argumentRemoved.ArgumentName));

            return argumentRemoved;
        }

        public IEnumerable<ArgumentValue> ArgumentValues()
        {
            return new ReadOnlyCollection<ArgumentValue>(_values);
        }
    }
}