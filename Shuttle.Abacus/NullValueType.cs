namespace Shuttle.Abacus
{
    public class NullValueType : ValueType
    {
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