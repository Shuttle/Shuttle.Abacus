using Shuttle.Abacus.Localisation;

namespace Shuttle.Abacus
{
    public class MethodResultValueSource : IValueSource, IValueSelectionHolder
    {
        private readonly Method method;

        public MethodResultValueSource(Method method)
        {
            this.method = method;
        }

        public string ValueSelection
        {
            get { return method.Id.ToString("n"); }
        }

        public decimal Operand(IMethodContext methodContext, ICalculationContext calculationContext)
        {
            using (var wrapped = methodContext.Wrapped(method.MethodName).AssignGraphNode(calculationContext.GraphNode.AddGraphNode(method.MethodName)))
            {
                if (wrapped.LoggerEnabled)
                {
                    wrapped.Log("Starting Method Calculation: {0}", method.MethodName);
                    wrapped.Log();
                }

                method.Calculate(wrapped);

                if (!wrapped.OK)
                {
                    foreach (var errorMessage in wrapped.ErrorMessages)
                    {
                        methodContext.AddErrorMessage(errorMessage);
                    }

                    return 0;
                }

                return wrapped.Total.Value;
            }
        }

        public string Description(decimal operand, IMethodContext methodContext)
        {
            return string.Format("{0} (from method result '{1}')", operand.ToString(Resources.FormatDecimal), method.MethodName);
        }

        public string Name
        {
            get { return "MethodResult"; }
        }

        public object Text
        {
            get { return method.MethodName; }
        }

        public IValueSource Copy()
        {
            return new MethodResultValueSource(method);
        }
    }
}
