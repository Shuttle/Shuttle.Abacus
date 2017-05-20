using System;
using Shuttle.Abacus.DataAccess;
using Shuttle.Abacus.Localisation;
using Shuttle.Abacus.Shell.Core.Presentation;
using Shuttle.Abacus.Shell.Core.Resources;
using Shuttle.Abacus.Shell.Messages.ArgumentValue;
using Shuttle.Abacus.Shell.Messages.Core;
using Shuttle.Abacus.Shell.Messages.Resources;
using Shuttle.Abacus.Shell.Models;
using Shuttle.Abacus.Shell.Navigation;
using Shuttle.Abacus.Shell.UI.ArgumentValue;
using Shuttle.Abacus.Shell.UI.Shell.TabbedWorkspace;
using Shuttle.Abacus.Shell.UI.WorkItem.ContextToolbar;
using Shuttle.Core.Data;
using Shuttle.Core.Infrastructure;

namespace Shuttle.Abacus.Shell.Coordinators
{
    public class ArgumentValueCoordinator : Coordinator, IArgumentValueCoordinator
    {
        private readonly IArgumentQuery _argumentQuery;
        private readonly IDatabaseContextFactory _databaseContextFactory;

        private readonly INavigationItem _register =
            new NavigationItem(new ResourceItem("Register", "ArgumentValue"));

        private readonly INavigationItem _remove = new NavigationItem(new ResourceItem("Remove", "ArgumentValue"));

        public ArgumentValueCoordinator(IDatabaseContextFactory databaseContextFactory, IArgumentQuery argumentQuery)
        {
            Guard.AgainstNull(databaseContextFactory, "databaseContextFactory");
            Guard.AgainstNull(argumentQuery, "argumentQuery");

            _databaseContextFactory = databaseContextFactory;
            _argumentQuery = argumentQuery;
        }

        public void HandleMessage(ResourceMenuRequestMessage message)
        {
            if (!message.Item.ResourceKey.Equals(ResourceKeys.ArgumentValue))
            {
                return;
            }

            switch (message.Item.Type)
            {
                case Resource.ResourceType.Container:
                    {
                        message.NavigationItems.Add(_register.AssignMessage(new RegisterArgumentValueMessage(message.UpstreamItems[ResourceKeys.Argument].Key)));

                        break;
                    }
                case Resource.ResourceType.Item:
                    {
                        message.NavigationItems.Add(_remove.AssignMessage(
                                new RemoveArgumentValueMessage(message.Item.Text, message.UpstreamItems[ResourceKeys.Argument].Key)));

                        break;
                    }
            }
        }

        public void HandleMessage(RegisterArgumentValueMessage message)
        {
            var item = WorkItemManager
                .Create("New ArgumentValue")
                .ControlledBy<IArgumentValueController>()
                .ShowIn<IContextToolbarPresenter>()
                .AddPresenter<IArgumentValuePresenter>().WithModel(new ArgumentValueModel())
                .AddNavigationItem(_register.AssignMessage(message)).AsDefault()
                .AssignInitiator(message);

            HostInWorkspace<ITabbedWorkspacePresenter>(item);
        }

        public void HandleMessage(PopulateResourceMessage message)
        {
            if (!message.Resource.ResourceKey.Equals(ResourceKeys.ArgumentValue))
            {
                return;
            }

            switch (message.Resource.Type)
            {
                case Resource.ResourceType.Container:
                    {
                        var argumentId = message.RelatedResources[ResourceKeys.Argument].Key;

                        using (_databaseContextFactory.Create())
                        {
                            foreach (var row in _argumentQuery.GetValues(argumentId))
                            {
                                message.Resources.Add(new Resource(ResourceKeys.ArgumentValue,
                                    ArgumentColumns.ValueColumns.ArgumentId.MapFrom(row),
                                    ArgumentColumns.ValueColumns.Value.MapFrom(row),
                                    ImageResources.ArgumentValue).AsLeaf());
                            }
                        }

                        break;
                    }
            }
        }

        public void HandleMessage(ResourceRefreshItemTextMessage message)
        {
            if (!message.Item.ResourceKey.Equals(ResourceKeys.ArgumentValue) ||
                message.Item.Type != Resource.ResourceType.Item)
            {
                return;
            }

            using (_databaseContextFactory.Create())
            {
                throw new NotImplementedException();
                //message.Item.AssignText(TestColumns.ArgumentColumns.Name.MapFrom(_argumentQuery.Get(message.Item.Key)));
            }
        }

        public void HandleMessage(RemoveArgumentValueMessage message)
        {
            if (!UIService.Confirm(string.Format("Are you sure that you want to delete argument '{0}'?", message.Value)))
            {
                return;
            }

            WorkItemControllerFactory.Create<IArgumentValueController>().HandleMessage(message);
        }

        public void HandleMessage(SummaryViewRequestedMessage message)
        {
            if (SummaryViewManager.CanIgnore(message, ResourceKeys.ArgumentValue))
            {
                return;
            }

            using (_databaseContextFactory.Create())
            {
                switch (message.Item.Type)
                {
                    case Resource.ResourceType.Container:
                        {
                            message.AddTable("ArgumentValues", _argumentQuery.All());

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