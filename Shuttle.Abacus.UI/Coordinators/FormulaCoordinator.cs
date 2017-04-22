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

namespace Shuttle.Abacus.UI.Coordinators
{
    public class FormulaCoordinator : Coordinator, IFormulaCoordinator
    {
        private readonly IArgumentQuery argumentQuery;
        private readonly ICalculationQuery calculationQuery;
        private readonly IConstraintQuery constraintQuery;
        private readonly IDecimalTableQuery decimalTableQuery;
        private readonly IFormulaQuery formulaQuery;
        private readonly IMethodQuery methodQuery;
        private readonly IOperationTypeQuery operationTypeQuery;
        private readonly IValueSourceTypeQuery valueSourceTypeQuery;

        public FormulaCoordinator(IFormulaQuery formulaQuery, IConstraintQuery constraintQuery,
            IArgumentQuery argumentQuery, ICalculationQuery calculationQuery,
            IOperationTypeQuery operationTypeQuery, IValueSourceTypeQuery valueSourceTypeQuery,
            IDecimalTableQuery decimalTableQuery, IMethodQuery methodQuery)
        {
            this.formulaQuery = formulaQuery;
            this.decimalTableQuery = decimalTableQuery;
            this.methodQuery = methodQuery;
            this.calculationQuery = calculationQuery;
            this.operationTypeQuery = operationTypeQuery;
            this.valueSourceTypeQuery = valueSourceTypeQuery;

            this.constraintQuery = constraintQuery;
            this.argumentQuery = argumentQuery;
        }

        public void HandleMessage(ResourceMenuRequestMessage message)
        {
            if (message.Item.ResourceKey !=
                ResourceKeys.Formula)
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
            else
            {
                ownerName = "Calculation";
                ownerId = message.RelatedItems[ResourceKeys.Calculation].Key;
            }

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
            if (message.Resource.ResourceKey !=
                ResourceKeys.Formula)
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
                    foreach (var row in formulaQuery.AllForOwner(ownerId))
                    {
                        message.Resources.Add(
                            new Resource(ResourceKeys.Formula, FormulaColumns.Id.MapFrom(row),
                                    FormulaColumns.Description.MapFrom(row), ImageResources.Formula)
                                .AsLeaf());
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

            formulaModel.FormulaOperations = formulaQuery.OperationDTOs(message.FormulaId);

            var constraintModel = BuildConstraintModel(formulaModel);

            if (constraintModel == null)
            {
                return;
            }

            constraintModel.Constraints = constraintQuery.DTOsForOwner(message.FormulaId);

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

        public void HandleMessage(ResourceRefreshItemTextMessage message)
        {
            if (message.Item.ResourceKey != ResourceKeys.Formula ||
                message.Item.Type != Resource.ResourceType.Item)
            {
                return;
            }

            message.Item.AssignText(FormulaColumns.Description.MapFrom(formulaQuery.Get(message.Item.Key)));
        }

        public void HandleMessage(ChangeFormulaOrderMessage message)
        {
            var model = new SimpleListModel
            {
                Rows = formulaQuery.AllForOwner(message.OwnerId),
                VisibleColumns = new List<string>
                {
                    "Description"
                }
            };

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

            formulaModel.FormulaOperations = formulaQuery.OperationDTOs(message.FormulaId);

            var constraintModel = BuildConstraintModel(formulaModel);

            if (constraintModel == null)
            {
                return;
            }

            constraintModel.Constraints = constraintQuery.DTOsForOwner(message.FormulaId);

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

            switch (message.Item.Type)
            {
                case Resource.ResourceType.Container:
                {
                    var ownerId = message.RelatedItems.Contains(ResourceKeys.Limit)
                        ? message.RelatedItems[ResourceKeys.Limit].Key
                        : message.RelatedItems[ResourceKeys.Calculation].Key;

                    message.AddTable("Formulas",
                        formulaQuery.AllForOwner(ownerId));

                    break;
                }
                case Resource.ResourceType.Item:
                {
                    //var result = formulaQuery.OperationsSummary(message.Item.Key);
                    var result = formulaQuery.Get(message.Item.Key);

                    message.AddTable("Formula Operations", new[] {result});

                    break;
                }
            }
        }

        private ConstraintModel BuildConstraintModel(FormulaModel formulaModel)
        {
            return new ConstraintModel
            {
                Arguments = formulaModel.Arguments
                //TODO
                //ConstraintTypes = constraintQuery.ConstraintTypes()
            };
        }

        private FormulaModel BuildFormulaModel(Guid methodId, string ownerName, Guid ownerId)
        {
            return new FormulaModel
            {
                DecimalTables = decimalTableQuery.AllDTOs(),
                Arguments = argumentQuery.AllDTOs(),
                PrecedingCalculations =
                    ownerName.ToLower() == "calculation"
                        ? calculationQuery.DTOsBeforeCalculation(methodId, ownerId)
                        : calculationQuery.DTOsForMethod(methodId),
                OperationTypes = operationTypeQuery.AllDTOs(),
                ValueSourceTypes = valueSourceTypeQuery.AllDTOs(),
                Methods = methodQuery.AllDTOs()
            };
        }
    }
}