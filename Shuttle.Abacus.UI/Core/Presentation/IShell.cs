using System;
using System.Windows.Forms;

namespace Shuttle.Abacus.UI.Core.Presentation
{
    public interface IShell
    {
        void AssignShell(Control control);
        void Invoke(Action action);
    }
}
