namespace Shuttle.Abacus.Messages.v1
{
    public class RegisterTestCommand
    {
        public string Name { get; set; }
        public string ExpectedResult { get; set; }
        public string ExpectedResultType { get; set; }
        public string Comparison { get; set; }
        public string FormulaName { get; set; }
    }
}