using System;
using Shuttle.Abacus.DataAccess;
using Shuttle.Abacus.Localisation;
using Shuttle.Abacus.UI.Coordinators.Interfaces;
using Shuttle.Abacus.UI.Core.Presentation;
using Shuttle.Abacus.UI.Core.Resources;
using Shuttle.Abacus.UI.Messages.Core;
using Shuttle.Abacus.UI.Messages.Limit;
using Shuttle.Abacus.UI.Messages.Resources;
using Shuttle.Abacus.UI.UI.Limit;
using Shuttle.Abacus.UI.UI.Shell.TabbedWorkspace;
using Shuttle.Abacus.UI.UI.WorkItem.ContextToolbar;
using Shuttle.Abacus.UI.WorkItemControllers.Interfaces;
using Shuttle.Core.Data;
using Shuttle.Core.Infrastructure;

namespace Shuttle.Abacus.UI.Coordinators
{
    public class LimitCoordinator : Coordinator, ILimitCoordinator
    {
        private readonly IDatabaseContextFactory _databaseContextFactory;
        private readonly ILimitQuery _limitQuery;

        public LimitCoordinator(IDatabaseContextFactory databaseContextFactory, ILimitQuery limitQuery)
        {
            Guard.AgainstNull(databaseContextFactory, "databaseContextFactory");
            Guard.AgainstNull(limitQuery, "limitQuery");

            _databaseContextFactory = databaseContextFactory;
            _limitQuery = limitQuery;
        }

        public void HandleMessage(PopulateResourceMessage message)
        {
            if (!message.Resource.ResourceKey.Equals(ResourceKeys.Limit))
            {
                return;
            }

            var ownerId = message.RelatedResources.Contains(ResourceKeys.Calculation)
                ? message.RelatedResources[ResourceKeys.Calculation].Key
                : message.RelatedResources[ResourceKeys.Method].Key;

            using (_databaseContextFactory.Create())
            {
                switch (message.Resource.Type)
                {
                    case Resource.ResourceType.Container:
                    {
                        foreach (var row in _limitQuery.AllForOwner(ownerId))
                        {
                            message.Resources.Add(
                                new Resource(ResourceKeys.Limit, LimitColumns.Id.MapFrom(row),
                                    LimitColumns.Name.MapFrom(row), ImageResources.Limit));
                        }

                        break;
                    }
                    case Resource.ResourceType.Item:
                    {
                        message.Resources.Add(
                            new Resource(ResourceKeys.Formula, Guid.NewGuid(), "Formulas",
                                ImageResources.Formula).AsContainer());

                        break;
                    }
                }
            }
        }

        public void HandleMessage(ResourceMenuRequestMessage message)
        {
            if (!message.Item.ResourceKey.Equals(ResourceKeys.Limit))
            {
                return;
            }

            Guid ownerId;
            string ownerName;

            if (message.RelatedItems.Contains(ResourceKeys.Calculation))
            {
                ownerName = "Calculation";
                ownerId = message.RelatedItems[ResourceKeys.Calculation].Key;
            }
            ownerName = "Method";
            ownerId = message.RelatedItems[ResourceKeys.Method].Key;

            switch (message.Item.Type)
            {
                case Resource.ResourceType.Container:
                {
                    message.NavigationItems.Add(
                        NavigationItemFactory.Create(
                            new NewLimitMessage(ownerName, ownerId)));

                    break;
                }
                case Resource.ResourceType.Item:
                {
                    message.NavigationItems.Add(
                        NavigationItemFactory.Create(
                            new EditLimitMessage(message.Item.Key, message.Item.Text)));

                    message.NavigationItems.Add(
                        NavigationItemFactory.Create(
                            new DeleteLimitMessage(message.Item.Key, message.Item.Text, message.UpstreamItems[0])));

                    break;
                }
            }
        }

        public void HandleMessage(NewLimitMessage message)
        {
            var item = WorkItemManager
                .Create("New limit")
                .ControlledBy<ILimitController>()
                .ShowIn<IContextToolbarPresenter>()
                .AddPresenter<ILimitPresenter>()
                .AddNavigationItem(
                    NavigationItemFactory.Create(message).AssignResourceItem(ResourceItems.Submit)).AsDefault()
                .AssignInitiator(message);

            HostInWorkspace<ITabbedWorkspacePresenter>(item);
        }

        public void HandleMessage(EditLimitMessage message)
        {
            var item = WorkItemManager
                .Create(string.Format("Edit limit: {0}", message.Text))
                .ControlledBy<ILimitController>()
                .ShowIn<IContextToolbarPresenter>()
                .AddPresenter<ILimitPresenter>().WithModel(_limitQuery.Get(message.LimitId))
                .AddNavigationItem(
                    NavigationItemFactory.Create(message).AssignResourceItem(ResourceItems.Submit)).AsDefault()
                .AssignInitiator(message);

            HostInWorkspace<ITabbedWorkspacePresenter>(item);
        }

        public void HandleMessage(DeleteLimitMessage message)
        {
            if (!UIService.Confirm(string.Format("Are you sure that you want to delete limit '{0}'?", message.Text)))
            {
                return;
            }

            WorkItemControllerFactory.Create<ILimitController>().HandleMessage(message);
        }

        public void HandleMessage(SummaryViewRequestedMessage message)
        {
            if (SummaryViewManager.CanIgnore(message, ResourceKeys.Limit))
            {
                return;
            }

            using (_databaseContextFactory.Create())
            {
                switch (message.Item.Type)
                {
                    case Resource.ResourceType.Container:
                    {
                        var ownerId = message.RelatedItems.Contains(ResourceKeys.Calculation)
                            ? message.RelatedItems[ResourceKeys.Calculation].Key
                            : message.RelatedItems[ResourceKeys.Method].Key;

                        message.AddTable("Limits", _limitQuery.AllForOwner(ownerId));

                        break;
                    }
                    case Resource.ResourceType.Item:
                    {
                        //message.AddRow(message.Item.Text, productQuery.Get(message.Item.Key));

                        break;
                    }
                }
            }
        }
    }
}