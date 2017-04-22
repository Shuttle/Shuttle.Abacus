using System.Collections.Generic;
using Shuttle.Abacus.UI.Messages.Core;
using Shuttle.Abacus.UI.Messages.SystemUser;

namespace Shuttle.Abacus.UI.Navigation
{
    public class DefaultNavigationMap : INavigationMap
    {
        private readonly INavigationItemFactory factory;

        public DefaultNavigationMap(INavigationItemFactory factory)
        {
            this.factory = factory;

            items = new List<INavigationItem> {File(), Administration(), View(), Help()};
        }

        private static List<INavigationItem> items;

        public IEnumerable<INavigationItem> Items
        {
            get { return items; }
        }

        public IEnumerable<INavigationItem> SecuredItems(IPermissionCollection permissions)
        {
            return SecuredItems(Items, permissions);
        }

        private static IEnumerable<INavigationItem> SecuredItems(IEnumerable<INavigationItem> secureThese,
                                                                 IPermissionCollection permissions)
        {
            var result = new List<INavigationItem>();

            foreach (var item in secureThese)
            {
                var copy = item.Copy();

                if (item.GraphHasPermittedItems(permissions))
                {
                    result.Add(copy);
                }

                copy.AddRange(SecuredItems(item.Items, permissions));
            }

            return result;
        }

        public INavigationItem File()
        {
            var file = new NavigationItem(new ResourceItem("File"));

            
            file.Add(factory.Create<ApplicationExitMessage>());

            return file;
        }

        public INavigationItem Administration()
        {
            var item = new NavigationItem(new ResourceItem("Administration"));

            var users = new NavigationItem(new ResourceItem("Users", "SystemUser"));

            users.Add(factory.Create<ListSystemUserMessage>());
            users.Add(factory.Create<NewSystemUserMessage>());

            item.Add(users);

            return item;
        }

        public INavigationItem View()
        {
            var item = new NavigationItem(new ResourceItem("View"));

            item.Add(factory.Create<ShowSummaryViewMessage>());

            return item;
        }

        public INavigationItem Help()
        {
            var item = new NavigationItem(new ResourceItem("Help"));

            item.Add(new ResourceItem("Manual"));

            return item;
        }
    }
}
