using System;
using Shuttle.Abacus.Localisation;

namespace Shuttle.Abacus.Invariants.Core.Rules
{
    public class BooleanRule : Rule
    {
        public BooleanRule()
            : base(Resources.BooleanRule, (item, rule) =>
            {
                bool b;

                return !bool.TryParse(Convert.ToString(item), out b);
            })
        {
        }
    }
}