using System;
using System.Collections.Generic;
using System.Text;

namespace Shuttle.Abacus.Infrastructure
{
    public abstract class AbstractResult : IAbstractResult
    {
        protected static IMapper<string, ResultMessage> mapper = new StringToResultMessageMapper();

        public List<ResultMessage> SuccessMessages { get; set; }
        public List<ResultMessage> FailureMessages { get; set; }

        protected AbstractResult()
        {
            FailureMessages = new List<ResultMessage>();
            SuccessMessages = new List<ResultMessage>();

            Aborted = false;
        }

        public bool OK
        {
            get { return FailureMessages.Count == 0; }
        }

        public bool Aborted { get; private set; }

        public void SetAbort()
        {
            Aborted = true;
        }

        public void Merge(IAbstractResult result)
        {
            SuccessMessages.AddRange(result.SuccessMessages);
            FailureMessages.AddRange(result.FailureMessages);

            if (result.Aborted)
            {
                SetAbort();
            }
        }

        public bool HasMessages
        {
            get { return HasFailureMessages || HasSuccessMessages; }
        }

        public bool HasFailureMessages
        {
            get { return FailureMessages.Count > 0; }
        }

        public bool HasSuccessMessages
        {
            get { return SuccessMessages.Count > 0; }
        }

        public override string ToString()
        {
            var result = new StringBuilder();

            if (HasFailureMessages)
            {
                result.AppendLine("Failure Messages:");

                AppendMessages(result, FailureMessages);
            }

            if (HasSuccessMessages)
            {
                result.AppendLine("Success Messages:");

                AppendMessages(result, SuccessMessages);
            }

            return result.ToString();
        }

        private static void AppendMessages(StringBuilder result, IEnumerable<ResultMessage> messages)
        {
            AppendMessages(0, result, messages);
        }

        private static void AppendMessages(int indent, StringBuilder result, IEnumerable<ResultMessage> messages)
        {
            foreach (var message in messages)
            {
                result.AppendFormat("{0} - {1}", new String(' ', indent * 3), message);
                result.AppendLine();

                AppendMessages(indent + 1, result, message.Messages);
            }
        }
    }
}
