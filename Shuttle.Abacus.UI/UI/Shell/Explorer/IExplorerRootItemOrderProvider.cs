using Shuttle.Abacus.Shell.Core.Resources;

namespace Shuttle.Abacus.Shell.UI.Shell.Explorer
{
    public interface IExplorerRootItemOrderProvider
    {
        ResourceCollection OrderedList(ResourceCollection unorderedItems);
    }
}
