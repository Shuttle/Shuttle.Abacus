using System.Collections.Generic;
using System.Linq;
using Shuttle.Core.Infrastructure;

namespace Shuttle.Abacus.Domain
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
            IList<IRule<T>> brokenRules = Rules.AddParameterValue(rule => rule.IsBrokenBy(item)).ToList();

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