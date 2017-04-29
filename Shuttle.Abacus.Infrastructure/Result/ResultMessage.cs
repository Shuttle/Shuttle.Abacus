using System.Collections.Generic;

namespace Shuttle.Abacus.Infrastructure
{
    public class ResultMessage
    {
        public ResultMessage()
        {
        }

        public ResultMessage(string message)
        {
            Message = message;

            Messages = new List<ResultMessage>();
        }

        public string Message { get; set; }

        public List<ResultMessage> Messages { get; set; }

        public override string ToString()
        {
            return Message;
        }
    }
}