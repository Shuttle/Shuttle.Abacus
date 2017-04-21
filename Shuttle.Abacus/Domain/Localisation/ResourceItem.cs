using System.Drawing;
using System.Threading;
using Shuttle.Abacus.Localisation;

namespace Shuttle.Abacus
{
    public class ResourceItem : IResourceAccessor
    {
        private readonly string imageKey;
        private readonly string textKey;

        public ResourceItem(string sharedKey) : this(sharedKey, sharedKey)
        {
        }

        public ResourceItem(string textKey, string imageKey)
        {
            this.textKey = textKey;
            this.imageKey = imageKey;
        }

        public string Text
        {
            get { return GetText(textKey); }
        }

        public Image Image
        {
            get { return GetImage(imageKey); }
        }

        public static string GetText(string key)
        {
            var result = Resources.ResourceManager.GetString("Text_" + key,
                                                             Thread.CurrentThread.CurrentUICulture);

            if (string.IsNullOrEmpty(result))
            {
                result = string.Format("[{0}]", key);
            }

            return result;
        }

        public static Image GetImage(string key)
        {
            return
                Resources.ResourceManager.GetObject("Image_" + key, Thread.CurrentThread.CurrentUICulture)
                as Image;
        }
    }
}
