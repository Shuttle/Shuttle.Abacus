using System;

namespace Shuttle.Abacus.Domain
{
    public class OperationFactory : IOperationFactory
    {
        public Operation Create(string name, IValueSource valueSource)
        {
            switch (name.ToLowerInvariant())
            {
                case "addition":
                {
                    return new AdditionOperation(valueSource);
                }
                case "division":
                {
                    return new DivisionOperation(valueSource);
                }
                case "multiplication":
                {
                    return new MultiplicationOperation(valueSource);
                }
                case "percentage":
                {
                    return new PercentageOperation(valueSource);
                }
                case "rounding":
                {
                    return new RoundingOperation(valueSource);
                }
                case "squareroot":
                {
                    return new SquareRootOperation(valueSource);
                }
                case "subtraction":
                {
                    return new SubtractionOperation(valueSource);
                }
                default:
                {
                    throw new InvalidOperationException();
                }
            }
        }
    }
}