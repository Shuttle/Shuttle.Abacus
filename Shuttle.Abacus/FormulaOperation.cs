using System;
using Shuttle.Core.Contract;

namespace Shuttle.Abacus
{
    public enum ValueProviderType
    {
        Argument = 1,
        Constant = 2,
        Matrix = 3,
        Formula = 4,
        Result = 5
    }

    public class FormulaOperation
    {
        public FormulaOperation(Guid id, int sequenceNumber, string operation, string valueProvider, string input)
        {
            Guard.AgainstNullOrEmptyString(operation, nameof(operation));
            Guard.AgainstNullOrEmptyString(valueProvider, nameof(valueProvider));
            Guard.AgainstNullOrEmptyString(input, nameof(input));

            Id = id;
            SequenceNumber = sequenceNumber;
            Operation = operation;
            ValueProvider = valueProvider;
            Input = input;
        }

        public Guid Id { get; }
        public int SequenceNumber { get; }
        public string Operation { get; }
        public string ValueProvider { get; }
        public string Input { get; }

        public void Perform(FormulaContext context, decimal value)
        {
            switch (Operation.ToLower())
            {
                case "addition":
                {
                    context.SetResult(context.Result + value);

                    return;
                }
            }
        }
    }
}