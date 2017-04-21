using System;
using System.Drawing;

namespace Abacus.UI
{
    public class PresenterImageChangedArgs : EventArgs
    {
        public Image Image { get; private set; }

        public PresenterImageChangedArgs(Image image)
        {
            Image = image;
        }
    }
}
