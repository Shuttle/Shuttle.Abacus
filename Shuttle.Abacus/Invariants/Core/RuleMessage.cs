using System.Collections.Generic;
using System.Collections.ObjectModel;
using Shuttle.Abacus.Infrastructure;

namespace Shuttle.Abacus.Invariants.Core
{
    public class RuleMessage
    {
        private readonly List<string> detailMessages = new List<string>();

        public RuleMessage(string message)
        {
            Text = message;
        }

        public string Text { get; internal set; }

        public IEnumerable<string> DetailMessages => new ReadOnlyCollection<string>(detailMessages);

        public void AddDetailMessage(string message)
        {
            detailMessages.Add(message);
        }

        public void AddDetailMessages(IEnumerable<string> messages)
        {
            detailMessages.AddRange(messages);
        }

        public void AddDetailMessages(IEnumerable<RuleMessage> messages)
        {
            messages.ForEach(message => detailMessages.Add(message.Text));
        }
    }
}
