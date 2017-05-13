using System;
using Shuttle.Abacus.DataAccess;
using Shuttle.Abacus.Localisation;
using Shuttle.Abacus.UI.Coordinators.Interfaces;
using Shuttle.Abacus.UI.Core.Presentation;
using Shuttle.Abacus.UI.Core.Resources;
using Shuttle.Abacus.UI.Messages.Formula;
using Shuttle.Abacus.UI.Messages.Resources;
using Shuttle.Abacus.UI.Models;
using Shuttle.Abacus.UI.UI.Formula;
using Shuttle.Abacus.UI.UI.FormulaConstraint;
using Shuttle.Abacus.UI.UI.Shell.TabbedWorkspace;
using Shuttle.Abacus.UI.UI.WorkItem.ContextToolbar;
using Shuttle.Core.Data;
using Shuttle.Core.Infrastructure;

namespace Shuttle.Abacus.UI.Coordinators
{
    public class FormulaConstraintCoordinator : Coordinator, IFormulaConstraintCoordinator
    {
        private readonly IArgumentQuery _argumentQuery;
        private readonly IConstraintTypeQuery _constraintTypeQuery;
        private readonly IFormulaConstraintQuery _formulaConstraintQuery;
        private readonly IDatabaseContextFactory _databaseContextFactory;

        public FormulaConstraintCoordinator(IDatabaseContextFactory databaseContextFactory, IFormulaConstraintQuery formulaConstraintQuery,
            IArgumentQuery argumentQuery, IConstraintTypeQuery constraintTypeQuery)
        {
            Guard.AgainstNull(databaseContextFactory, "databaseContextFactory");
            Guard.AgainstNull(formulaConstraintQuery, "formulaConstraintQuery");
            Guard.AgainstNull(argumentQuery, "argumentQuery");
            Guard.AgainstNull(constraintTypeQuery, "constraintTypeQuery");

            _databaseContextFactory = databaseContextFactory;
            _formulaConstraintQuery = formulaConstraintQuery;
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
                    var formulaId = message.RelatedResources[ResourceKeys.Formula].Key;

                    using (_databaseContextFactory.Create())
                    {
                        foreach (var row in _formulaConstraintQuery.AllForOwner(formulaId))
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
                .AddPresenter<IFormulaConstraintPresenter>().WithModel(constraintModel)
                .AddNavigationItem(
                    NavigationItemFactory.Create(message).WithResourceItem(ResourceItems.Submit)).AsDefault()
                .AssignInitiator(message);

            HostInWorkspace<ITabbedWorkspacePresenter>(item);
        }

        private FormulaConstraintModel BuildConstraintModel(Guid calculationId)
        {
            using (_databaseContextFactory.Create())
            {
                return new FormulaConstraintModel
                {
                    ArgumentRows = _argumentQuery.All(),
                    ConstraintTypeRows = _constraintTypeQuery.All(),
                    ConstraintRows = _formulaConstraintQuery.AllForOwner(calculationId)
                };
            }
        }
    }
}