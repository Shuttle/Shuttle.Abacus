using Abacus.Validation;

namespace Abacus.UI
{
    public interface IControlValidatorBuild
    {
        IViewValidator ShouldSatisfy(IRule<object> rule);
        IViewValidator ShouldSatisfy(IRuleCollection<object> rules);
    }
}
