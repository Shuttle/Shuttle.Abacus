using System;
using Shuttle.Abacus.DataAccess;
using Shuttle.Abacus.Infrastructure;
using Shuttle.Abacus.Localisation;
using Shuttle.Abacus.Shell.Coordinators.Interfaces;
using Shuttle.Abacus.Shell.Core.Presentation;
using Shuttle.Abacus.Shell.Core.Resources;
using Shuttle.Abacus.Shell.Messages.Formula;
using Shuttle.Abacus.Shell.Messages.Resources;
using Shuttle.Abacus.Shell.Models;
using Shuttle.Abacus.Shell.Navigation;
using Shuttle.Abacus.Shell.UI.Formula;
using Shuttle.Abacus.Shell.UI.FormulaConstraint;
using Shuttle.Abacus.Shell.UI.Shell.TabbedWorkspace;
using Shuttle.Abacus.Shell.UI.WorkItem.ContextToolbar;
using Shuttle.Core.Data;
using Shuttle.Core.Infrastructure;

namespace Shuttle.Abacus.Shell.Coordinators
{
    public class FormulaConstraintCoordinator : Coordinator, IFormulaConstraintCoordinator
    {
        private readonly IArgumentQuery _argumentQuery;
        private readonly IConstraintTypeQuery _constraintTypeQuery;
        private readonly IFormulaQuery _formulaQuery;
        private readonly IDatabaseContextFactory _databaseContextFactory;

        public FormulaConstraintCoordinator(IDatabaseContextFactory databaseContextFactory,
            IArgumentQuery argumentQuery, IConstraintTypeQuery constraintTypeQuery, IFormulaQuery formulaQuery)
        {
            Guard.AgainstNull(databaseContextFactory, "databaseContextFactory");
            Guard.AgainstNull(argumentQuery, "argumentQuery");
            Guard.AgainstNull(constraintTypeQuery, "constraintTypeQuery");
            Guard.AgainstNull(formulaQuery, "formulaQuery");

            _databaseContextFactory = databaseContextFactory;
            _argumentQuery = argumentQuery;
            _constraintTypeQuery = constraintTypeQuery;
            _formulaQuery = formulaQuery;
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
                        foreach (var row in _formulaQuery.Constraints(formulaId))
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
                        new NavigationItem(new ResourceItem("Manage", "FormulaConstraint")).AssignMessage(
                            new ManageFormulaConstraintsMessage(message.RelatedItems[ResourceKeys.Formula].Text,
                                message.RelatedItems[ResourceKeys.Formula].Key)));

                    break;
                }
            }
        }

        public void HandleMessage(ManageFormulaConstraintsMessage message)
        {
            using (_databaseContextFactory.Create())
            {
                var model = new ManageFormulaConstraintsModel
                {
                    ConstraintTypes = _constraintTypeQuery.All().Map(row => ConstraintTypeColumns.Name.MapFrom(row)),
                    Arguments = _argumentQuery.All().Map(row => new ArgumentModel(row)),
                    Constraints = _formulaQuery.Constraints(message.FormulaId).Map(row => new FormulaConstraintModel(row))
                };

                var item = WorkItemManager
                    .Create(string.Format("Formula constraints: {0}", message.FormulaName))
                    .ControlledBy<IFormulaController>()
                    .ShowIn<IContextToolbarPresenter>()
                    .AddPresenter<IFormulaConstraintPresenter>()
                    .WithModel(model)
                    .AddNavigationItem(new NavigationItem(new ResourceItem("Submit")).AssignMessage(message))
                    .AsDefault()
                    .AssignInitiator(message);

                HostInWorkspace<ITabbedWorkspacePresenter>(item);
            }
        }
    }
}