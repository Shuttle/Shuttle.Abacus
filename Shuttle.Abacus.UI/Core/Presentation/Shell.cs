using System;
using System.Windows.Forms;

namespace Shuttle.Abacus.UI.Core.Presentation
{
    public class Shell : IShell
    {
        private Control shell;

        public void AssignShell(Control control)
        {
            shell = control;
        }

        public void Invoke(Action action)
        {
            Guard.AgainstNullDependency(shell, "shell");

            if (shell.InvokeRequired)
            {
                shell.Invoke(new MethodInvoker(action));
            }
            else
            {
                action.Invoke();
            }
        }
    }
}
