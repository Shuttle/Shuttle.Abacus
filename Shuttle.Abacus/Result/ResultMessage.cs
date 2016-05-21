using System.Collections.Generic;

namespace Shuttle.Abacus
{
    public class ResultMessage
    {
        public ResultMessage()
        {
        }

        public override string ToString()
        {
            return Message;
        }

        public string Message { get; private set; }

        public ResultMessage(string message)
        {
            Message = message;

            Messages = new List<ResultMessage>();
        }

        public List<ResultMessage> Messages { get; set; }
    }
}
