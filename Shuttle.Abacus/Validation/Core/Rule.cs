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
    public delegate bool RulePredicate<T>(T item, IRule<T> rule);

    public class Rule<T> : IRule<T> where T : class
    {
        private readonly RulePredicate<T> predicate;

        private string rawMessage;

        public Rule(string message, RulePredicate<T> predicate)
        {
            RawMessage = message;

            this.predicate = predicate;
        }

        protected string RawMessage
        {
            get { return rawMessage; }
            set
            {
                rawMessage = value;
                Message = new RuleMessage(rawMessage);
            }
        }

        public bool IsBrokenBy(T item)
        {
            return predicate(item, this);
        }

        public RuleMessage Message { get; protected set; }

        public void SetMessageArguments(params string[] args)
        {
            Message.Text = string.Format(RawMessage, args);
        }

        public void SetException(Exception ex)
        {
            Message.Text = ex.Message;
        }
    }

    public class Rule : Rule<object>
    {
        public Rule(string message, RulePredicate<object> predicate) : base(message, predicate)
        {
        }

        public static IRuleCollectionBuilder With()
        {
            return new RuleCollectionBuilder();
        }
    }
}
