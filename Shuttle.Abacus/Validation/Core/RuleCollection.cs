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

using System.Collections.Generic;
using System.Linq;
using Shuttle.Core.Infrastructure;

namespace Shuttle.Abacus
{
    public class RuleCollection<T> : IRuleCollection<T>
    {
        internal IList<IRule<T>> Rules;

        public RuleCollection(params IRule<T>[] rules)
            : this(new List<IRule<T>>(rules))
        {
        }

        public RuleCollection(IList<IRule<T>> rules)
        {
            Rules = rules;
        }

        public RuleCollection(IEnumerable<IRule<T>> rules)
        {
            Rules = new List<IRule<T>>();

            foreach (var rule in rules)
            {
                Rules.Add(rule);
            }
        }

        public RuleCollection(IRuleCollection<T> rules)
        {
            Rules = new List<IRule<T>>();

            rules.AssignTo(Rules);
        }

        public int Count
        {
            get { return Rules.Count; }
        }

        public IList<RuleMessage> Messages
        {
            get
            {
                return
                    new List<IRule<T>>(Rules).ConvertAll(
                        rule => rule.Message);
            }
        }

        public bool IsEmpty
        {
            get { return Rules.Count == 0; }
        }

        public IRuleCollection<T> BrokenBy(T item)
        {
            IList<IRule<T>> brokenRules = Rules.Where(rule => rule.IsBrokenBy(item)).ToList();

            return new RuleCollection<T>(brokenRules);
        }

        public IResult ToResult()
        {
            var result = Result.Create();

            if (!IsEmpty)
            {
                foreach (var rule in Rules)
                {
                    var message = new ResultMessage(rule.Message.Text);

                    foreach (var detailMessage in rule.Message.DetailMessages)
                    {
                        message.Messages.Add(StringToResultMessageMapper.Instance.MapFrom(detailMessage));
                    }

                    result.AddFailureMessage(message);
                }
            }

            return result;
        }

        public void AssignTo(IList<IRule<T>> list)
        {
            Guard.AgainstNull(list, "list");

            foreach (var rule in Rules)
            {
                list.Add(rule);
            }
        }

        public void Enforce(T item)
        {
            foreach (var rule in Rules)
            {
                if (rule.IsBrokenBy(item))
                {
                    throw new InvariantException(rule.Message.Text);
                }
            }
        }
    }

    public class RuleCollection : RuleCollection<object>
    {
        public RuleCollection(IRuleCollection<object> rules) : base(rules)
        {
        }

        public RuleCollection(params IRule<object>[] rules)
            : this(new List<IRule<object>>(rules))
        {
        }

        public RuleCollection(IEnumerable<IRule<object>> rules)
        {
            Rules = new List<IRule<object>>(rules);
        }
    }
}