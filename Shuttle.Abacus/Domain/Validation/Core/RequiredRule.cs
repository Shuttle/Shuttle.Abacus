using System;

namespace Shuttle.Abacus.Domain
{
    public class RequiredRule : Rule
    {
        public RequiredRule()
            : base(Resources.RequiredRule, (item, rule) => Convert.ToString(item).Length < 1)
        {
        }
    }
}
