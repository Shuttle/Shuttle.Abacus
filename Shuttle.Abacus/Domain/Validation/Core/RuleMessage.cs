using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Shuttle.Abacus.Domain
{
    public class RuleMessage
    {
        private readonly List<string> detailMessages = new List<string>();

        public RuleMessage(string message)
        {
            Text = message;
        }

        public string Text { get; internal set; }

        public IEnumerable<string> DetailMessages
        {
            get { return new ReadOnlyCollection<string>(detailMessages); }
        }

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
            foreach (var message in messages)
            {
                detailMessages.Add(message.Text);
            }
        }
    }
}
