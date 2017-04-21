namespace Shuttle.Abacus
{
    public class GraphNodeArgument
    {
        public GraphNodeArgument(Argument argument, string format)
        {
            Argument = argument;
            Format = format;
        }

        public Argument Argument { get; private set; }
        public string Format { get; private set; }

        public string DisplayString(IMethodContext methodContext)
        {
            if (!methodContext.HasArgumentAnswer(Argument.Name))
            {
                return string.Empty;
            }

            var answer = methodContext.GetArgumentAnswer(Argument.Name);

            return string.Format(Format, Argument.Name, answer.DisplayString());
        }
    }
}
