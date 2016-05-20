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

using Shuttle.Core.Infrastructure;

namespace Shuttle.Abacus
{
    public class TextArgumentAnswer : ArgumentAnswer
    {
        public TextArgumentAnswer(string argumentName, string text) : base(argumentName)
        {
            AnswerString = text;
            Answer = text;
        }

        public override string AnswerType
        {
            get { return "Text"; }
        }

        public override int CompareTo(ArgumentAnswer other)
        {
            Guard.AgainstNull(other, "other");

            return AnswerString.CompareTo(other.AnswerString);
        }

        public override string DisplayString()
        {
            return AnswerString;
        }
    }
}