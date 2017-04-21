using System;
using Abacus.Localisation;

namespace Abacus.Validation
{
    public class DecimalRule : Rule
    {
        public DecimalRule()
            : base(Resources.DecimalRule, (item, rule) =>
                {
                    decimal dec;

                    return !decimal.TryParse(Convert.ToString(item), out dec);
                })
        {
        }
    }
}
