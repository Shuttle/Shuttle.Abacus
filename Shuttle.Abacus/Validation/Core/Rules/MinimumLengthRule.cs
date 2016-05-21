using System;
using Shuttle.Abacus.Localisation;

namespace Shuttle.Abacus
{
    public class MinimumLengthRule : Rule
    {
        public MinimumLengthRule(int minimumLength)
            : base(Resources.MinimumLengthRule, (item, rule) =>
                                                    {
                                                        var value = Convert.ToString(item);

                                                        var result = value.Length < minimumLength;

                                                        if (result)
                                                        {
                                                            rule.SetMessageArguments(Convert.ToString(minimumLength));
                                                        }

                                                        return result;
                                                    })
        {
        }
    }
}
