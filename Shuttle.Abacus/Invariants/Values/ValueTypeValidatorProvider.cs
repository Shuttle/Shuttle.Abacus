using System.Collections.Generic;
using Shuttle.Abacus.Localisation;

namespace Shuttle.Abacus.Invariants.Values
{
    public class ValueTypeValidatorProvider : IValueTypeValidatorProvider
    {
        private readonly Dictionary<string, IValueTypeValidator> _valueTypeValidators =
            new Dictionary<string, IValueTypeValidator>();

        public ValueTypeValidatorProvider()
        {
            _valueTypeValidators.Add("integer", new IntegerValueTypeValidator());
            _valueTypeValidators.Add("decimal", new DecimalValueTypeValidator());
            _valueTypeValidators.Add("date", new DateValueTypeValidator());
        }

        public IValueTypeValidator Get(string type)
        {
            if (!_valueTypeValidators.ContainsKey(type.ToLower()))
            {
                throw new KeyNotFoundException(string.Format(Resources.KeyNotFoundException, type,
                    "ValueTypeValidatorProvider"));
            }

            return _valueTypeValidators[type.ToLower()];
        }

        public bool Has(string type)
        {
            return _valueTypeValidators.ContainsKey(type.ToLower());
        }
    }
}