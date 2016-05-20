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

namespace Shuttle.Abacus
{
    public abstract class ArgumentAnswer : IComparable<ArgumentAnswer>
    {
        public static ArgumentAnswer Null = new NullArgumentAnswer();

        protected ArgumentAnswer(string argumentName)
        {
            ArgumentName = argumentName;
        }

        public string ArgumentName { get; protected set; }
        public string AnswerString { get; protected set; }
        public object Answer { get; protected set; }

        public abstract string AnswerType { get; }

        public virtual bool IsNull
        {
            get { return false; }
        }

        public abstract int CompareTo(ArgumentAnswer other);

        public abstract string DisplayString();

        public virtual string Description()
        {
            return AnswerString == Convert.ToString(Answer)
                       ? AnswerString
                       : string.Format("{0} ({1})", AnswerString, Convert.ToString(Answer));
        }
    }
}