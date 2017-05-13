namespace Shuttle.Abacus.Domain
{
    public class NullCalculationLogger : DefaultCalculationLogger
    {
        public override bool Enabled => false;

        public override string ToString()
        {
            return string.Empty;
        }

        public override void AppendLine()
        {
        }

        public override void AppendLine(string text)
        {
        }

        public override void AppendLine(string text, params string[] args)
        {
        }
    }
}
