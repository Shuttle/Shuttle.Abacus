namespace Shuttle.Abacus.Domain
{
    public interface IArgumentRules
    {
        IRuleCollection<object> ArgumentNameRules();
        IRuleCollection<object> AnswerTypeRules();
    }
}
