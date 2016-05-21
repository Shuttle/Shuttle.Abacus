namespace Shuttle.Abacus
{
    public interface IArgumentRules
    {
        IRuleCollection<object> ArgumentNameRules();
        IRuleCollection<object> AnswerTypeRules();
    }
}
