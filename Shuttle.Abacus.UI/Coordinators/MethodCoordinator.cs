using System;
using System.Data;
using Abacus.Data;
using Abacus.Infrastructure;
using Abacus.Localisation;

namespace Abacus.UI
{
    public class MethodCoordinator :
        Coordinator,
        IMethodCoordinator
    {
        private readonly IMethodQuery methodQuery;

        public MethodCoordinator(IMethodQuery methodQuery)
        {
            this.methodQuery = methodQuery;
        }

        public void HandleMessage(ExplorerInitializeMessage message)
        {
            if (!Permissions.Method.IsSatisfiedBy(Session.Permissions))
            {
                return;
            }

            message.Items.Add(
                new Resource(ResourceKeys.Method, Guid.NewGuid(), "Methods", ImageResources.Method).AsContainer());
        }

        public void HandleMessage(PopulateResourceMessage message)
        {
            if (message.Resource.ResourceKey !=
                ResourceKeys.Method)
            {
                return;
            }

            switch (message.Resource.Type)
            {
                case Resource.ResourceType.Container:
                    {
                        foreach (
                            DataRow row in
                                methodQuery.All().
                                    Table.Rows)
                        {
                            message.Resources.Add(
                                new Resource(
                                    ResourceKeys.Method,
                                    MethodColumns.Id.MapFrom(row),
                                    MethodColumns.Name.MapFrom(row),
                                    ImageResources.Method));
                        }

                        break;
                    }
                case Resource.ResourceType.Item:
                    {
                        message.Resources.Add(
                            new Resource(ResourceKeys.Calculation, Guid.NewGuid(), "Calculations",
                                             ImageResources.Calculation).AsContainer());

                        message.Resources.Add(
                            new Resource(ResourceKeys.Limit, Guid.NewGuid(), "Limits",
                                             ImageResources.Limit).AsContainer());

                        message.Resources.Add(
                            new Resource(ResourceKeys.MethodTest, Guid.NewGuid(), "Tests",
                                             ImageResources.MethodTest).AsContainer().AsLeaf());

                        break;
                    }
            }
        }

        public void HandleMessage(NewMethodMessage message)
        {
            var item = WorkItemManager
                .Create("New method")
                .ControlledBy<IMethodController>()
                .ShowIn<IContextToolbarPresenter>()
                .AddPresenter<IMethodPresenter>()
                .AddNavigationItem(NavigationItemFactory.Create(message).AssignResourceItem(ResourceItems.Submit)).
                AsDefault()
                .AssignInitiator(message);

            HostInWorkspace<ITabbedWorkspacePresenter>(item);
        }

        public void HandleMessage(ResourceMenuRequestMessage message)
        {
            if (message.Item.ResourceKey != ResourceKeys.Method)
            {
                return;
            }

            switch (message.Item.Type)
            {
                case Resource.ResourceType.Container:
                    {
                        message.NavigationItems.Add(NavigationItemFactory.Create<NewMethodMessage>());

                        break;
                    }
                case Resource.ResourceType.Item:
                    {
                        message.NavigationItems.Add(
                            NavigationItemFactory.Create(
                                new EditMethodMessage(message.Item.Key)));

                        message.NavigationItems.Add(
                            NavigationItemFactory.Create(
                                new NewMethodFromExistingMessage(message.Item.Key,
                                                                  message.Item.Text)));

                        message.NavigationItems.Add(
                            NavigationItemFactory.Create(
                                new DeleteMethodMessage(message.Item.Key, message.Item.Text, message.UpstreamItems[0])));

                        break;
                    }
            }
        }

        public void HandleMessage(EditMethodMessage message)
        {
            var item = WorkItemManager
                .Create("Method: " + MethodColumns.Name.MapFrom(methodQuery.Get(message.MethodId).Row))
                .ControlledBy<IMethodController>()
                .ShowIn<IContextToolbarPresenter>()
                .AddPresenter<IMethodPresenter>().WithModel(methodQuery.Get(message.MethodId))
                .AddNavigationItem(NavigationItemFactory.Create(message).AssignResourceItem(ResourceItems.Submit)).
                AsDefault()
                .AssignInitiator(message);

            item.GetPresenter<IMethodPresenter>().HandleMessage(message);

            HostInWorkspace<ITabbedWorkspacePresenter>(item);
        }

        public void HandleMessage(ResourceRefreshItemTextMessage message)
        {
            if (message.Item.ResourceKey != ResourceKeys.Method ||
                message.Item.Type != Resource.ResourceType.Item)
            {
                return;
            }

            message.Item.AssignText(MethodColumns.Name.MapFrom(methodQuery.MethodName(message.Item.Key).Row));
        }

        public void HandleMessage(NewMethodFromExistingMessage message)
        {
            var item = WorkItemManager
                .Create(string.Format("New method from '{0}'", message.MethodName))
                .ControlledBy<IMethodController>()
                .ShowIn<IContextToolbarPresenter>()
                .AddPresenter<IMethodPresenter>()
                .AddNavigationItem(NavigationItemFactory.Create(message).AssignResourceItem(ResourceItems.Submit)).
                AsDefault()
                .AssignInitiator(message.WithRefreshOwner());

            HostInWorkspace<ITabbedWorkspacePresenter>(item);
        }

        public void HandleMessage(DeleteMethodMessage message)
        {
            if (!UIService.Confirm(string.Format("Are you sure that you want to delete method '{0}'?", message.Text)))
            {
                return;
            }

            WorkItemControllerFactory.Create<IMethodController>().HandleMessage(message);
        }

        public void HandleMessage(SummaryViewRequestedMessage message)
        {
            if (SummaryViewManager.CanIgnore(message, ResourceKeys.Method))
            {
                return;
            }

            switch (message.Item.Type)
            {
                case Resource.ResourceType.Container:
                    {
                        message.AddTable("Methods", methodQuery.All());

                        break;
                    }
                case Resource.ResourceType.Item:
                    {
                        message.AddRow(message.Item.Text, methodQuery.Get(message.Item.Key));

                        break;
                    }
            }
        }
    }
}
