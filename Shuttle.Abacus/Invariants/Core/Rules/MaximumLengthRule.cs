using System;
using Shuttle.Abacus.Localisation;

namespace Shuttle.Abacus.Invariants.Core.Rules
{
    public class MaximumLengthRule : Rule
    {
        public MaximumLengthRule(int maximumLength)
            : base(Resources.MaximumLengthRule, (item, rule) =>
                                                    {
                                                        var value = Convert.ToString(item);

                                                        var result = value.Length > maximumLength;

                                                        if (result)
                                                        {
                                                            rule.SetMessageArguments(Convert.ToString(maximumLength));
                                                        }

                                                        return result;
                                                    })
        {
        }
    }
}
