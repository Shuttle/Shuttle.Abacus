using System.Drawing;

namespace Shuttle.Abacus.UI.Core.EventArgs
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
