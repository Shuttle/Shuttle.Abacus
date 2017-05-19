using System.Drawing;

namespace Shuttle.Abacus.Shell.Core.EventArgs
{
    public class PresenterImageChangedArgs : System.EventArgs
    {
        public Image Image { get; private set; }

        public PresenterImageChangedArgs(Image image)
        {
            Image = image;
        }
    }
}
