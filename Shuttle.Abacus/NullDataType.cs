namespace Shuttle.Abacus
{
    public class NullDataType : DataType
    {
        public override string Name => "NULL";

        public override bool IsNull => true;

        public override int CompareTo(DataType other)
        {
            return 0;
        }

        public override string Text()
        {
            return "NULL";
        }
    }
}