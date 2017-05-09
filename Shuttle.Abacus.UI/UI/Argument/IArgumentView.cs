using Shuttle.Abacus.Invariants.Core;
using Shuttle.Abacus.UI.Core.Presentation;

namespace Shuttle.Abacus.UI.UI.Argument
{
    public interface IArgumentView : IView
    {
        string ArgumentNameValue { get; set; }
        string AnswerTypeValue { get; set; }
        IRuleCollection<object> ArgumentNameRules { set; }
        IRuleCollection<object> AnswerTypeRules { set; }
        bool HasAnswerType { get; }
    }
}