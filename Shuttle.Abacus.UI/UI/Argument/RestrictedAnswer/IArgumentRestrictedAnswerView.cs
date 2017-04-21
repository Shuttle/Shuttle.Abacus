using System.Collections.Generic;
using System.Windows.Forms;
using Abacus.DTO;
using Abacus.Validation;

namespace Abacus.UI
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
