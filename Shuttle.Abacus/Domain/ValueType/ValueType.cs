using System;

namespace Shuttle.Abacus.Domain
{
    public abstract class ValueType : IComparable<ValueType>
    {
        public static ValueType Null = new NullValueType();

        protected ValueType(string argumentName)
        {
            ArgumentName = argumentName;
        }

        public string ArgumentName { get; protected set; }
        public string AnswerString { get; protected set; }
        public object Answer { get; protected set; }

        public abstract string AnswerType { get; }

        public virtual bool IsNull => false;

        public abstract int CompareTo(ValueType other);

        public abstract string DisplayString();

        public virtual string Description()
        {
            return AnswerString == Convert.ToString(Answer)
                       ? AnswerString
                       : string.Format("{0} ({1})", AnswerString, Convert.ToString(Answer));
        }

        public static ValueType Create(string type, string name, string answer)
        {
            switch (type.ToLowerInvariant())
            {
                case "boolean":
                    {
                        return new BooleanValueType(name, answer);
                    }
                case "datetime":
                    {
                        return new DateTimeValueType(name, answer);
                    }
                case "decimal":
                    {
                        return new ConstantValueType(name, answer);
                    }
                case "integer":
                    {
                        return new IntegerValueType(name, answer);
                    }
                case "text":
                    {
                        return new TextValueType(name, answer);
                    }
                default:
                    {
                        throw new InvalidOperationException();
                    }
            }
        }
    }
}