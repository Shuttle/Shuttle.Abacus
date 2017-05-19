using System;
using System.Collections.Generic;
using Shuttle.Abacus.Events.Test.v1;
using Shuttle.Core.Infrastructure;

namespace Shuttle.Abacus.Domain
{
    public class Test
    {
        private readonly Dictionary<string, string> _argumentValues = new Dictionary<string, string>();

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

        public Registered On(Registered registered)
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

        public static string Key(string name)
        {
            return string.Format("[argument]:name={0}", name);
        }

        public ArgumentValueRemoved RemoveArgumentName(string argumentName)
        {
            if (!_argumentValues.ContainsKey(argumentName))
            {
                throw new DomainException(string.Format("Cannot remove argument name '{0}' since it does not exist.",
                    argumentName));
            }

            return On(new ArgumentValueRemoved
            {
                ArgumentName = argumentName
            });
        }

        public ArgumentValueRemoved On(ArgumentValueRemoved argumentValueRemoved)
        {
            Guard.AgainstNull(argumentValueRemoved, "argumentValueRemoved");

            _argumentValues.Remove(argumentValueRemoved.ArgumentName);

            return argumentValueRemoved;
        }

        public ArgumentValueSet SetArgumentValue(string argumentName, string value)
        {
            Guard.AgainstNullOrEmptyString(argumentName, "argumentName");
            Guard.AgainstNullOrEmptyString(value, "value");

            return On(new ArgumentValueSet
            {
                ArgumentName = argumentName,
                Value = value
            });
        }

        public ArgumentValueSet On(ArgumentValueSet argumentValueSet)
        {
            Guard.AgainstNull(argumentValueSet, "argumentValueSet");

            _argumentValues.Remove(argumentValueSet.ArgumentName);
            _argumentValues.Add(argumentValueSet.ArgumentName, argumentValueSet.Value);

            return argumentValueSet;
        }
    }
}