using System.Collections.Generic;
using System.Windows.Forms;
using Shuttle.Abacus.Invariants.Core;
using Shuttle.Abacus.UI.Core.Presentation;
using Shuttle.Abacus.UI.Models;

namespace Shuttle.Abacus.UI.UI.Test
{
    public interface ITestView : IView
    {
        string DescriptionValue { get; set; }

        string ExpectedResultValue { get; set; }

        IEnumerable<ArgumentValueModel> ArgumentAnswers { get; }

        ArgumentModel ArgumentModel { get; }
        bool HasArgument { get; }
        bool HasAnswer { get; }
        string AnswerValue { get; }

        IRuleCollection<object> DescriptionRules { set; }
        IRuleCollection<object> ExpectedResultRules { set; }

        ComboBox ValueSelectionControl { get; }
        TextBox FormattedControl { get; }
        void PopulateArgumentValues(IEnumerable<string> answers);
        void PopulateArguments(IEnumerable<ArgumentModel> arguments);
        void ShowArgumentError();
        void ShowAnswerError(string message);
        void AddArgumentValue(ArgumentModel argument, string answer);
        void AddArgument(ArgumentModel model);
    }
}