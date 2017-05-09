namespace Shuttle.Abacus.Messages.v1
{
    public class CreateFormulaCommand
    {
        public string Name { get; set; }
        public string MaximumFormulaName { get; set; }
        public string MinimumFormulaName { get; set; }
        public string ExecutionType { get; set; }
    }
}