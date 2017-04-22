using System.Collections.Generic;
using System.Windows.Forms;
using Shuttle.Abacus.UI.Core.Presentation;

namespace Shuttle.Abacus.UI.UI.Argument.RestrictedAnswer
{
    public interface IArgumentRestrictedAnswerView : IView
    {
        List<ArgumentRestrictedAnswerDTO> ArgumentAnswerCatalog { get; set; }
        IRuleCollection<object> ArgumentRestrictedAnswerRules { set; }
        bool HasAnswer();
        void ShowAnswerError(string message);
        void RemoveSelectedItem();
        ListViewItem AddRestrictedAnswer(string answer);
    }
}
