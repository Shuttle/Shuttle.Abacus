using System;
using Shuttle.Abacus.DataAccess;
using Shuttle.Abacus.Infrastructure;
using Shuttle.Abacus.Localisation;
using Shuttle.Abacus.Shell.Core.Presentation;
using Shuttle.Abacus.Shell.Core.Resources;
using Shuttle.Abacus.Shell.Messages.Formula;
using Shuttle.Abacus.Shell.Messages.Resources;
using Shuttle.Abacus.Shell.Models;
using Shuttle.Abacus.Shell.Navigation;
using Shuttle.Abacus.Shell.UI.Formula;
using Shuttle.Abacus.Shell.UI.Formula.FormulaOperation;
using Shuttle.Abacus.Shell.UI.Shell.TabbedWorkspace;
using Shuttle.Abacus.Shell.UI.WorkItem.ContextToolbar;
using Shuttle.Core.Data;
using Shuttle.Core.Infrastructure;

namespace Shuttle.Abacus.Shell.Coordinators
{
    public class FormulaOperationCoordinator : Coordinator, IFormulaOperationCoordinator
    {
        private readonly IArgumentQuery _argumentQuery;
        private readonly IDatabaseContextFactory _databaseContextFactory;
        private readonly IFormulaQuery _formulaQuery;
        private readonly IMatrixQuery _matrixQuery;

        public FormulaOperationCoordinator(IDatabaseContextFactory databaseContextFactory, IFormulaQuery formulaQuery,
            IArgumentQuery argumentQuery, IMatrixQuery matrixQuery)
        {
            Guard.AgainstNull(databaseContextFactory, "databaseContextFactory");
            Guard.AgainstNull(formulaQuery, "formulaQuery");
            Guard.AgainstNull(argumentQuery, "argumentQuery");
            Guard.AgainstNull(matrixQuery, "matrixQuery");

            _databaseContextFactory = databaseContextFactory;
            _formulaQuery = formulaQuery;
            _argumentQuery = argumentQuery;
            _matrixQuery = matrixQuery;
        }

        public void HandleMessage(PopulateResourceMessage message)
        {
            if (!message.Resource.ResourceKey.Equals(ResourceKeys.FormulaOperation))
            {
                return;
            }

            switch (message.Resource.Type)
            {
                case Resource.ResourceType.Container:
                {
                    using (_databaseContextFactory.Create())
                    {
                        foreach (var row in _formulaQuery.Operations(message.RelatedResources[ResourceKeys.Formula].Key))
                        {
                            message.Resources.Add(
                                new Resource(ResourceKeys.FormulaOperation, Guid.Empty,
                                        FormulaColumns.OperationColumns.ValueSource.MapFrom(row),
                                        ImageResources.FormulaOperation)
                                    .AsLeaf());
                        }
                    }

                    break;
                }
            }
        }

        public void HandleMessage(ResourceMenuRequestMessage message)
        {
            if (!message.Item.ResourceKey.Equals(ResourceKeys.FormulaOperation))
            {
                return;
            }

            switch (message.Item.Type)
            {
                case Resource.ResourceType.Container:
                {
                    message.NavigationItems.Add(
                        new NavigationItem(new ResourceItem("Manage", "FormulaOperation")).AssignMessage(
                            new ManageFormulaOperationsMessage(message.RelatedItems[ResourceKeys.Formula].Text,
                                message.RelatedItems[ResourceKeys.Formula].Key)));

                    break;
                }
            }
        }

        public void HandleMessage(ManageFormulaOperationsMessage message)
        {
            using (_databaseContextFactory.Create())
            {
                var model = new ManageFormulaOperationsModel
                {
                    Arguments = _argumentQuery.All().Map(row => new ArgumentModel(row)),
                    FormulaOperations = _formulaQuery.Operations(message.FormulaId).Map(row => new FormulaOperationModel(row)),
                    Matrixes = _matrixQuery.All().Map(row => new MatrixModel(row)),
                    Formulas = _formulaQuery.All().Map(row => new FormulaModel(row))
                };

                var item = WorkItemManager
                    .Create(string.Format("Formula operations: {0}", message.FormulaName))
                    .ControlledBy<IFormulaController>()
                    .ShowIn<IContextToolbarPresenter>()
                    .AddPresenter<IFormulaOperationPresenter>()
                    .WithModel(model)
                    .AddNavigationItem(new NavigationItem(new ResourceItem("Submit")).AssignMessage(message)).AsDefault()
                    .AssignInitiator(message);

                HostInWorkspace<ITabbedWorkspacePresenter>(item);
            }
        }
    }
}