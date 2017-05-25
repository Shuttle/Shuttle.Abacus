namespace Shuttle.Abacus
{
    public class ConstraintViolation
    {
        public ConstraintViolation(string argumentName, string argumentValue, string comparison, string constraintValue)
        {
            ArgumentName = argumentName;
            ArgumentValue = argumentValue;
            Comparison = comparison;
            ConstraintValue = constraintValue;
        }

        public string ArgumentName { get; }
        public string ArgumentValue { get; }
        public string Comparison { get; }
        public string ConstraintValue { get; }
    }
}