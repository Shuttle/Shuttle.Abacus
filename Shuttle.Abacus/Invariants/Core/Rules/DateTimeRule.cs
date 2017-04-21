using System;
using Abacus.Localisation;

namespace Abacus.Validation
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
