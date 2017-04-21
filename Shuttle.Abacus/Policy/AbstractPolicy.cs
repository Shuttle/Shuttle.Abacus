using System;
using Shuttle.Abacus.Domain;

namespace Shuttle.Abacus.Policy
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
