using System;
using System.Collections.Generic;
using System.Data;
using Shuttle.Abacus.DataAccess;
using Shuttle.Abacus.Infrastructure;
using Shuttle.Abacus.Localisation;
using Shuttle.Abacus.UI.Coordinators.Interfaces;
using Shuttle.Abacus.UI.Core.Presentation;
using Shuttle.Abacus.UI.Core.Resources;
using Shuttle.Abacus.UI.Core.State;
using Shuttle.Abacus.UI.Messages.Calculation;
using Shuttle.Abacus.UI.Messages.Core;
using Shuttle.Abacus.UI.Messages.Resources;
using Shuttle.Abacus.UI.Models;
using Shuttle.Abacus.UI.UI.Calculation;
using Shuttle.Abacus.UI.UI.Calculation.GraphNodeArgument;
using Shuttle.Abacus.UI.UI.List;
using Shuttle.Abacus.UI.UI.Shell.TabbedWorkspace;
using Shuttle.Abacus.UI.UI.WorkItem.ContextToolbar;
using Shuttle.Abacus.UI.WorkItemControllers.Interfaces;
using Shuttle.Core.Data;
using Shuttle.Core.Infrastructure;

namespace Shuttle.Abacus.UI.Coordinators
{
    public class CalculationCoordinator :
        Coordinator,
        ICalculationCoordinator
    {
        private readonly IArgumentQuery _argumentQuery;
        private readonly ICalculationQuery _calculationQuery;
        private readonly IDatabaseContextFactory _databaseContextFactory;

        public CalculationCoordinator(IDatabaseContextFactory databaseContextFactory, ICalculationQuery calculationQuery,
            IArgumentQuery argumentQuery)
        {
            Guard.AgainstNull(databaseContextFactory, "databaseContextFactory");
            Guard.AgainstNull(calculationQuery, "calculationQuery");
            Guard.AgainstNull(argumentQuery, "argumentQuery");

            _databaseContextFactory = databaseContextFactory;
            _calculationQuery = calculationQuery;
            _argumentQuery = argumentQuery;
        }

        public void HandleMessage(PopulateResourceMessage message)
        {
            if (!message.Resource.ResourceKey.Equals(ResourceKeys.Calculation))
            {
                return;
            }

            switch (message.Resource.Type)
            {
                case Resource.ResourceType.Container:
                    {
                        using (_databaseContextFactory.Create())
                        {
                            foreach (
                                var row in
                                _calculationQuery.AllForOwner(message.RelatedResources.FirstItem.Key))
                            {
                                message.Resources.Add(
                                    new Resource(
                                            ResourceKeys.Calculation,
                                            CalculationColumns.Id.MapFrom(row),
                                            CalculationColumns.Name.MapFrom(row),
                                            ImageResources.Calculation)
                                        .State.Add(StateKeys.Type,
                                            Enumeration.Cast<Enumeration.CalculationType>(
                                                CalculationColumns.Type.MapFrom(row)))
                                );
                            }
                        }

                        break;
                    }
                case Resource.ResourceType.Item:
                    {
                        switch (message.Resource.State.Get<Enumeration.CalculationType>(StateKeys.Type))
                        {
                            case Enumeration.CalculationType.Formula:
                                {
                                    message.Resources.Add(
                                        new Resource(ResourceKeys.Formula, Guid.NewGuid(), "Formulas",
                                            ImageResources.Formula).AsContainer());

                                    break;
                                }
                            case Enumeration.CalculationType.Collection:
                                {
                                    message.Resources.Add(
                                        new Resource(ResourceKeys.Calculation, Guid.NewGuid(), "Calculations",
                                            ImageResources.Calculation).AsContainer());

                                    break;
                                }
                        }

                        message.Resources.Add(
                            new Resource(ResourceKeys.Limit, Guid.NewGuid(), "Limits",
                                ImageResources.Limit).AsContainer());

                        message.Resources.Add(
                            new Resource(ResourceKeys.Constraint, Guid.NewGuid(), "Constraints",
                                ImageResources.Constraint).AsContainer());

                        break;
                    }
            }
        }

        public void HandleMessage(NewCalculationMessage message)
        {
            using (_databaseContextFactory.Create())
            {
                var item = WorkItemManager
                    .Create("New calculation")
                    .ControlledBy<ICalculationController>()
                    .ShowIn<IContextToolbarPresenter>()
                    .AddPresenter<ICalculationPresenter>()
                    .AddPresenter<IGraphNodeArgumentPresenter>()
                    .WithModel(new ArgumentDisplayModel(_argumentQuery.All()))
                    .AddNavigationItem(NavigationItemFactory.Create(message).AssignResourceItem(ResourceItems.Submit)).
                    AsDefault()
                    .AssignInitiator(message);

                HostInWorkspace<ITabbedWorkspacePresenter>(item);
            }
        }

        public void HandleMessage(EditCalculationMessage message)
        {
            using (_databaseContextFactory.Create())
            {
                var item = WorkItemManager
                    .Create("Edit calculation: " +
                            CalculationColumns.Name.MapFrom(_calculationQuery.Get(message.CalculationId)))
                    .ControlledBy<ICalculationController>()
                    .ShowIn<IContextToolbarPresenter>()
                    .AddPresenter<ICalculationPresenter>().WithModel(_calculationQuery.Get(message.CalculationId))
                    .AddPresenter<IGraphNodeArgumentPresenter>()
                    .WithModel(new ArgumentDisplayModel(_argumentQuery.All())
                    {
                        GraphNodeArguments =
                            _calculationQuery.GraphNodeArguments(message.CalculationId).CopyToDataTable()
                    })
                    .AddNavigationItem(
                        NavigationItemFactory.Create(message).AssignResourceItem(ResourceItems.Submit)).AsDefault()
                    .AssignInitiator(message);

                HostInWorkspace<ITabbedWorkspacePresenter>(item);
            }
        }

        public void HandleMessage(ChangeCalculationOrderMessage message)
        {
            var model = new SimpleListModel
            {
                VisibleColumns = new List<string>
                {
                    CalculationColumns.Name
                }
            };

            using (_databaseContextFactory.Create())
            {
                model.Rows = _calculationQuery.AllForOwner(message.OwnerId);
            }

            var item = WorkItemManager
                    .Create(string.Format("'{0}' Calculations", message.OwnerText))
                    .ControlledBy<ICalculationController>()
                    .ShowIn<IContextToolbarPresenter>()
                    .AddPresenter<ISimpleListPresenter>().WithModel(model)
                    .AddNavigationItem(NavigationItemFactory.Create(message).AssignResourceItem(ResourceItems.Submit))
                    .AddNavigationItem(NavigationItemFactory.Create<MoveDownMessage>())
                    .AddNavigationItem(NavigationItemFactory.Create<MoveUpMessage>())
                    .AssignInitiator(message);

            HostInWorkspace<ITabbedWorkspacePresenter>(item);
        }

        public void HandleMessage(ResourceMenuRequestMessage message)
        {
            if (!message.Item.ResourceKey.Equals(ResourceKeys.Calculation))
            {
                return;
            }

            string ownerName;
            Guid ownerId;

            if (message.RelatedItems.Contains(ResourceKeys.Calculation))
            {
                ownerName = "Calculation";

                ownerId = message.RelatedItems[ResourceKeys.Calculation].Key;
            }
            else
            {
                ownerName = "Method";

                ownerId = message.RelatedItems[ResourceKeys.Method].Key;
            }

            switch (message.Item.Type)
            {
                case Resource.ResourceType.Container:
                    {
                        message.NavigationItems.Add(
                            NavigationItemFactory.Create(
                                new NewCalculationMessage(message.RelatedItems[ResourceKeys.Method].Key,
                                    ownerName,
                                    ownerId)));

                        message.NavigationItems.Add(
                            NavigationItemFactory.Create(
                                new ChangeCalculationOrderMessage(
                                    message.RelatedItems[ResourceKeys.Method].Key,
                                    ownerId,
                                    message.RelatedItems[0].Text)));

                        break;
                    }
                case Resource.ResourceType.Item:
                    {
                        message.NavigationItems.Add(
                            NavigationItemFactory.Create(
                                new EditCalculationMessage(message.Item.Key,
                                    ownerName,
                                    ownerId,
                                    message.RelatedItems[ResourceKeys.Method].Key)));

                        message.NavigationItems.Add(
                            NavigationItemFactory.Create(
                                new DeleteCalculationMessage(
                                    message.UpstreamItems[0],
                                    message.Item.Text,
                                    message.Item.Key,
                                    message.RelatedItems[ResourceKeys.Method].Key)));

                        if (message.Item.State.Get<Enumeration.CalculationType>(StateKeys.Type) ==
                            Enumeration.CalculationType.Collection)
                        {
                            message.NavigationItems.Add(
                                NavigationItemFactory.Create(
                                    new GrabCalculationsMessage(
                                        message.Item.Key,
                                        message.RelatedItems[ResourceKeys.Method].Key,
                                        message.Item.Text,
                                        message.RelatedItems[ResourceKeys.Method])));
                        }

                        break;
                    }
            }
        }

        public void HandleMessage(ResourceRefreshItemTextMessage message)
        {
            if (!message.Item.ResourceKey.Equals(ResourceKeys.Calculation) ||
                message.Item.Type != Resource.ResourceType.Item)
            {
                return;
            }

            using (_databaseContextFactory.Create())
            {
                message.Item.AssignText(CalculationColumns.Name.MapFrom(_calculationQuery.Get(message.Item.Key)));
            }
        }

        public void HandleMessage(DeleteCalculationMessage message)
        {
            if (
                !UIService.Confirm(string.Format("Are you sure that you want to delete calculation '{0}'?", message.Text)))
            {
                return;
            }

            WorkItemControllerFactory.Create<ICalculationController>().HandleMessage(message);
        }

        public void HandleMessage(GrabCalculationsMessage message)
        {
            using (_databaseContextFactory.Create())
            {
                var item = WorkItemManager
                    .Create(string.Format("'{0}' Grabbing", message.Text))
                    .ControlledBy<ICalculationController>()
                    .ShowIn<IContextToolbarPresenter>()
                    .AddPresenter<ISimpleListPresenter>()
                    .WithModel(
                        new SimpleListModel(_calculationQuery.AllForMethod(message.MethodId,
                            message.GrabberCalculationId))
                        {
                            HasCheckBoxes = true
                        })
                    .AddNavigationItem(NavigationItemFactory.Create(message).AssignResourceItem(ResourceItems.Submit))
                    .AssignInitiator(message);

                HostInWorkspace<ITabbedWorkspacePresenter>(item);
            }
        }

        public void HandleMessage(SummaryViewRequestedMessage message)
        {
            if (SummaryViewManager.CanIgnore(message, ResourceKeys.Calculation))
            {
                return;
            }

            using (_databaseContextFactory.Create())
            {
                switch (message.Item.Type)
                {
                    case Resource.ResourceType.Container:
                        {
                            message.AddTable("Calculations",
                                _calculationQuery.AllForMethod(message.RelatedItems[ResourceKeys.Method].Key));

                            break;
                        }
                    case Resource.ResourceType.Item:
                        {
                            message.AddRow(message.Item.Text, _calculationQuery.Get(message.Item.Key));

                            break;
                        }
                }
            }
        }

        private static class StateKeys
        {
            public static readonly StateKey Type = new StateKey("Type");
        }
    }
}