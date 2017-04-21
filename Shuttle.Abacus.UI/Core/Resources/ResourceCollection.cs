using Abacus.Infrastructure;

namespace Abacus.UI
{
    public class ResourceCollection : RichList<Resource>
    {
        public Resource this[ResourceKey key]
        {
            get { return Find(item => item.ResourceKey == key); }
        }

        public Resource FirstItem
        {
            get
            {
                return Count > 0
                           ? this[0]
                           : null;
            }
        }

        public bool Contains(ResourceKey key)
        {
            return this[key] != null;
        }
    }
}
