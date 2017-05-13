namespace Shuttle.Abacus.Domain
{
    public class NullArgumentAnswer : ArgumentAnswer
    {
        public NullArgumentAnswer() : base("NULL")
        {
        }

        public override string AnswerType => "NULL";

        public override bool IsNull => true;

        public override int CompareTo(ArgumentAnswer other)
        {
            return 0;
        }

        public override string DisplayString()
        {
            return "NULL";
        }
    }
}