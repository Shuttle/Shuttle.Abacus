using System;

namespace Abacus.UI
{
    public interface IExplorerRootItemOrderProvider
    {
        ResourceCollection OrderedList(ResourceCollection unorderedItems);
    }
}
