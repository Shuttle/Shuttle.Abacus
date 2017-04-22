using Shuttle.Abacus.UI.Core.Resources;

namespace Shuttle.Abacus.UI.UI.Shell.Explorer
{
    public interface IExplorerRootItemOrderProvider
    {
        ResourceCollection OrderedList(ResourceCollection unorderedItems);
    }
}
