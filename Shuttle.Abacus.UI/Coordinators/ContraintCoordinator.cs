using System;
using Shuttle.Abacus.DataAccess;
using Shuttle.Abacus.Localisation;
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
using Shuttle.Core.Data;
using Shuttle.Core.Infrastructure;

namespace Shuttle.Abacus.UI.Coordinators
{
    public class ConstraintCoordinator : Coordinator, IConstraintCoordinator
    {
        private readonly IArgumentQuery _argumentQuery;
        private readonly IConstraintQuery _constraintQuery;
        private readonly IDatabaseContextFactory _databaseContextFactory;

        public ConstraintCoordinator(IDatabaseContextFactory databaseContextFactory, IConstraintQuery constraintQuery,
            IArgumentQuery argumentQuery)
        {
            Guard.AgainstNull(databaseContextFactory, "databaseContextFactory");
            Guard.AgainstNull(constraintQuery, "constraintQuery");
            Guard.AgainstNull(argumentQuery, "argumentQuery");

            _databaseContextFactory = databaseContextFactory;
            _constraintQuery = constraintQuery;
            _argumentQuery = argumentQuery;
        }

        public void HandleMessage(PopulateResourceMessage message)
        {
            if (!message.Resource.ResourceKey.Equals(ResourceKeys.Constraint))
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

                    using (_databaseContextFactory.Create())
                    {
                        foreach (var row in _constraintQuery.QueryAllForOwner(ownerId))
                        {
                            message.Resources.Add(
                                new Resource(ResourceKeys.Constraint, Guid.Empty,
                                        ConstraintColumns.Description.MapFrom(row), ImageResources.Constraint)
                                    .AsLeaf());
                        }
                    }

                    break;
                }
            }
        }

        public void HandleMessage(ResourceMenuRequestMessage message)
        {
            if (!message.Item.ResourceKey.Equals(ResourceKeys.Constraint))
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
            using (_databaseContextFactory.Create())
            {
                return new ConstraintModel
                {
                    ArgumentRows = _argumentQuery.AllDTOs(),
                    //TODO
                    //ConstraintTypes = _constraintQuery.ConstraintTypes(),
                    Constraints = _constraintQuery.DTOsForOwner(calculationId)
                };
            }
        }
    }
}