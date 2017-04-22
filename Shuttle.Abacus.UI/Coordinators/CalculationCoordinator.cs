using System;
using System.Collections.Generic;
using System.Data;
using Shuttle.Abacus.DataAccess;
using Shuttle.Abacus.Domain;
using Shuttle.Abacus.Infrastructure;
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

namespace Shuttle.Abacus.UI.Coordinators
{
    public class CalculationCoordinator :
        Coordinator,
        ICalculationCoordinator
    {
        private readonly ICalculationQuery calculationQuery;
        private readonly IArgumentQuery argumentQuery;

        public CalculationCoordinator(ICalculationQuery calculationQuery, IArgumentQuery argumentQuery)
        {
            this.calculationQuery = calculationQuery;
            this.argumentQuery = argumentQuery;
        }

        public void HandleMessage(PopulateResourceMessage message)
        {
            if (message.Resource.ResourceKey !=
                ResourceKeys.Calculation)
            {
                return;
            }

            switch (message.Resource.Type)
            {
                case Resource.ResourceType.Container:
                    {
                        foreach (
                            DataRow row in
                                calculationQuery.AllForOwner(message.RelatedResources.FirstItem.Key).Table.Rows)
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
            var item = WorkItemManager
                .Create("New calculation")
                .ControlledBy<ICalculationController>()
                .ShowIn<IContextToolbarPresenter>()
                .AddPresenter<ICalculationPresenter>()
                .AddPresenter<IGraphNodeArgumentPresenter>().WithModel(new ArgumentDisplayModel(argumentQuery.AllDTOs()))
                .AddNavigationItem(NavigationItemFactory.Create(message).AssignResourceItem(ResourceItems.Submit)).
                AsDefault()
                .AssignInitiator(message);

            HostInWorkspace<ITabbedWorkspacePresenter>(item);
        }

        public void HandleMessage(EditCalculationMessage message)
        {
            var item = WorkItemManager
                .Create("Edit calculation: " +
                        CalculationColumns.Name.MapFrom(calculationQuery.Get(message.CalculationId).Row))
                .ControlledBy<ICalculationController>()
                .ShowIn<IContextToolbarPresenter>()
                .AddPresenter<ICalculationPresenter>().WithModel(calculationQuery.Get(message.CalculationId))
                .AddPresenter<IGraphNodeArgumentPresenter>().WithModel(new ArgumentDisplayModel(argumentQuery.AllDTOs()) { GraphNodeArguments = calculationQuery.GraphNodeArguments(message.CalculationId).Table})
                .AddNavigationItem(
                NavigationItemFactory.Create(message).AssignResourceItem(ResourceItems.Submit)).AsDefault()
                .AssignInitiator(message);

            HostInWorkspace<ITabbedWorkspacePresenter>(item);
        }

        public void HandleMessage(ChangeCalculationOrderMessage message)
        {
            var model = new SimpleListModel
                        {
                            ListItems = calculationQuery.AllForOwner(message.OwnerId),
                            VisibleColumns = new List<QueryColumn>
                                             {
                                                 CalculationColumns.Name
                                             }
                        };

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
            if (message.Item.ResourceKey !=
                ResourceKeys.Calculation)
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
            if (message.Item.ResourceKey != ResourceKeys.Calculation ||
                message.Item.Type != Resource.ResourceType.Item)
            {
                return;
            }

            message.Item.AssignText(CalculationColumns.Name.MapFrom(calculationQuery.Name(message.Item.Key).Row));
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
            var item = WorkItemManager
                .Create(string.Format("'{0}' Grabbing", message.Text))
                .ControlledBy<ICalculationController>()
                .ShowIn<IContextToolbarPresenter>()
                .AddPresenter<ISimpleListPresenter>()
                .WithModel(
                new SimpleListModel(calculationQuery.AllForMethod(message.MethodId, message.GrabberCalculationId))
                {
                    HasCheckBoxes = true
                })
                .AddNavigationItem(NavigationItemFactory.Create(message).AssignResourceItem(ResourceItems.Submit))
                .AssignInitiator(message);

            HostInWorkspace<ITabbedWorkspacePresenter>(item);
        }

        public void HandleMessage(SummaryViewRequestedMessage message)
        {
            if (SummaryViewManager.CanIgnore(message, ResourceKeys.Calculation))
            {
                return;
            }

            switch (message.Item.Type)
            {
                case Resource.ResourceType.Container:
                    {
                        message.AddTable("Calculations",
                                         calculationQuery.AllForMethod(message.RelatedItems[ResourceKeys.Method].Key));

                        break;
                    }
                case Resource.ResourceType.Item:
                    {
                        //message.AddRow(message.Item.Text, productQuery.Get(message.Item.Key));

                        break;
                    }
            }
        }

        private static class StateKeys
        {
            public static readonly StateKey Type = new StateKey("Type");
        }
    }
}
