using System.Collections.Generic;

namespace Shuttle.Abacus.Domain
{
    public class ValueTypeValidatorProvider : IValueTypeValidatorProvider
    {
        private readonly Dictionary<string, IValueTypeValidator> _validators = new Dictionary<string, IValueTypeValidator>();

        public ValueTypeValidatorProvider(IEnumerable<IValueTypeValidator> validators)
        {
            foreach (var validator in validators)
            {
                _validators.Add(validator.Type.ToLower(), validator);
            }
        }

        public IValueTypeValidator Get(string type)
        {
            if (!_validators.ContainsKey(type.ToLower()))
            {
                throw new KeyNotFoundException(string.Format(Resources.KeyNotFoundException, type,
                                                             "ValueTypeValidatorProvider"));
            }

            return _validators[type.ToLower()];
        }

        public bool Has(string type)
        {
            return _validators.ContainsKey(type.ToLower());
        }
    }
}
