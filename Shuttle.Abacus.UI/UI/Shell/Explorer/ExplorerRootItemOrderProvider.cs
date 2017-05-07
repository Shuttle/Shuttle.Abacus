using System.Collections.Generic;
using Shuttle.Abacus.UI.Core.Resources;

namespace Shuttle.Abacus.UI.UI.Shell.Explorer
{
    public class ExplorerRootItemOrderProvider : IExplorerRootItemOrderProvider
    {
        private readonly List<ResourceKey> orderedKeys;

        public ExplorerRootItemOrderProvider()
        {
            orderedKeys = new List<ResourceKey>
                          {
                              ResourceKeys.Formula, ResourceKeys.Test, ResourceKeys.DecimalTable, ResourceKeys.Argument
                          };
        }

        public ResourceCollection OrderedList(ResourceCollection unorderedItems)
        {
            var result = new ResourceCollection();

            foreach (var orderedKey in orderedKeys)
            {
                var key = orderedKey;

                var item = unorderedItems.Find(candidate => candidate.ResourceKey.Equals(key));

                if (item != null)
                {
                    result.Add(item);
                }
            }

            unorderedItems.ForEach(item =>
                {
                    if (!orderedKeys.Contains(item.ResourceKey))
                    {
                        result.Add(item);
                    }
                });

            return result;
        }
    }
}
