using System;
using Shuttle.Abacus.Localisation;

namespace Shuttle.Abacus
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
