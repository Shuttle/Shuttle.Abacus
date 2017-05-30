using System;
using Shuttle.Abacus.Infrastructure;

namespace Shuttle.Abacus.Invariants.Values
{
    public class DateValueTypeValidator : IValueTypeValidator
    {
        public string Type => "Date";

        public IResult Validate(string value)
        {
            var result = Result.Create();

            DateTime date;

            if (!DateTime.TryParse(value, out date))
            {
                result.AddFailureMessage("Please enter an date/time value.");
            }

            return result;
        }
    }
}