using System;
using Shuttle.Core.Infrastructure;

namespace Shuttle.Abacus.Domain
{
    public class ArgumentAnswerValueSource : 
        IValueSource, 
        IValueSelectionHolder,
        ISpecification<IMethodContext>
    {
        private readonly Argument argument;

        public ArgumentAnswerValueSource(Argument argument)
        {
            this.argument = argument;
        }

        public string ValueSelection
        {
            get { return argument.Id.ToString("n"); }
        }

        public decimal Operand(IMethodContext methodContext, ICalculationContext calculationContext)
        {
            return Convert.ToDecimal(ContextValue(methodContext).Answer);
        }

        public string Description(decimal operand, IMethodContext methodContext)
        {
            return string.Format("{0} (from input '{1}')", ContextValue(methodContext).Description(), argument.Name);
        }

        public string Name
        {
            get { return "ArgumentAnswer"; }
        }

        public object Text
        {
            get { return argument.Name; }
        }

        public IValueSource Copy()
        {
            return new ArgumentAnswerValueSource(argument);
        }

        public ArgumentAnswer ContextValue(IMethodContext collectionContext)
        {
            return collectionContext.GetArgumentAnswer(argument.Name);
        }

        public bool IsSatisfiedBy(IMethodContext collectionMethodContext)
        {
            var result = collectionMethodContext.HasArgumentAnswer(argument.Name);

            if (!result && collectionMethodContext.LoggerEnabled)
            {
                collectionMethodContext.Log("No answer defined for argument '{0}'.", argument.Name);
            }

            return result;
        }
    }
}
