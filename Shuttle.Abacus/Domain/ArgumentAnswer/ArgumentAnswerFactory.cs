using System;

namespace Shuttle.Abacus.Domain
{
    public class ArgumentAnswerFactory : IArgumentAnswerFactory
    {
        public ArgumentAnswer Create(string type, string name, string answer)
        {
            switch (type.ToLowerInvariant())
            {
                case "boolean":
                    {
                        return new BooleanArgumentAnswer(name, answer);
                    }
                case "datetime":
                    {
                        return new DateTimeArgumentAnswer(name, answer);
                    }
                case "decimal":
                    {
                        return new DecimalArgumentAnswer(name, answer);
                    }
                case "integer":
                    {
                        return new IntegerArgumentAnswer(name, answer);
                    }
                case "text":
                    {
                        return new TextArgumentAnswer(name, answer);
                    }
                default:
                    {
                        throw new InvalidOperationException();
                    }
            }
        }
    }
}