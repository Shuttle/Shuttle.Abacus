using System;
using Shuttle.Abacus.DataAccess;
using Shuttle.Abacus.Localisation;
using Shuttle.Abacus.UI.Coordinators.Interfaces;
using Shuttle.Abacus.UI.Core.Presentation;
using Shuttle.Abacus.UI.Core.Resources;
using Shuttle.Abacus.UI.Messages.Formula;
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
        private readonly IConstraintTypeQuery _constraintTypeQuery;
        private readonly IConstraintQuery _constraintQuery;
        private readonly IDatabaseContextFactory _databaseContextFactory;

        public ConstraintCoordinator(IDatabaseContextFactory databaseContextFactory, IConstraintQuery constraintQuery,
            IArgumentQuery argumentQuery, IConstraintTypeQuery constraintTypeQuery)
        {
            Guard.AgainstNull(databaseContextFactory, "databaseContextFactory");
            Guard.AgainstNull(constraintQuery, "constraintQuery");
            Guard.AgainstNull(argumentQuery, "argumentQuery");
            Guard.AgainstNull(constraintTypeQuery, "constraintTypeQuery");

            _databaseContextFactory = databaseContextFactory;
            _constraintQuery = constraintQuery;
            _argumentQuery = argumentQuery;
            _constraintTypeQuery = constraintTypeQuery;
        }

        public void HandleMessage(PopulateResourceMessage message)
        {
            if (!message.Resource.ResourceKey.Equals(ResourceKeys.FormulaConstraint))
            {
                return;
            }

            switch (message.Resource.Type)
            {
                case Resource.ResourceType.Container:
                {
                    var ownerId = message.RelatedResources.Contains(ResourceKeys.Formula)
                        ? message.RelatedResources[ResourceKeys.Formula].Key
                        : message.RelatedResources[ResourceKeys.Formula].Key;

                    using (_databaseContextFactory.Create())
                    {
                        foreach (var row in _constraintQuery.AllForOwner(ownerId))
                        {
                            message.Resources.Add(
                                new Resource(ResourceKeys.FormulaConstraint, Guid.Empty,
                                        FormulaColumns.ConstraintColumns.ArgumentName.MapFrom(row), ImageResources.FormulaConstraint)
                                    .AsLeaf());
                        }
                    }

                    break;
                }
            }
        }

        public void HandleMessage(ResourceMenuRequestMessage message)
        {
            if (!message.Item.ResourceKey.Equals(ResourceKeys.FormulaConstraint))
            {
                return;
            }

            switch (message.Item.Type)
            {
                case Resource.ResourceType.Container:
                {
                    message.NavigationItems.Add(
                        NavigationItemFactory.Create(
                            new ManageFormulaConstraintsMessage(message.RelatedItems[ResourceKeys.Formula].Text,
                                message.RelatedItems[ResourceKeys.Formula].Key)));

                    break;
                }
            }
        }

        public void HandleMessage(ManageFormulaConstraintsMessage message)
        {
            var constraintModel = BuildConstraintModel(message.FormulaId);

            if (constraintModel == null)
            {
                return;
            }

            var item = WorkItemManager
                .Create(string.Format("Formula constraints: {0}", message.FormulaName))
                .ControlledBy<IFormulaController>()
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
                    ArgumentRows = _argumentQuery.All(),
                    ConstraintTypeRows = _constraintTypeQuery.All(),
                    ConstraintRows = _constraintQuery.AllForOwner(calculationId)
                };
            }
        }
    }
}