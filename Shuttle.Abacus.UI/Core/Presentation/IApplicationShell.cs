using System;
using System.Windows.Forms;

namespace Shuttle.Abacus.Shell.Core.Presentation
{
    public interface IApplicationShell
    {
        void AssignShell(Control control);
        void Invoke(Action action);
    }
}
