using System;

namespace Shuttle.Abacus.Domain
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
