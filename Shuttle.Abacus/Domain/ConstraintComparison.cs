using System;
using Shuttle.Core.Infrastructure;

namespace Shuttle.Abacus.Domain
{
    public class ConstraintComparison : IConstraintComparison
    {
        private static readonly char[] Separator = { ',' };

        private readonly IValueTypeFactory _valueTypeFactory;

        public ConstraintComparison(IValueTypeFactory valueTypeFactory)
        {
            Guard.AgainstNull(valueTypeFactory, "valueTypeFactory");

            _valueTypeFactory = valueTypeFactory;
        }

        public bool IsSatisfiedBy(string type, string argumentValue, string comparison, string constraintValue)
        {
            foreach (var argumentValueItem in argumentValue.Split(Separator, StringSplitOptions.RemoveEmptyEntries))
            {
                //var argumentValueType = _valueTypeFactory.Create(argumentValueItem);

                //foreach (var comparisonValue in Value.Split(Separator, StringSplitOptions.RemoveEmptyEntries))
                //{
                //    var comparisonValueType = argument.CreateValueType(comparisonValue);
                //}


            }

            throw new System.NotImplementedException();
        }
    }
}