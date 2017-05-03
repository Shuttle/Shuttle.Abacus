using System;
using System.Collections.Generic;
using Shuttle.Abacus.DataAccess;
using Shuttle.Abacus.Localisation;
using Shuttle.Abacus.UI.Coordinators.Interfaces;
using Shuttle.Abacus.UI.Core.Presentation;
using Shuttle.Abacus.UI.Core.Resources;
using Shuttle.Abacus.UI.Messages.Core;
using Shuttle.Abacus.UI.Messages.Formula;
using Shuttle.Abacus.UI.Messages.Resources;
using Shuttle.Abacus.UI.Models;
using Shuttle.Abacus.UI.UI.Constraint;
using Shuttle.Abacus.UI.UI.Formula;
using Shuttle.Abacus.UI.UI.List;
using Shuttle.Abacus.UI.UI.Shell.TabbedWorkspace;
using Shuttle.Abacus.UI.UI.WorkItem.ContextToolbar;
using Shuttle.Abacus.UI.WorkItemControllers.Interfaces;
using Shuttle.Core.Data;
using Shuttle.Core.Infrastructure;

namespace Shuttle.Abacus.UI.Coordinators
{
    public class FormulaCoordinator : Coordinator, IFormulaCoordinator
    {
        private readonly IArgumentQuery _argumentQuery;
        private readonly ICalculationQuery _calculationQuery;
        private readonly IConstraintQuery _constraintQuery;
        private readonly IConstraintTypeQuery _constraintTypeQuery;
        private readonly IDatabaseContextFactory _databaseContextFactory;
        private readonly IDecimalTableQuery _decimalTableQuery;
        private readonly IFormulaQuery _formulaQuery;
        private readonly IMethodQuery _methodQuery;
        private readonly IOperationTypeQuery _operationTypeQuery;
        private readonly IValueSourceTypeQuery _valueSourceTypeQuery;

        public FormulaCoordinator(IDatabaseContextFactory databaseContextFactory, IFormulaQuery formulaQuery,
            IConstraintQuery constraintQuery, IConstraintTypeQuery constraintTypeQuery,
            IArgumentQuery argumentQuery, ICalculationQuery calculationQuery,
            IOperationTypeQuery operationTypeQuery, IValueSourceTypeQuery valueSourceTypeQuery,
            IDecimalTableQuery decimalTableQuery, IMethodQuery methodQuery)
        {
            Guard.AgainstNull(databaseContextFactory, "databaseContextFactory");
            Guard.AgainstNull(formulaQuery, "_formulaQuery");
            Guard.AgainstNull(constraintQuery, "_constraintQuery");
            Guard.AgainstNull(constraintTypeQuery, "constraintTypeQuery");
            Guard.AgainstNull(argumentQuery, "_argumentQuery");
            Guard.AgainstNull(calculationQuery, "_calculationQuery");
            Guard.AgainstNull(operationTypeQuery, "operationTypeQueryy");
            Guard.AgainstNull(valueSourceTypeQuery, "_valueSourceTypeQuery");
            Guard.AgainstNull(decimalTableQuery, "_decimalTableQuery");
            Guard.AgainstNull(methodQuery, "_methodQuery");

            _databaseContextFactory = databaseContextFactory;
            _formulaQuery = formulaQuery;
            _decimalTableQuery = decimalTableQuery;
            _methodQuery = methodQuery;
            _calculationQuery = calculationQuery;
            _operationTypeQuery = operationTypeQuery;
            _valueSourceTypeQuery = valueSourceTypeQuery;

            _constraintQuery = constraintQuery;
            _constraintTypeQuery = constraintTypeQuery;
            _argumentQuery = argumentQuery;
        }

        public void HandleMessage(ResourceMenuRequestMessage message)
        {
            if (!message.Item.ResourceKey.Equals(ResourceKeys.Formula))
            {
                return;
            }

            Guid ownerId;
            string ownerName;

            if (message.RelatedItems.Contains(ResourceKeys.Limit))
            {
                ownerName = "Limit";
                ownerId = message.RelatedItems[ResourceKeys.Limit].Key;
            }
            ownerName = "Calculation";
            ownerId = message.RelatedItems[ResourceKeys.Calculation].Key;

            switch (message.Item.Type)
            {
                case Resource.ResourceType.Container:
                {
                    message.NavigationItems.Add(
                        NavigationItemFactory.Create(
                            new NewFormulaMessage(message.RelatedItems[ResourceKeys.Method].Key,
                                ownerName,
                                ownerId)));

                    message.NavigationItems.Add(
                        NavigationItemFactory.Create(
                            new ChangeFormulaOrderMessage(
                                message.RelatedItems[ResourceKeys.Method].Key,
                                ownerName,
                                ownerId,
                                message.RelatedItems[0].Text)));

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
                            new NewFormulaFromExistingMessage(message.RelatedItems[ResourceKeys.Method].Key,
                                ownerName, ownerId, message.Item.Key)));

                    message.NavigationItems.Add(
                        NavigationItemFactory.Create(
                            new EditFormulaMessage(message.RelatedItems[ResourceKeys.Method].Key,
                                ownerName, ownerId, message.Item.Key)));

                    message.NavigationItems.Add(
                        NavigationItemFactory.Create(
                            new DeleteFormulaMessage(
                                message.UpstreamItems[0],
                                message.Item.Text,
                                ownerName,
                                ownerId,
                                message.Item.Key)));

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

            var ownerId = message.RelatedResources.Contains(ResourceKeys.Limit)
                ? message.RelatedResources[ResourceKeys.Limit].Key
                : message.RelatedResources[ResourceKeys.Calculation].Key;

            switch (message.Resource.Type)
            {
                case Resource.ResourceType.Container:
                {
                    using (_databaseContextFactory.Create())
                    {
                        foreach (var row in _formulaQuery.AllForOwner(ownerId))
                        {
                            message.Resources.Add(
                                new Resource(ResourceKeys.Formula, FormulaColumns.Id.MapFrom(row),
                                        FormulaColumns.Description.MapFrom(row), ImageResources.Formula)
                                    .AsLeaf());
                        }
                    }

                    break;
                }
            }
        }

        public void HandleMessage(NewFormulaMessage message)
        {
            var formulaModel = BuildFormulaModel(message.MethodId,
                message.OwnerName,
                message.OwnerId);

            if (formulaModel == null)
            {
                return;
            }

            var constraintModel = BuildConstraintModel(formulaModel);

            if (constraintModel == null)
            {
                return;
            }

            var item = WorkItemManager
                .Create("New formula")
                .ControlledBy<IFormulaController>()
                .ShowIn<IContextToolbarPresenter>()
                .AddPresenter<IFormulaPresenter>().WithModel(formulaModel)
                .AddPresenter<IConstraintPresenter>().WithModel(constraintModel)
                .AddNavigationItem(
                    NavigationItemFactory.Create(message).AssignResourceItem(ResourceItems.Submit)).AsDefault()
                .AssignInitiator(message);

            HostInWorkspace<ITabbedWorkspacePresenter>(item);
        }

        public void HandleMessage(EditFormulaMessage message)
        {
            var formulaModel = BuildFormulaModel(message.MethodId,
                message.OwnerName,
                message.OwnerId);

            if (formulaModel == null)
            {
                return;
            }

            using (_databaseContextFactory.Create())
            {
                formulaModel.FormulaOperations = _formulaQuery.OperationDTOs(message.FormulaId);

                var constraintModel = BuildConstraintModel(formulaModel);

                if (constraintModel == null)
                {
                    return;
                }

                constraintModel.ConstraintRows = _constraintQuery.AllForOwner(message.FormulaId);

                var item = WorkItemManager
                    .Create("Edit formula")
                    .ControlledBy<IFormulaController>()
                    .ShowIn<IContextToolbarPresenter>()
                    .AddPresenter<IFormulaPresenter>().WithModel(formulaModel)
                    .AddPresenter<IConstraintPresenter>().WithModel(constraintModel)
                    .AddNavigationItem(
                        NavigationItemFactory.Create(message).AssignResourceItem(ResourceItems.Submit)).AsDefault()
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
                message.Item.AssignText(FormulaColumns.Description.MapFrom(_formulaQuery.Get(message.Item.Key)));
            }
        }

        public void HandleMessage(ChangeFormulaOrderMessage message)
        {
            var model = new SimpleListModel
            {
                VisibleColumns = new List<string>
                {
                    "Description"
                }
            };

            using (_databaseContextFactory.Create())
            {
                model.Rows = _formulaQuery.AllForOwner(message.OwnerId);
            }

            var item = WorkItemManager
                .Create(string.Format("'{0}' Formulas", message.OwnerText))
                .ControlledBy<IFormulaController>()
                .ShowIn<IContextToolbarPresenter>()
                .AddPresenter<ISimpleListPresenter>().WithModel(model)
                .AddNavigationItem(NavigationItemFactory.Create(message).AssignResourceItem(ResourceItems.Submit))
                .AddNavigationItem(NavigationItemFactory.Create<MoveDownMessage>())
                .AddNavigationItem(NavigationItemFactory.Create<MoveUpMessage>())
                .AssignInitiator(message);

            HostInWorkspace<ITabbedWorkspacePresenter>(item);
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
            var formulaModel = BuildFormulaModel(message.MethodId,
                message.OwnerName,
                message.OwnerId);

            if (formulaModel == null)
            {
                return;
            }

            using (_databaseContextFactory.Create())
            {
                formulaModel.FormulaOperations = _formulaQuery.OperationDTOs(message.FormulaId);
            }

            var constraintModel = BuildConstraintModel(formulaModel);

            if (constraintModel == null)
            {
                return;
            }

            using (_databaseContextFactory.Create())
            {
                constraintModel.ConstraintRows = _constraintQuery.AllForOwner(message.FormulaId);
            }

            var item = WorkItemManager
                .Create("New formula")
                .ControlledBy<IFormulaController>()
                .ShowIn<IContextToolbarPresenter>()
                .AddPresenter<IFormulaPresenter>().WithModel(formulaModel)
                .AddPresenter<IConstraintPresenter>().WithModel(constraintModel)
                .AddNavigationItem(
                    NavigationItemFactory.Create(new NewFormulaMessage(message))
                        .AssignResourceItem(ResourceItems.Submit)).
                AsDefault()
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
                        var ownerId = message.RelatedItems.Contains(ResourceKeys.Limit)
                            ? message.RelatedItems[ResourceKeys.Limit].Key
                            : message.RelatedItems[ResourceKeys.Calculation].Key;

                        message.AddTable("Formulas",
                            _formulaQuery.AllForOwner(ownerId));

                        break;
                    }
                    case Resource.ResourceType.Item:
                    {
                        //var result = _formulaQuery.OperationsSummary(message.Item.Key);
                        var result = _formulaQuery.Get(message.Item.Key);

                        message.AddTable("Formula Operations", new[] {result});

                        break;
                    }
                }
            }
        }

        private ConstraintModel BuildConstraintModel(FormulaModel formulaModel)
        {
            return new ConstraintModel
            {
                ArgumentRows = _argumentQuery.All(),
                ConstraintRows = _constraintQuery.AllForOwner(formulaModel.Id),
                ConstraintTypeRows = _constraintTypeQuery.All()
            };
        }

        private FormulaModel BuildFormulaModel(Guid methodId, string ownerName, Guid ownerId)
        {
            using (_databaseContextFactory.Create())
            {
                return new FormulaModel
                {
                    DecimalTables = _decimalTableQuery.All(),
                    ArgumentRows = _argumentQuery.All(),
                    PrecedingCalculations =
                        ownerName.ToLower() == "calculation"
                            ? _calculationQuery.DTOsBeforeCalculation(methodId, ownerId)
                            : _calculationQuery.DTOsForMethod(methodId),
                    OperationTypes = _operationTypeQuery.AllDTOs(),
                    ValueSourceTypes = _valueSourceTypeQuery.AllDTOs(),
                    Methods = _methodQuery.AllDTOs()
                };
            }
        }
    }
}