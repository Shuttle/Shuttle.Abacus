using System;
using Shuttle.Abacus.DataAccess;
using Shuttle.Abacus.Infrastructure;
using Shuttle.Abacus.Localisation;
using Shuttle.Abacus.UI.Coordinators.Interfaces;
using Shuttle.Abacus.UI.Core.Presentation;
using Shuttle.Abacus.UI.Core.Resources;
using Shuttle.Abacus.UI.Messages.Core;
using Shuttle.Abacus.UI.Messages.Explorer;
using Shuttle.Abacus.UI.Messages.Formula;
using Shuttle.Abacus.UI.Messages.Resources;
using Shuttle.Abacus.UI.Models;
using Shuttle.Abacus.UI.UI.Constraint;
using Shuttle.Abacus.UI.UI.Formula;
using Shuttle.Abacus.UI.UI.Shell.TabbedWorkspace;
using Shuttle.Abacus.UI.UI.WorkItem.ContextToolbar;
using Shuttle.Abacus.UI.WorkItemControllers.Interfaces;
using Shuttle.Core.Data;
using Shuttle.Core.Infrastructure;

namespace Shuttle.Abacus.UI.Coordinators
{
    public class FormulaCoordinator : Coordinator, IFormulaCoordinator
    {
        private readonly IDatabaseContextFactory _databaseContextFactory;
        private readonly IFormulaQuery _formulaQuery;

        public FormulaCoordinator(IDatabaseContextFactory databaseContextFactory, IFormulaQuery formulaQuery)
        {
            Guard.AgainstNull(databaseContextFactory, "databaseContextFactory");
            Guard.AgainstNull(formulaQuery, "_formulaQuery");

            _databaseContextFactory = databaseContextFactory;
            _formulaQuery = formulaQuery;
        }

        public void HandleMessage(ResourceMenuRequestMessage message)
        {
            if (!message.Item.ResourceKey.Equals(ResourceKeys.Formula))
            {
                return;
            }

            switch (message.Item.Type)
            {
                case Resource.ResourceType.Container:
                {
                    message.NavigationItems.Add(
                        NavigationItemFactory.Create(
                            new NewFormulaMessage()));

                    if (Clipboard.Contains(ResourceKeys.Formula))
                    {
                        message.NavigationItems.Add(NavigationItemFactory.Create<PasteFormulaMessage>());
                    }

                    break;
                }
                case Resource.ResourceType.Item:
                {
                    message.NavigationItems.Add(
                        NavigationItemFactory.Create(
                            new NewFormulaFromExistingMessage(message.Item.Key)));

                    message.NavigationItems.Add(
                        NavigationItemFactory.Create(
                            new EditFormulaMessage(message.Item.Key)));

                    message.NavigationItems.Add(
                        NavigationItemFactory.Create(
                            new DeleteFormulaMessage(message.Item.Text, message.Item.Key, message.UpstreamItems[0])));

                    break;
                }
            }
        }

        public void HandleMessage(PopulateResourceMessage message)
        {
            if (!message.Resource.ResourceKey.Equals(ResourceKeys.Formula))
            {
                return;
            }

            switch (message.Resource.Type)
            {
                case Resource.ResourceType.Container:
                {
                    using (_databaseContextFactory.Create())
                    {
                        foreach (var row in _formulaQuery.All())
                        {
                            message.Resources.Add(
                                new Resource(ResourceKeys.Formula, FormulaColumns.Id.MapFrom(row),
                                    FormulaColumns.Name.MapFrom(row), ImageResources.Formula));
                        }
                    }

                    break;
                }
                case Resource.ResourceType.Item:
                {
                    message.Resources.Add(
                        new Resource(ResourceKeys.Constraint, Guid.NewGuid(), "Constraints",
                            ImageResources.Constraint).AsContainer());

                    message.Resources.Add(
                        new Resource(ResourceKeys.Constraint, Guid.NewGuid(), "Constraints",
                            ImageResources.Constraint).AsContainer());

                    break;
                }
            }
        }

        public void HandleMessage(NewFormulaMessage message)
        {
            using (_databaseContextFactory.Create())
            {
                var formulaModel = new FormulaModel();

                var item = WorkItemManager
                    .Create("New formula")
                    .ControlledBy<IFormulaController>()
                    .ShowIn<IContextToolbarPresenter>()
                    .AddPresenter<IFormulaPresenter>().WithModel(formulaModel)
                    .AddNavigationItem(
                        NavigationItemFactory.Create(message).AssignResourceItem(ResourceItems.Submit)).AsDefault()
                    .AssignInitiator(message);

                HostInWorkspace<ITabbedWorkspacePresenter>(item);
            }
        }

        public void HandleMessage(EditFormulaMessage message)
        {
            using (_databaseContextFactory.Create())
            {
                var row = _formulaQuery.Get(message.FormulaId);

                if (row == null)
                {
                    MessageBus.Publish(
                        new ResultNotificationMessage(new Result().AddFailureMessage("Formula not found.")));

                    return;
                }

                var formulaModel = new FormulaModel(row);

                var item = WorkItemManager
                    .Create("Edit formula")
                    .ControlledBy<IFormulaController>()
                    .ShowIn<IContextToolbarPresenter>()
                    .AddPresenter<IFormulaPresenter>()
                    .WithModel(formulaModel)
                    .AddPresenter<IConstraintPresenter>()
                    .AddNavigationItem(
                        NavigationItemFactory.Create(message).AssignResourceItem(ResourceItems.Submit))
                    .AsDefault()
                    .AssignInitiator(message);

                HostInWorkspace<ITabbedWorkspacePresenter>(item);
            }
        }

        public void HandleMessage(ResourceRefreshItemTextMessage message)
        {
            if (!message.Item.ResourceKey.Equals(ResourceKeys.Formula) ||
                message.Item.Type != Resource.ResourceType.Item)
            {
                return;
            }

            using (_databaseContextFactory.Create())
            {
                message.Item.AssignText(FormulaColumns.Name.MapFrom(_formulaQuery.Get(message.Item.Key)));
            }
        }

        public void HandleMessage(DeleteFormulaMessage message)
        {
            if (!UIService.Confirm(string.Format("Are you sure that you want to delete formula '{0}'?", message.Text)))
            {
                return;
            }

            WorkItemControllerFactory.Create<IFormulaController>().HandleMessage(message);
        }

        public void HandleMessage(NewFormulaFromExistingMessage message)
        {
            var formulaModel = new FormulaModel();

            var item = WorkItemManager
                .Create("New formula")
                .ControlledBy<IFormulaController>()
                .ShowIn<IContextToolbarPresenter>()
                .AddPresenter<IFormulaPresenter>()
                .WithModel(formulaModel)
                .AddNavigationItem(
                    NavigationItemFactory.Create(new NewFormulaMessage(message))
                        .AssignResourceItem(ResourceItems.Submit))
                .AsDefault()
                .AssignInitiator(message.WithRefreshOwner());

            HostInWorkspace<ITabbedWorkspacePresenter>(item);
        }

        public void HandleMessage(SummaryViewRequestedMessage message)
        {
            if (SummaryViewManager.CanIgnore(message, ResourceKeys.Formula))
            {
                return;
            }

            using (_databaseContextFactory.Create())
            {
                switch (message.Item.Type)
                {
                    case Resource.ResourceType.Container:
                    {
                        message.AddTable("Formulas", _formulaQuery.All());

                        break;
                    }
                    case Resource.ResourceType.Item:
                    {
                        //var result = _formulaQuery.OperationsSummary(message.Item.Key);
                        var result = _formulaQuery.Get(message.Item.Key);

                        message.AddRow("Formula Operations", result);

                        break;
                    }
                }
            }
        }

        public void HandleMessage(ExplorerInitializeMessage message)
        {
            if (!Permissions.Formula.IsSatisfiedBy(Session.Permissions))
            {
                return;
            }

            message.Items.Add(
                new Resource(ResourceKeys.Formula, Guid.NewGuid(), "Formulas", ImageResources.Formula)
                    .AsContainer());
        }
    }
}