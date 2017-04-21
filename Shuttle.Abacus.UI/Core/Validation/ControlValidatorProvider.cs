using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Abacus.Infrastructure;

namespace Abacus.UI
{
    public class ControlValidatorProvider : IControlValidatorProvider
    {
        private readonly List<IControlValidator> validators = new List<IControlValidator>();

        public ControlValidatorProvider()
        {
            foreach (var validator in DependencyResolver.Resolver.ResolveAll<IControlValidator>())
            {
                Register(validator);
            }
        }

        public IControlValidatorProvider Register(IControlValidator validator)
        {
            if (GetFor(validator.HandlesType) != null)
            {
                return this;
            }

            validators.Add(validator);

            return this;
        }

        public IControlValidator GetFor<T>()
        {
            return GetFor(typeof (T));
        }

        public IControlValidator GetFor(Type type)
        {
            return validators.Find(validator => validator.HandlesType.Equals(type));
        }

        public IEnumerable<IControlValidator> Validators
        {
            get { return new ReadOnlyCollection<IControlValidator>(validators); }
        }
    }
}
