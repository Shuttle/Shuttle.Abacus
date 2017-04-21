using System;

namespace Shuttle.Abacus.Domain
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
