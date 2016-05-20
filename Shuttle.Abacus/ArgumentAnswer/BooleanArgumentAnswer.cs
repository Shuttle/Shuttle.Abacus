/*
    This file forms part of Shuttle.Abacus.

    Shuttle.Abacus - A constraint-based calculation engine.
    Copyright (C) 2016  Eben Roux

    This program is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with this program.  If not, see <http://www.gnu.org/licenses/>.
*/

using System;
using Shuttle.Core.Infrastructure;

namespace Shuttle.Abacus
{
    public class BooleanArgumentAnswer : ArgumentAnswer
    {
        private readonly bool value;

        public BooleanArgumentAnswer(string argumentName, bool value)
            : base(argumentName)
        {
            Answer = value;
            AnswerString = Convert.ToString(value);
        }

        public BooleanArgumentAnswer(string argumentName, string text)
            : base(argumentName)
        {
            AnswerString = text;

            switch (text.ToLower())
            {
                case "1":
                case "yes":
                case "true":
                case "y":
                case "t":
                    {
                        value = true;
                        break;
                    }
                default:
                    {
                        value = false;
                        break;
                    }
            }

            Answer = value;
        }

        public override string AnswerType
        {
            get { return "Boolean"; }
        }

        public override int CompareTo(ArgumentAnswer other)
        {
            Guard.AgainstNull(other, "other");

            bool otherValue;

            if (!bool.TryParse(other.AnswerString, out otherValue))
            {
                throw new InvalidCastException(string.Format(Resources.IncompatibleCalculationValues, GetType().Name,
                                                             other.GetType().Name));
            }

            return value.CompareTo(otherValue);
        }

        public override string DisplayString()
        {
            return value ? "Yes" : "No";
        }
    }
}