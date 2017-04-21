using System.Drawing;
using System.Windows.Forms;

namespace Abacus.Shell
{
    public partial class Splash : Form
    {
        public Splash()
        {
            InitializeComponent();
        }

        public void ShowStatus(string heading, string message)
        {
            var g = Graphics.FromHwnd(Handle);

            var chopper = 0;

            var full = GetFullMessage(heading, message, chopper);

            while (g.MeasureString(full, Font).Width > StatusLabel.Width)
            {
                chopper++;

                full = GetFullMessage(heading, message, chopper);
            }

            StatusLabel.Text = full;
        }

        private static string GetFullMessage(string header, string message, int chopper)
        {
            return string.Format("{0}: {1}", header, message.Substring(chopper));
        }
    }
}
