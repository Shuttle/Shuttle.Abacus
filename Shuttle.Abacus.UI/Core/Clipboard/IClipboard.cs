using Shuttle.Abacus.Shell.Core.Resources;

namespace Shuttle.Abacus.Shell.Core.Clipboard
{
    public interface IClipboard
    {
        void Replace(Resource resource);
        void Replace(IResourceHolder resourceHolderHolder);
        bool Contains(ResourceKey resourceKey);
        ClipboardResource Get(ResourceKey resourceKey);
        void Remove(ResourceKey resourceKey);
        void Replace(ClipboardResource clipboardResource);
    }
}
