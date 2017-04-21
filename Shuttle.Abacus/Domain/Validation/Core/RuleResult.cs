namespace Shuttle.Abacus
{
    public class RuleResult : IRuleResult
    {
        public RuleResult(ResultMessage rootMessage)
        {
            RootMessage = rootMessage;
        }

        public RuleResult() : this (null)
        {
        }

        public bool OK
        {
            get { return RootMessage == null; }
        }

        public ResultMessage RootMessage { get; private set; }
        public IResult ToResult()
        {
            return OK ? Result.Create() : Result.Create().AddFailureMessage(RootMessage);
        }
    }
}
