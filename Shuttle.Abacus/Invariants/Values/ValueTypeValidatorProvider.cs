using System.Collections.Generic;
using Abacus.Infrastructure;
using Abacus.Localisation;

namespace Abacus.Validation
{
    public class ValueTypeValidatorProvider : IValueTypeValidatorProvider
    {
        private readonly Dictionary<string, IValueTypeValidator> validators = new Dictionary<string, IValueTypeValidator>();

        public ValueTypeValidatorProvider()
        {
            DependencyResolver.Resolver.ResolveAll<IValueTypeValidator>().ForEach(validator => validators.Add(validator.Type.ToLower(), validator));
        }

        public IValueTypeValidator Get(string type)
        {
            if (!validators.ContainsKey(type.ToLower()))
            {
                throw new KeyNotFoundException(string.Format(Resources.KeyNotFoundException, type,
                                                             "ValueTypeValidatorProvider"));
            }

            return validators[type.ToLower()];
        }

        public bool Has(string type)
        {
            return validators.ContainsKey(type.ToLower());
        }
    }
}
