using System;

namespace Shuttle.Abacus.Domain
{
    public abstract class ArgumentAnswer : IComparable<ArgumentAnswer>
    {
        public static ArgumentAnswer Null = new NullArgumentAnswer();

        protected ArgumentAnswer(string argumentName)
        {
            ArgumentName = argumentName;
        }

        public string ArgumentName { get; protected set; }
        public string AnswerString { get; protected set; }
        public object Answer { get; protected set; }

        public abstract string AnswerType { get; }

        public virtual bool IsNull => false;

        public abstract int CompareTo(ArgumentAnswer other);

        public abstract string DisplayString();

        public virtual string Description()
        {
            return AnswerString == Convert.ToString(Answer)
                       ? AnswerString
                       : string.Format("{0} ({1})", AnswerString, Convert.ToString(Answer));
        }
    }
}