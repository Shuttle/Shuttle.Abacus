using System;
using Shuttle.Abacus.Localisation;

namespace Shuttle.Abacus.Invariants.Core.Rules
{
    public class DateTimeRule : Rule
    {
        public DateTimeRule()
            : base(Resources.DateTimeRule, (item, rule) =>
                {
                    DateTime dt;

                    return !DateTime.TryParse(Convert.ToString(item), out dt);
                })
        {
        }
    }
}
