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
    public class IntegerArgumentAnswer : ArgumentAnswer
    {
        private readonly int value;

        public IntegerArgumentAnswer(string argumentName, string text) : base(argumentName)
        {
            AnswerString = text;

            value = int.Parse(text);

            Answer = value;
        }

        public IntegerArgumentAnswer(string argumentName, int value) : base(argumentName)
        {
            this.value = value;

            Answer = value;
            AnswerString = Convert.ToString(value);
        }

        public override string AnswerType
        {
            get { return "Integer"; }
        }

        public override int CompareTo(ArgumentAnswer other)
        {
            Guard.AgainstNull(other, "other");

            int otherValue;

            if (!int.TryParse(other.AnswerString, out otherValue))
            {
                throw new InvalidCastException(string.Format(Resources.IncompatibleCalculationValues, GetType().Name,
                                                             other.GetType().Name));
            }

            return value.CompareTo(otherValue);
        }

        public override string DisplayString()
        {
            return value.ToString("#,##0");
        }
    }
}