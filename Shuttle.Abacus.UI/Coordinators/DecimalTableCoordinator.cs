using System;
using System.Data;
using Shuttle.Abacus.DataAccess;
using Shuttle.Abacus.Domain;
using Shuttle.Abacus.Infrastructure;
using Shuttle.Abacus.UI.Coordinators.Interfaces;
using Shuttle.Abacus.UI.Core.Presentation;
using Shuttle.Abacus.UI.Core.Resources;
using Shuttle.Abacus.UI.Messages.Core;
using Shuttle.Abacus.UI.Messages.DecimalTable;
using Shuttle.Abacus.UI.Messages.Explorer;
using Shuttle.Abacus.UI.Messages.Report;
using Shuttle.Abacus.UI.Messages.Resources;
using Shuttle.Abacus.UI.Models;
using Shuttle.Abacus.UI.UI.DecimalTable;
using Shuttle.Abacus.UI.UI.Shell.TabbedWorkspace;
using Shuttle.Abacus.UI.UI.WorkItem.ContextToolbar;
using Shuttle.Abacus.UI.WorkItemControllers.Interfaces;

namespace Shuttle.Abacus.UI.Coordinators
{
    public class DecimalTableCoordinator : Coordinator, IDecimalTableCoordinator
    {
        private readonly IArgumentQuery argumentQuery;
        private readonly IConstraintQuery constraintQuery;
        private readonly IDecimalTableQuery decimalTableQuery;

        public DecimalTableCoordinator(IArgumentQuery argumentQuery, IDecimalTableQuery decimalTableQuery,
                                       IConstraintQuery constraintQuery)
        {
            this.argumentQuery = argumentQuery;
            this.decimalTableQuery = decimalTableQuery;
            this.constraintQuery = constraintQuery;
        }

        public void HandleMessage(ExplorerInitializeMessage message)
        {
            if (!Permissions.DecimalTable.IsSatisfiedBy(Session.Permissions))
            {
                return;
            }

            message.Items.Add(
                new Resource(ResourceKeys.DecimalTable, Guid.NewGuid(), "Decimal Tables", ImageResources.DecimalTable).AsContainer());
        }

        public void HandleMessage(ResourceMenuRequestMessage message)
        {
            if (message.Item.ResourceKey != ResourceKeys.DecimalTable)
            {
                return;
            }

            switch (message.Item.Type)
            {
                case Resource.ResourceType.Container:
                    {
                        message.NavigationItems.Add(NavigationItemFactory.Create<NewDecimalTableMessage>());

                        break;
                    }
                case Resource.ResourceType.Item:
                    {
                        message.NavigationItems.Add(
                            NavigationItemFactory.Create(
                                new NewDecimalTableFromExistingMessage(message.Item.Key)));

                        message.NavigationItems.Add(
                            NavigationItemFactory.Create(
                                new EditDecimalTableMessage(message.Item.Key,
                                    message.Item.Text)));

                        message.NavigationItems.Add(
                            NavigationItemFactory.Create(
                                new DecimalTableReportMessage(message.Item.Key,
                                    message.Item.Text)));

                        break;
                    }
            }
        }

        public void HandleMessage(PopulateResourceMessage message)
        {
            if (message.Resource.ResourceKey != ResourceKeys.DecimalTable)
            {
                return;
            }

            switch (message.Resource.Type)
            {
                case Resource.ResourceType.Container:
                    {
                        foreach (
                            DataRow row in
                                decimalTableQuery.All().Table.Rows)
                        {
                            message.Resources.Add(new Resource(ResourceKeys.DecimalTable,
                                                               DecimalTableColumns.Id.MapFrom(row),
                                                               DecimalTableColumns.Name.MapFrom(row),
                                                               ImageResources.DecimalTable).AsLeaf());
                        }

                        break;
                    }
            }
        }

        public void HandleMessage(ResourceRefreshItemTextMessage message)
        {
            if (message.Item.ResourceKey != ResourceKeys.DecimalTable ||
                message.Item.Type != Resource.ResourceType.Item)
            {
                return;
            }

            message.Item.AssignText(DecimalTableColumns.Name.MapFrom(decimalTableQuery.Name(message.Item.Key).Row));
        }

        public void HandleMessage(NewDecimalTableMessage message)
        {
            var item = WorkItemManager
                .Create("New Decimal Table")
                .ControlledBy<IDecimalTableController>()
                .ShowIn<IContextToolbarPresenter>()
                .AddPresenter<IDecimalTablePresenter>().WithModel(BuildDecimalTableModel())
                .AddNavigationItem(NavigationItemFactory.Create(message).AssignResourceItem(ResourceItems.Submit)).
                AsDefault()
                .AssignInitiator(message);

            HostInWorkspace<ITabbedWorkspacePresenter>(item);
        }

        public void HandleMessage(EditDecimalTableMessage message)
        {
            var model = BuildDecimalTableModel();

            model.DecimalTableRow = decimalTableQuery.Get(message.DecimalTableId).Row;
            model.ConstrainedDecimalValues = decimalTableQuery.ConstrainedDecimalValues(message.DecimalTableId);

            var item = WorkItemManager
                .Create(string.Format("Decimal Table: {0}", message.DecimalTableName))
                .ControlledBy<IDecimalTableController>()
                .ShowIn<IContextToolbarPresenter>()
                .AddPresenter<IDecimalTablePresenter>().WithModel(model)
                .AddNavigationItem(NavigationItemFactory.Create(message).AssignResourceItem(ResourceItems.Submit)).
                AsDefault()
                .AssignInitiator(message);

            HostInWorkspace<ITabbedWorkspacePresenter>(item);
        }

        public void HandleMessage(NewDecimalTableFromExistingMessage message)
        {
            var decimalTableModel = BuildDecimalTableModel();

            if (decimalTableModel == null)
            {
                return;
            }

            decimalTableModel.ConstrainedDecimalValues = decimalTableQuery.ConstrainedDecimalValues(message.DecimalTableId);
            decimalTableModel.DecimalTableRow = decimalTableQuery.Get(message.DecimalTableId).Row;

            var item = WorkItemManager
                .Create("New Decimal Table")
                .ControlledBy<IDecimalTableController>()
                .ShowIn<IContextToolbarPresenter>()
                .AddPresenter<IDecimalTablePresenter>().WithModel(decimalTableModel)
                .AddNavigationItem(
                NavigationItemFactory.Create<NewDecimalTableMessage>().AssignResourceItem(
                    ResourceItems.Submit))
                .
                AsDefault()
                .AssignInitiator(message.WithRefreshOwner());

            HostInWorkspace<ITabbedWorkspacePresenter>(item);
        }

        public void HandleMessage(SummaryViewRequestedMessage message)
        {
            if (SummaryViewManager.CanIgnore(message, ResourceKeys.DecimalTable))
            {
                return;
            }

            switch (message.Item.Type)
            {
                case Resource.ResourceType.Container:
                    {
                        message.AddTable("DecimalTables", decimalTableQuery.All());

                        break;
                    }
                case Resource.ResourceType.Item:
                    {
                        break;
                    }
            }
        }

        private DecimalTableModel BuildDecimalTableModel()
        {
            return new DecimalTableModel
                       {
                           Factors = argumentQuery.AllDTOs(),
                           ConstraintTypes = constraintQuery.ConstraintTypes()
                       };
        }
    }
}