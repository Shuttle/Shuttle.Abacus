using System.Collections.Generic;
using System.Windows.Forms;
using Shuttle.Abacus.DTO;
using Shuttle.Abacus.Invariants.Core;
using Shuttle.Abacus.UI.Core.Presentation;

namespace Shuttle.Abacus.UI.UI.Argument.RestrictedAnswer
{
    public interface IArgumentRestrictedAnswerView : IView
    {
        List<string> Answers { get; set; }
        IRuleCollection<object> ArgumentRestrictedAnswerRules { set; }
        bool HasAnswer();
        void ShowAnswerError(string message);
        void RemoveSelectedItem();
        ListViewItem AddRestrictedAnswer(string answer);
    }
}
