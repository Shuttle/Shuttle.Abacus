using System;
using System.Windows.Forms;
using Shuttle.Core.Infrastructure;

namespace Shuttle.Abacus.Shell.Core.Presentation
{
    public class ApplicationShell : IApplicationShell
    {
        private Control _shell;

        public void AssignShell(Control control)
        {
            _shell = control;
        }

        public void Invoke(Action action)
        {
            Guard.AgainstNull(_shell, "shell");

            if (_shell.InvokeRequired)
            {
                _shell.Invoke(new MethodInvoker(action));
            }
            else
            {
                action.Invoke();
            }
        }
    }
}
