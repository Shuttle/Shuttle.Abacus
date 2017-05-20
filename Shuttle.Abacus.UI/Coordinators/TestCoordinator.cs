using System;
using Shuttle.Abacus.DataAccess;
using Shuttle.Abacus.Infrastructure;
using Shuttle.Abacus.Localisation;
using Shuttle.Abacus.Shell.Core.Presentation;
using Shuttle.Abacus.Shell.Core.Resources;
using Shuttle.Abacus.Shell.Messages.Core;
using Shuttle.Abacus.Shell.Messages.Explorer;
using Shuttle.Abacus.Shell.Messages.Formula;
using Shuttle.Abacus.Shell.Messages.Resources;
using Shuttle.Abacus.Shell.Messages.Test;
using Shuttle.Abacus.Shell.Models;
using Shuttle.Abacus.Shell.Navigation;
using Shuttle.Abacus.Shell.UI.Shell.TabbedWorkspace;
using Shuttle.Abacus.Shell.UI.Test;
using Shuttle.Abacus.Shell.UI.WorkItem.ContextToolbar;
using Shuttle.Core.Data;
using Shuttle.Core.Infrastructure;

namespace Shuttle.Abacus.Shell.Coordinators
{
    public class TestCoordinator : Coordinator, ITestCoordinator
    {
        private readonly IDatabaseContextFactory _databaseContextFactory;

        private readonly IFormulaQuery _formulaQuery;

        private readonly INavigationItem _register =
            new NavigationItem(new ResourceItem("Register", "Test")).AssignMessage(new RegisterTestMessage());

        private readonly INavigationItem _remove = new NavigationItem(new ResourceItem("Remove", "Test"));
        private readonly INavigationItem _rename = new NavigationItem(new ResourceItem("Rename", "Test"));
        private readonly ITestQuery _testQuery;

        public TestCoordinator(IDatabaseContextFactory databaseContextFactory, ITestQuery testQuery,
            IFormulaQuery formulaQuery)
        {
            Guard.AgainstNull(databaseContextFactory, "databaseContextFactory");
            Guard.AgainstNull(testQuery, "testQuery");
            Guard.AgainstNull(formulaQuery, "formulaQuery");

            _databaseContextFactory = databaseContextFactory;
            _formulaQuery = formulaQuery;
            _testQuery = testQuery;
        }

        public void HandleMessage(ResourceMenuRequestMessage message)
        {
            if (!message.Item.ResourceKey.Equals(ResourceKeys.Test))
            {
                return;
            }

            switch (message.Item.Type)
            {
                case Resource.ResourceType.Container:
                {
                    message.NavigationItems.Add(_register);

                    break;
                }
                case Resource.ResourceType.Item:
                {
                    message.NavigationItems.Add(_rename.AssignMessage(new RenameFormulaMessage(message.Item.Key)));

                    message.NavigationItems.Add(_remove.AssignMessage(
                        new RemoveFormulaMessage(message.Item.Text, message.Item.Key, message.UpstreamItems[0])));

                    break;
                }
            }
        }

        public void HandleMessage(RegisterTestMessage message)
        {
            using (_databaseContextFactory.Create())
            {
                var model = new TestModel
                {
                    Formulas = _formulaQuery.All().Map(row => FormulaColumns.Name.MapFrom(row))
                };

                var item = WorkItemManager
                    .Create("New test")
                    .ControlledBy<ITestController>()
                    .ShowIn<IContextToolbarPresenter>()
                    .AddPresenter<ITestPresenter>()
                    .WithModel(model)
                    .AddNavigationItem(_register)
                    .AsDefault()
                    .AssignInitiator(message);

                HostInWorkspace<ITabbedWorkspacePresenter>(item);
            }
        }

        public void HandleMessage(TestRunMessage message)
        {
            throw new NotImplementedException();
            //var workItem = WorkItemManager.Get(message.WorkItemId);

            //if (workItem == null)
            //{
            //    return;
            //}

            //var list = workItem.GetView<ISimpleListView>();

            //if (!list.Contains(message.Event.MethodTestId))
            //{
            //    return;
            //}

            //var view = workItem.GetView<ITestResultView>();

            //view.AddRun(
            //    message.Event.MethodTestId,
            //    message.Event.MethodTestDescription,
            //    message.Event.ExpectedResult);

            //view.ShowView();
        }

        public void HandleMessage(SummaryViewRequestedMessage message)
        {
            if (SummaryViewManager.CanIgnore(message, ResourceKeys.Test))
            {
                return;
            }

            using (_databaseContextFactory.Create())
            {
                switch (message.Item.Type)
                {
                    case Resource.ResourceType.Container:
                    {
                        message.AddTable("Tests", _testQuery.All());

                        break;
                    }
                    case Resource.ResourceType.Item:
                    {
                        break;
                    }
                }
            }
        }

        public void HandleMessage(ExplorerInitializeMessage message)
        {
            if (!Permissions.Test.IsSatisfiedBy(Session.Permissions))
            {
                return;
            }

            message.Items.Add(
                new Resource(ResourceKeys.Test, Guid.NewGuid(), "Tests", ImageResources.Test)
                    .AsContainer());
        }

        public void HandleMessage(PopulateResourceMessage message)
        {
            if (!message.Resource.ResourceKey.Equals(ResourceKeys.Test))
            {
                return;
            }

            switch (message.Resource.Type)
            {
                case Resource.ResourceType.Container:
                {
                    using (_databaseContextFactory.Create())
                    {
                        foreach (var row in _testQuery.All())
                        {
                            message.Resources.Add(
                                new Resource(ResourceKeys.Test, TestColumns.Id.MapFrom(row),
                                    TestColumns.Name.MapFrom(row), ImageResources.Test));
                        }
                    }

                    break;
                }
                case Resource.ResourceType.Item:
                {
                    message.Resources.Add(
                        new Resource(ResourceKeys.TestArgument, Guid.NewGuid(), "Argument values",
                            ImageResources.ArgumentValue).AsContainer());

                    break;
                }
            }
        }
    }
}