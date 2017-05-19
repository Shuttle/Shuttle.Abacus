using System.Collections.Generic;
using Shuttle.Abacus.Shell.Core.Resources;
using Shuttle.Core.Infrastructure;

namespace Shuttle.Abacus.Shell.Core.Clipboard
{
    public class Clipboard : IClipboard
    {
        private readonly List<ClipboardResource> clipboardResources = new List<ClipboardResource>();

        public void Replace(Resource resource)
        {
            Replace(new ClipboardResource(resource));
        }

        public void Replace(ClipboardResource clipboardResource)
        {
            Guard.AgainstNull(clipboardResource, "clipboardResource");

            Remove(clipboardResource.Resource.ResourceKey);

            clipboardResources.Add(clipboardResource);
        }

        public bool Contains(ResourceKey resourceKey)
        {
            return Find(resourceKey) != null;
        }

        public ClipboardResource Get(ResourceKey resourceKey)
        {
            var result = Find(resourceKey);

            Remove(resourceKey);

            return result;
        }

        public void Remove(ResourceKey resourceKey)
        {
            clipboardResources
                .FindAll(item => item.Resource.ResourceKey.Equals(resourceKey))
                .ForEach(item => clipboardResources.Remove(item));
        }

        public void Replace(IResourceHolder resourceHolderHolder)
        {
            Guard.AgainstNull(resourceHolderHolder, "resourceHolderHolder");

            Replace(resourceHolderHolder.Resource);
        }

        private ClipboardResource Find(ResourceKey resourceKey)
        {
            return clipboardResources.Find(item => item.Resource.ResourceKey.Equals(resourceKey));
        }
    }
}
