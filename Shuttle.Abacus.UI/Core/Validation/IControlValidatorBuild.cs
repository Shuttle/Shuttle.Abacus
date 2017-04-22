namespace Shuttle.Abacus.UI.Core.Validation
{
    public interface IControlValidatorBuild
    {
        IViewValidator ShouldSatisfy(IRule<object> rule);
        IViewValidator ShouldSatisfy(IRuleCollection<object> rules);
    }
}
