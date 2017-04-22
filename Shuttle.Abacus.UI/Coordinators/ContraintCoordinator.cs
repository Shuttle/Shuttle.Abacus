using System;
using System.Data;
using Shuttle.Abacus.DataAccess;
using Shuttle.Abacus.Domain;
using Shuttle.Abacus.UI.Coordinators.Interfaces;
using Shuttle.Abacus.UI.Core.Presentation;
using Shuttle.Abacus.UI.Core.Resources;
using Shuttle.Abacus.UI.Messages.Calculation;
using Shuttle.Abacus.UI.Messages.Resources;
using Shuttle.Abacus.UI.Models;
using Shuttle.Abacus.UI.UI.Constraint;
using Shuttle.Abacus.UI.UI.Shell.TabbedWorkspace;
using Shuttle.Abacus.UI.UI.WorkItem.ContextToolbar;
using Shuttle.Abacus.UI.WorkItemControllers.Interfaces;

namespace Shuttle.Abacus.UI.Coordinators
{
    public class ConstraintCoordinator : Coordinator, IConstraintCoordinator
    {
        private readonly IConstraintQuery constraintQuery;
        private readonly IArgumentQuery argumentQuery;

        public ConstraintCoordinator(IConstraintQuery constraintQuery, IArgumentQuery argumentQuery)
        {
            this.constraintQuery = constraintQuery;
            this.argumentQuery = argumentQuery;
        }

        public void HandleMessage(PopulateResourceMessage message)
        {
            if (message.Resource.ResourceKey !=
                ResourceKeys.Constraint)
            {
                return;
            }

            switch (message.Resource.Type)
            {
                case Resource.ResourceType.Container:
                    {
                        var ownerId = message.RelatedResources.Contains(ResourceKeys.Formula)
                                          ? message.RelatedResources[ResourceKeys.Formula].Key
                                          : message.RelatedResources[ResourceKeys.Calculation].Key;

                        foreach (DataRow row in constraintQuery.QueryAllForOwner(ownerId).Table.Rows)
                        {
                            message.Resources.Add(
                                new Resource(ResourceKeys.Constraint, Guid.Empty,
                                                 ConstraintColumns.Description.MapFrom(row), ImageResources.Constraint)
                                    .AsLeaf());
                        }


                        break;
                    }
            }
        }

        public void HandleMessage(ResourceMenuRequestMessage message)
        {
            if (message.Item.ResourceKey !=
                ResourceKeys.Constraint)
            {
                return;
            }

            switch (message.Item.Type)
            {
                case Resource.ResourceType.Container:
                    {
                        message.NavigationItems.Add(
                            NavigationItemFactory.Create(
                                new ManageCalculationConstraintsMessage(message.RelatedItems[ResourceKeys.Calculation].Text,
                                    message.RelatedItems[ResourceKeys.Calculation].Key)));

                        break;
                    }
            }
        }

        public void HandleMessage(ManageCalculationConstraintsMessage message)
        {
            var constraintModel = BuildConstraintModel(message.CalculationId);

            if (constraintModel == null)
            {
                return;
            }

            var item = WorkItemManager
                .Create(string.Format("Calculation constraints: {0}", message.CalculationName))
                .ControlledBy<ICalculationController>()
                .ShowIn<IContextToolbarPresenter>()
                .AddPresenter<IConstraintPresenter>().WithModel(constraintModel)
                .AddNavigationItem(
                NavigationItemFactory.Create(message).AssignResourceItem(ResourceItems.Submit)).AsDefault()
                .AssignInitiator(message);

            HostInWorkspace<ITabbedWorkspacePresenter>(item);
        }

        private ConstraintModel BuildConstraintModel(Guid calculationId)
        {
            return new ConstraintModel
                   {
                       Arguments = argumentQuery.AllDTOs(),
                       ConstraintTypes = constraintQuery.ConstraintTypes(),
                       Constraints = constraintQuery.DTOsForOwner(calculationId)
                   };
        }
    }
}
