using System.Collections.Generic;
using Shuttle.Abacus.Infrastructure;

namespace Shuttle.Abacus.Messages.v1
{
    public class ReplyMessage
    {
        public ReplyMessage()
        {
            SuccessMessages = new List<ResultMessage>();
            FailureMessages = new List<ResultMessage>();
        }

        public List<ResultMessage> SuccessMessages { get; set; }
        public List<ResultMessage> FailureMessages { get; set; }
    }
}