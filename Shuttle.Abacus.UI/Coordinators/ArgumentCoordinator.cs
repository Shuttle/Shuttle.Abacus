using System;
using Shuttle.Abacus.DataAccess;
using Shuttle.Abacus.Infrastructure;
using Shuttle.Abacus.Localisation;
using Shuttle.Abacus.UI.Coordinators.Interfaces;
using Shuttle.Abacus.UI.Core.Presentation;
using Shuttle.Abacus.UI.Core.Resources;
using Shuttle.Abacus.UI.Messages.Argument;
using Shuttle.Abacus.UI.Messages.Core;
using Shuttle.Abacus.UI.Messages.Explorer;
using Shuttle.Abacus.UI.Messages.Resources;
using Shuttle.Abacus.UI.Models;
using Shuttle.Abacus.UI.Navigation;
using Shuttle.Abacus.UI.UI.Argument;
using Shuttle.Abacus.UI.UI.Shell.TabbedWorkspace;
using Shuttle.Abacus.UI.UI.WorkItem.ContextToolbar;
using Shuttle.Abacus.UI.WorkItemControllers.Interfaces;
using Shuttle.Core.Data;
using Shuttle.Core.Infrastructure;

namespace Shuttle.Abacus.UI.Coordinators
{
    public class ArgumentCoordinator : Coordinator, IArgumentCoordinator
    {
        private readonly IArgumentQuery _argumentQuery;
        private readonly IDatabaseContextFactory _databaseContextFactory;

        private readonly INavigationItem _register =
            new NavigationItem(new ResourceItem("Register", "Argument")).AssignMessage(new RegisterArgumentMessage());

        private readonly INavigationItem _rename = new NavigationItem(new ResourceItem("Rename", "Argument"));
        private readonly INavigationItem _remove = new NavigationItem(new ResourceItem("Remove", "Argument"));

        public ArgumentCoordinator(IDatabaseContextFactory databaseContextFactory, IArgumentQuery argumentQuery)
        {
            Guard.AgainstNull(databaseContextFactory, "databaseContextFactory");
            Guard.AgainstNull(argumentQuery, "argumentQuery");

            _databaseContextFactory = databaseContextFactory;
            _argumentQuery = argumentQuery;
        }

        public void HandleMessage(ExplorerInitializeMessage message)
        {
            if (!Permissions.Argument.IsSatisfiedBy(Session.Permissions))
            {
                return;
            }

            message.Items.Add(
                new Resource(ResourceKeys.Argument, Guid.NewGuid(), "Arguments", ImageResources.Argument).AsContainer());
        }

        public void HandleMessage(ResourceMenuRequestMessage message)
        {
            if (!message.Item.ResourceKey.Equals(ResourceKeys.Argument))
            {
                return;
            }

            switch (message.Item.Type)
            {
                case Resource.ResourceType.Container:
                    {
                        message.NavigationItems.Add(_register);

                        break;
                    }
                case Resource.ResourceType.Item:
                    {
                        message.NavigationItems.Add(_rename.AssignMessage(new RenameArgumentMessage(message.Item.Key)));

                        message.NavigationItems.Add(_remove.AssignMessage(
                                new RemoveArgumentMessage(message.Item.Text, message.Item.Key, message.UpstreamItems[0])));

                        break;
                    }
            }
        }

        public void HandleMessage(RegisterArgumentMessage message)
        {
            var item = WorkItemManager
                .Create("New Argument")
                .ControlledBy<IArgumentController>()
                .ShowIn<IContextToolbarPresenter>()
                .AddPresenter<IArgumentPresenter>().WithModel(new ArgumentModel())
                .AddNavigationItem(_register).AsDefault()
                .AssignInitiator(message);

            HostInWorkspace<ITabbedWorkspacePresenter>(item);
        }

        public void HandleMessage(PopulateResourceMessage message)
        {
            if (!message.Resource.ResourceKey.Equals(ResourceKeys.Argument))
            {
                return;
            }

            switch (message.Resource.Type)
            {
                case Resource.ResourceType.Container:
                    {
                        using (_databaseContextFactory.Create())
                        {
                            foreach (var row in _argumentQuery.All())
                            {
                                message.Resources.Add(new Resource(ResourceKeys.Argument,
                                    ArgumentColumns.Id.MapFrom(row),
                                    ArgumentColumns.Name.MapFrom(row),
                                    ImageResources.Argument));
                            }
                        }

                        break;
                    }
                case Resource.ResourceType.Item:
                    {
                        message.Resources.Add(
                            new Resource(ResourceKeys.ArgumentValue, Guid.NewGuid(), "Values",
                                ImageResources.ArgumentValue).AsContainer());

                        break;
                    }
            }
        }

        public void HandleMessage(RenameArgumentMessage message)
        {
            using (_databaseContextFactory.Create())
            {
                var item = WorkItemManager
                    .Create("Edit Argument")
                    .ControlledBy<IArgumentController>()
                    .ShowIn<IContextToolbarPresenter>()
                    .AddPresenter<IArgumentPresenter>()
                    .WithModel(new ArgumentModel(_argumentQuery.Get(message.ArgumentId)))
                    .AddNavigationItem(_rename).AsDefault()
                    .AssignInitiator(message);

                HostInWorkspace<ITabbedWorkspacePresenter>(item);
            }
        }

        public void HandleMessage(ResourceRefreshItemTextMessage message)
        {
            if (!message.Item.ResourceKey.Equals(ResourceKeys.Argument) ||
                message.Item.Type != Resource.ResourceType.Item)
            {
                return;
            }

            using (_databaseContextFactory.Create())
            {
                message.Item.AssignText(ArgumentColumns.Name.MapFrom(_argumentQuery.Get(message.Item.Key)));
            }
        }

        public void HandleMessage(RemoveArgumentMessage message)
        {
            if (!UIService.Confirm(string.Format("Are you sure that you want to delete argument '{0}'?", message.Text)))
            {
                return;
            }

            WorkItemControllerFactory.Create<IArgumentController>().HandleMessage(message);
        }

        public void HandleMessage(SummaryViewRequestedMessage message)
        {
            if (SummaryViewManager.CanIgnore(message, ResourceKeys.Argument))
            {
                return;
            }

            using (_databaseContextFactory.Create())
            {
                switch (message.Item.Type)
                {
                    case Resource.ResourceType.Container:
                        {
                            message.AddTable("Arguments", _argumentQuery.All());

                            break;
                        }
                    case Resource.ResourceType.Item:
                        {
                            message.AddRow(message.Item.Text, _argumentQuery.Get(message.Item.Key));

                            message.AddTable("Value Values", _argumentQuery.GetValues(message.Item.Key));

                            break;
                        }
                }
            }
        }
    }
}