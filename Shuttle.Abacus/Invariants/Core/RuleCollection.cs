using System.Collections.Generic;
using Shuttle.Abacus.Domain;
using Shuttle.Abacus.Infrastructure;
using Shuttle.Core.Infrastructure;

namespace Shuttle.Abacus.Invariants.Core
{
    public class RuleCollection<T> : IRuleCollection<T>
    {
        internal IList<IRule<T>> rules;

        public RuleCollection(params IRule<T>[] rules)
            : this(new List<IRule<T>>(rules))
        {
        }

        public RuleCollection(IList<IRule<T>> rules)
        {
            this.rules = rules;
        }

        public RuleCollection(IEnumerable<IRule<T>> rules)
        {
            this.rules = new List<IRule<T>>();

            rules.ForEach(this.rules.Add);
        }

        public RuleCollection(IRuleCollection<T> rules)
        {
            this.rules = new List<IRule<T>>();

            rules.AssignTo(this.rules);
        }

        public int Count
        {
            get { return rules.Count; }
        }

        public IList<RuleMessage> Messages
        {
            get
            {
                return
                    new List<IRule<T>>(rules).ConvertAll(
                        rule => rule.Message);
            }
        }

        public bool IsEmpty
        {
            get { return (rules.Count == 0); }
        }

        public IRuleCollection<T> BrokenBy(T item)
        {
            IList<IRule<T>> brokenRules = new List<IRule<T>>();
            foreach (var rule in rules)
            {
                if (rule.IsBrokenBy(item))
                {
                    brokenRules.Add(rule);
                }
            }
            return new RuleCollection<T>(brokenRules);
        }

        public IResult ToResult()
        {
            var result = Result.Create();

            if (!IsEmpty)
            {
                foreach (var rule in rules)
                {
                    var message = new ResultMessage(rule.Message.Text);

                    message.Messages.AddRange(
                        rule.Message.DetailMessages.Map(StringToResultMessageMapper.Instance));

                    result.AddFailureMessage(message);
                }
            }

            return result;
        }

        public void AssignTo(IList<IRule<T>> list)
        {
            Guard.AgainstNull(list, "list");

            rules.ForEach(list.Add);
        }

        public void Enforce(T item)
        {
            foreach (var rule in rules)
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
            base.rules = new List<IRule<object>>(rules);
        }
    }
}
