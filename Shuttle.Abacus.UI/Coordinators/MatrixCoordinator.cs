using System;
using Shuttle.Abacus.DataAccess;
using Shuttle.Abacus.Infrastructure;
using Shuttle.Abacus.Localisation;
using Shuttle.Abacus.Shell.Core.Presentation;
using Shuttle.Abacus.Shell.Core.Resources;
using Shuttle.Abacus.Shell.Messages.Core;
using Shuttle.Abacus.Shell.Messages.DecimalTable;
using Shuttle.Abacus.Shell.Messages.Explorer;
using Shuttle.Abacus.Shell.Messages.Resources;
using Shuttle.Abacus.Shell.Models;
using Shuttle.Abacus.Shell.Navigation;
using Shuttle.Abacus.Shell.UI.Matrix;
using Shuttle.Abacus.Shell.UI.Shell.TabbedWorkspace;
using Shuttle.Abacus.Shell.UI.WorkItem.ContextToolbar;
using Shuttle.Core.Data;
using Shuttle.Core.Infrastructure;

namespace Shuttle.Abacus.Shell.Coordinators
{
    public class MatrixCoordinator : Coordinator, IMatrixCoordinator
    {
        private readonly INavigationItem _register = new NavigationItem(new ResourceItem("Register", "Matrix"));
        private readonly IDatabaseContextFactory _databaseContextFactory;
        private readonly IMatrixQuery _matrixQuery;

        public MatrixCoordinator(IDatabaseContextFactory databaseContextFactory, IMatrixQuery matrixQuery)
        {
            Guard.AgainstNull(databaseContextFactory, "databaseContextFactory");
            Guard.AgainstNull(matrixQuery, "matrixQuery");

            _databaseContextFactory = databaseContextFactory;
            _matrixQuery = matrixQuery;
        }

        public void HandleMessage(ExplorerInitializeMessage message)
        {
            if (!Permissions.Matrix.IsSatisfiedBy(Session.Permissions))
            {
                return;
            }

            message.Items.Add(
                new Resource(ResourceKeys.Matrix, Guid.NewGuid(), "Matrixes", ImageResources.Matrix)
                    .AsContainer());
        }

        public void HandleMessage(ResourceMenuRequestMessage message)
        {
            if (!message.Item.ResourceKey.Equals(ResourceKeys.Matrix))
            {
                return;
            }

            switch (message.Item.Type)
            {
                case Resource.ResourceType.Container:
                {
                    message.NavigationItems.Add(_register
                        .AssignMessage(new RegisterMatrixMessage(Guid.Empty)));

                    break;
                }
                case Resource.ResourceType.Item:
                {
                    message.NavigationItems.Add(_register
                        .AssignMessage(new RegisterMatrixMessage(message.Item.Key)));

                    break;
                }
            }
        }

        public void HandleMessage(PopulateResourceMessage message)
        {
            if (!message.Resource.ResourceKey.Equals(ResourceKeys.Matrix))
            {
                return;
            }

            using (_databaseContextFactory.Create())
            {
                switch (message.Resource.Type)
                {
                    case Resource.ResourceType.Container:
                    {
                        foreach (
                            var row in
                            _matrixQuery.All())
                        {
                            message.Resources.Add(new Resource(ResourceKeys.Matrix,
                                MatrixColumns.Id.MapFrom(row),
                                MatrixColumns.Name.MapFrom(row),
                                ImageResources.Matrix).AsLeaf());
                        }

                        break;
                    }
                }
            }
        }

        public void HandleMessage(ResourceRefreshItemTextMessage message)
        {
            if (!message.Item.ResourceKey.Equals(ResourceKeys.Matrix) ||
                message.Item.Type != Resource.ResourceType.Item)
            {
                return;
            }

            using (_databaseContextFactory.Create())
            {
                message.Item.AssignText(MatrixColumns.Name.MapFrom(_matrixQuery.Get(message.Item.Key)));
            }
        }

        public void HandleMessage(RegisterMatrixMessage message)
        {
            using (_databaseContextFactory.Create())
            {
                var model = new MatrixModel();

                var item = WorkItemManager
                    .Create("New Decimal Table")
                    .ControlledBy<IMatrixController>()
                    .ShowIn<IContextToolbarPresenter>()
                    .AddPresenter<IMatrixPresenter>().WithModel(model)
                    .AddNavigationItem(_register.WithResourceItem(ResourceItems.Submit)).AsDefault()
                    .AssignInitiator(message);

                HostInWorkspace<ITabbedWorkspacePresenter>(item);
            }
        }


        public void HandleMessage(SummaryViewRequestedMessage message)
        {
            if (SummaryViewManager.CanIgnore(message, ResourceKeys.Matrix))
            {
                return;
            }

            using (_databaseContextFactory.Create())
            {
                switch (message.Item.Type)
                {
                    case Resource.ResourceType.Container:
                    {
                        message.AddTable("DecimalTables", _matrixQuery.All());

                        break;
                    }
                    case Resource.ResourceType.Item:
                    {
                        break;
                    }
                }
            }
        }
    }
}