using System;
using Shuttle.Core.Contract;

namespace Shuttle.Abacus
{
    public enum ValueProviderType
    {
        Argument = 1,
        Decimal = 2,
        Matrix = 3,
        Formula = 4,
        Result = 5
    }

    public class FormulaOperation
    {
        public FormulaOperation(Guid id, int sequenceNumber, string operation, string valueProviderName, string inputParameter)
        {
            Guard.AgainstNullOrEmptyString(operation, nameof(operation));
            Guard.AgainstNullOrEmptyString(valueProviderName, nameof(valueProviderName));
            Guard.AgainstNullOrEmptyString(inputParameter, nameof(inputParameter));

            Id = id;
            SequenceNumber = sequenceNumber;
            Operation = operation;
            ValueProviderName = valueProviderName;
            InputParameter = inputParameter;
        }

        public Guid Id { get; }
        public int SequenceNumber { get; }
        public string Operation { get; }
        public string ValueProviderName { get; }
        public string InputParameter { get; }

        public void Perform(FormulaContext context, decimal value)
        {
            switch (Operation.ToLower())
            {
                case "addition":
                {
                    context.SetResult(context.Result + value);

                    return;
                }
                case "subtraction":
                {
                    context.SetResult(context.Result - value);

                    return;
                }
                case "multiplication":
                {
                    context.SetResult(context.Result * value);

                    return;
                }
                case "division":
                {
                    context.SetResult(context.Result / value);

                    return;
                }
                case "rounding":
                {
                    context.SetResult(Math.Round(context.Result, (int)value));

                    return;
                }
            }
        }
    }
}