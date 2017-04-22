using System.Windows.Forms;

namespace Shuttle.Abacus.UI
{
    public class UIService : IUIService
    {
        public bool Confirm(string message)
        {
            return MessageBox.Show(message, "Abacus - Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes;
        }
    }
}
