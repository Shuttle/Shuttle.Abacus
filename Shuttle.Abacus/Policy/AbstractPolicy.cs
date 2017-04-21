using System;
using Abacus.Domain;
using Abacus.Validation;

namespace Abacus.Policy
{
    public abstract class AbstractPolicy<T> where T : Entity<T>
    {
        protected IRule<T> IdRule()
        {
            return
                new Rule<T>
                    (
                    "Id may not be empty.",
                    (item, rule) => item.Id.Equals(Guid.Empty));
        }
    }
}
