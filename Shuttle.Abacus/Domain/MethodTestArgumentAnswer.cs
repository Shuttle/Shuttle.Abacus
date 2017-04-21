using System;

namespace Shuttle.Abacus.Domain
{
    public class MethodTestArgumentAnswer
    {
        public MethodTestArgumentAnswer(Guid argumentId, string argumentName, string answerType, string answer)
        {
            ArgumentId = argumentId;
            ArgumentName = argumentName;
            AnswerType = answerType;
            Answer = answer;
        }

        public Guid ArgumentId { get; private set; }
        public string ArgumentName { get; private set; }
        public string AnswerType { get; private set; }
        public string Answer { get; private set; }
    }
}
