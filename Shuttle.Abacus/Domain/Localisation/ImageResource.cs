using System.Drawing;

namespace Shuttle.Abacus
{
    public class ImageResource
    {
        public string Key { get; private set; }
        public Image Image { get; private set; }

        public ImageResource(string key, Image image)
        {
            Key = key;
            Image = image;
        }
    }
}
