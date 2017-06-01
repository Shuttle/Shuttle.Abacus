using Shuttle.Abacus.Invariants.Core;
using Shuttle.Abacus.Shell.Core.Presentation;

namespace Shuttle.Abacus.Shell.UI.Argument
{
    public interface IArgumentView : IView
    {
        string ArgumentNameValue { get; set; }
        string ValueTypeValue { get; set; }
        IRuleCollection<object> ArgumentNameRules { set; }
        IRuleCollection<object> AnswerTypeRules { set; }
    }
}