using System;
using Shuttle.Abacus.DataAccess;
using Shuttle.Abacus.Infrastructure;
using Shuttle.Abacus.Localisation;
using Shuttle.Abacus.UI.Coordinators.Interfaces;
using Shuttle.Abacus.UI.Core.Presentation;
using Shuttle.Abacus.UI.Core.Resources;
using Shuttle.Abacus.UI.Messages.Formula;
using Shuttle.Abacus.UI.Messages.Resources;
using Shuttle.Abacus.UI.Models;
using Shuttle.Abacus.UI.Navigation;
using Shuttle.Abacus.UI.UI.Formula;
using Shuttle.Abacus.UI.UI.FormulaOperation;
using Shuttle.Abacus.UI.UI.Shell.TabbedWorkspace;
using Shuttle.Abacus.UI.UI.WorkItem.ContextToolbar;
using Shuttle.Core.Data;
using Shuttle.Core.Infrastructure;

namespace Shuttle.Abacus.UI.Coordinators
{
    public class FormulaOperationCoordinator : Coordinator, IFormulaOperationCoordinator
    {
        private readonly IArgumentQuery _argumentQuery;
        private readonly IOperationTypeQuery _constraintTypeQuery;
        private readonly IDatabaseContextFactory _databaseContextFactory;
        private readonly IFormulaQuery _formulaQuery;
        private readonly IValueSourceTypeQuery _valueSourceTypeQuery;
        private readonly IMatrixQuery _matrixQuery;

        public FormulaOperationCoordinator(IDatabaseContextFactory databaseContextFactory, IFormulaQuery formulaQuery,
            IArgumentQuery argumentQuery, IOperationTypeQuery constraintTypeQuery, IValueSourceTypeQuery valueSourceTypeQuery,
            IMatrixQuery matrixQuery)
        {
            Guard.AgainstNull(databaseContextFactory, "databaseContextFactory");
            Guard.AgainstNull(formulaQuery, "formulaQuery");
            Guard.AgainstNull(argumentQuery, "argumentQuery");
            Guard.AgainstNull(constraintTypeQuery, "constraintTypeQuery");
            Guard.AgainstNull(valueSourceTypeQuery, "valueSourceTypeQuery");
            Guard.AgainstNull(matrixQuery, "matrixQuery");

            _databaseContextFactory = databaseContextFactory;
            _formulaQuery = formulaQuery;
            _argumentQuery = argumentQuery;
            _constraintTypeQuery = constraintTypeQuery;
            _valueSourceTypeQuery = valueSourceTypeQuery;
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
                        var formulaId = message.RelatedResources[ResourceKeys.Formula].Key;

                        using (_databaseContextFactory.Create())
                        {
                            foreach (var row in _formulaQuery.Operations(formulaId))
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
                    OperationTypes = _constraintTypeQuery.All().Map(row => OperationTypeColumns.Name.MapFrom(row)),
                    Arguments = _argumentQuery.All().Map(row => new ArgumentModel(row)),
                    FormulaOperations =
                        _formulaQuery.Operations(message.FormulaId).Map(row => new FormulaOperationModel(row)),
                    ValueSourceTypes = _valueSourceTypeQuery.All().Map(row => new ValueSourceTypeModel(row)),
                    Matrixes = _matrixQuery.All().Map(row => new MatrixModel(row)),
                    Formulas = _formulaQuery.All().Map(row => new FormulaModel(row))
                };

                var item = WorkItemManager
                    .Create(string.Format("Formula operations: {0}", message.FormulaName))
                    .ControlledBy<IFormulaController>()
                    .ShowIn<IContextToolbarPresenter>()
                    .AddPresenter<IFormulaOperationPresenter>().WithModel(model)
                    .AddNavigationItem(new NavigationItem(new ResourceItem("Submit")).AssignMessage(message)).AsDefault()
                    .AssignInitiator(message);

                HostInWorkspace<ITabbedWorkspacePresenter>(item);
            }
        }
    }
}