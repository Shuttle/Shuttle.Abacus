namespace Abacus.Validation
{
    public interface IArgumentRules
    {
        IRuleCollection<object> ArgumentNameRules();
        IRuleCollection<object> AnswerTypeRules();
    }
}
