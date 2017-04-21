using System.Drawing;
using System.Drawing.Drawing2D;

namespace Shuttle.Abacus.Infrastructure
{
    public class ImageService : IImageService
    {
        public Icon IconFrom(Image image)
        {
            const int size = 16;

            var square = new Bitmap(size, size);
            var g = Graphics.FromImage(square);

            int x, y, w, h;

            var r = image.Width/(float) image.Height;

            if (r > 1)
            {
                w = size;
                h = (int) (size/r);
                x = 0;
                y = (size - h)/2;
            }
            else
            {
                w = (int) (size*r);
                h = size;
                y = 0;
                x = (size - w)/2;
            }

            g.InterpolationMode = InterpolationMode.HighQualityBicubic;

            g.DrawImage(image, x, y, w, h);

            g.Flush();

            return Icon.FromHandle(square.GetHicon());
        }
    }
}
