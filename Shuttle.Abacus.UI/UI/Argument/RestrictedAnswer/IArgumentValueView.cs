using System.Collections.Generic;
using System.Windows.Forms;
using Shuttle.Abacus.DTO;
using Shuttle.Abacus.Invariants.Core;
using Shuttle.Abacus.UI.Core.Presentation;

namespace Shuttle.Abacus.UI.UI.Argument.RestrictedAnswer
{
    public interface IArgumentValueView : IView
    {
        List<string> Values { get; set; }
        IRuleCollection<object> ArgumentValueRules { set; }
        bool HasValue();
        void ShowValueError(string message);
        void RemoveSelectedItem();
        ListViewItem AddValue(string answer);
    }
}
