using System;
using Abacus.Localisation;

namespace Abacus.Validation
{
    public class IntegerRule : Rule
    {
        public IntegerRule()
            : base(Resources.IntegerRule, (item, rule) =>
                {
                    int i;

                    return !int.TryParse(Convert.ToString(item), out i);
                })
        {
        }
    }
}
