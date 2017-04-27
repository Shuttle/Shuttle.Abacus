using System;
using Shuttle.Abacus.DataAccess;
using Shuttle.Abacus.Infrastructure;
using Shuttle.Abacus.Localisation;
using Shuttle.Abacus.UI.Coordinators.Interfaces;
using Shuttle.Abacus.UI.Core.Presentation;
using Shuttle.Abacus.UI.Core.Resources;
using Shuttle.Abacus.UI.Messages.Core;
using Shuttle.Abacus.UI.Messages.Explorer;
using Shuttle.Abacus.UI.Messages.Resources;
using Shuttle.Abacus.UI.Messages.Section;
using Shuttle.Abacus.UI.UI.Method;
using Shuttle.Abacus.UI.UI.Shell.TabbedWorkspace;
using Shuttle.Abacus.UI.UI.WorkItem.ContextToolbar;
using Shuttle.Abacus.UI.WorkItemControllers.Interfaces;
using Shuttle.Core.Data;
using Shuttle.Core.Infrastructure;

namespace Shuttle.Abacus.UI.Coordinators
{
    public class MethodCoordinator :
        Coordinator,
        IMethodCoordinator
    {
        private readonly IDatabaseContextFactory _databaseContextFactory;
        private readonly IMethodQuery _methodQuery;

        public MethodCoordinator(IDatabaseContextFactory databaseContextFactory, IMethodQuery methodQuery)
        {
            Guard.AgainstNull(databaseContextFactory, "databaseContextFactory");
            Guard.AgainstNull(methodQuery, "_methodQuery");

            _databaseContextFactory = databaseContextFactory;
            _methodQuery = methodQuery;
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
            if (message.Resource.ResourceKey.Equals(ResourceKeys.Method))
            {
                return;
            }

            switch (message.Resource.Type)
            {
                case Resource.ResourceType.Container:
                    {
                        using (_databaseContextFactory.Create())

                        {
                            foreach (var row in _methodQuery.All())
                            {
                                message.Resources.Add(
                                    new Resource(
                                        ResourceKeys.Method,
                                        MethodColumns.Id.MapFrom(row),
                                        MethodColumns.Name.MapFrom(row),
                                        ImageResources.Method));
                            }
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
            if (message.Item.ResourceKey.Equals(ResourceKeys.Method))
            {
                return;
            }

            using (_databaseContextFactory.Create())
            {
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
        }

        public void HandleMessage(EditMethodMessage message)
        {
            var item = WorkItemManager
                .Create("Method: " + MethodColumns.Name.MapFrom(_methodQuery.Get(message.MethodId)))
                .ControlledBy<IMethodController>()
                .ShowIn<IContextToolbarPresenter>()
                .AddPresenter<IMethodPresenter>().WithModel(_methodQuery.Get(message.MethodId))
                .AddNavigationItem(NavigationItemFactory.Create(message).AssignResourceItem(ResourceItems.Submit)).
                AsDefault()
                .AssignInitiator(message);

            item.GetPresenter<IMethodPresenter>().HandleMessage(message);

            HostInWorkspace<ITabbedWorkspacePresenter>(item);
        }

        public void HandleMessage(ResourceRefreshItemTextMessage message)
        {
            if (message.Item.ResourceKey.Equals(ResourceKeys.Method) ||
                message.Item.Type != Resource.ResourceType.Item)
            {
                return;
            }

            using (_databaseContextFactory.Create())
            {
                message.Item.AssignText(MethodColumns.Name.MapFrom(_methodQuery.Get(message.Item.Key)));
            }
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

            using (_databaseContextFactory.Create())
            {
                using (_databaseContextFactory.Create())
                {
                    switch (message.Item.Type)
                    {
                        case Resource.ResourceType.Container:
                            {
                                message.AddTable("Methods", _methodQuery.All());

                                break;
                            }
                        case Resource.ResourceType.Item:
                            {
                                message.AddRow(message.Item.Text, _methodQuery.Get(message.Item.Key));

                                break;
                            }
                    }
                }
            }
        }
    }
}