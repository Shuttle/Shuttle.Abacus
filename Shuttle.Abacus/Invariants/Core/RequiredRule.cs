using System;
using Abacus.Localisation;

namespace Abacus.Validation
{
    public class RequiredRule : Rule
    {
        public RequiredRule()
            : base(Resources.RequiredRule, (item, rule) => Convert.ToString(item).Length < 1)
        {
        }
    }
}
