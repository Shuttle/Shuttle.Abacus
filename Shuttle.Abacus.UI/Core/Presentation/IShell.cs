using System;
using System.Windows.Forms;

namespace Abacus.UI
{
    public interface IShell
    {
        void AssignShell(Control control);
        void Invoke(Action action);
    }
}
