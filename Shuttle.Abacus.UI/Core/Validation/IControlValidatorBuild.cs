using Shuttle.Abacus.Invariants.Core;

namespace Shuttle.Abacus.Shell.Core.Validation
{
    public interface IControlValidatorBuild
    {
        IViewValidator ShouldSatisfy(IRule<object> rule);
        IViewValidator ShouldSatisfy(IRuleCollection<object> rules);
    }
}
