namespace Shuttle.Abacus.Domain
{
    public class NullValueType : ValueType
    {
        public NullValueType() : base("NULL")
        {
        }

        public override string AnswerType => "NULL";

        public override bool IsNull => true;

        public override int CompareTo(ValueType other)
        {
            return 0;
        }

        public override string DisplayString()
        {
            return "NULL";
        }
    }
}