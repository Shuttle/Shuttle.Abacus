using System;
using System.Collections.Generic;

namespace Shuttle.Abacus.Infrastructure
{
    public static class ExceptionExtensions
    {
        public static IList<string> Messages(this Exception ex)
        {
            IList<string> messages = new List<string>();

            var i = 0;

            var enumerator = ex;

            while (enumerator != null)
            {
                messages.Add(string.Format("{0}{1}", new string(' ', i) + ((i > 0)
                                                                               ? "+->"
                                                                               : string.Empty), enumerator.Message));

                enumerator = enumerator.InnerException;

                i++;
            }

            return messages;
        }
    }
}
