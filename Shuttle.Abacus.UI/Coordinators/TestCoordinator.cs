using System;
using Shuttle.Abacus.DataAccess;
using Shuttle.Abacus.Infrastructure;
using Shuttle.Abacus.Localisation;
using Shuttle.Abacus.Shell.Coordinators.Interfaces;
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
using Shuttle.Abacus.Shell.UI.SimpleList;
using Shuttle.Abacus.Shell.UI.Test;
using Shuttle.Abacus.Shell.UI.WorkItem.ContextToolbar;
using Shuttle.Core.Data;
using Shuttle.Core.Infrastructure;

namespace Shuttle.Abacus.Shell.Coordinators
{
    public class TestCoordinator : Coordinator, ITestCoordinator
    {
        private readonly INavigationItem _register =
            new NavigationItem(new ResourceItem("Register", "Test")).AssignMessage(new RegisterTestMessage());

        private readonly INavigationItem _remove = new NavigationItem(new ResourceItem("Remove", "Test"));
        private readonly INavigationItem _rename = new NavigationItem(new ResourceItem("Rename", "Test"));

        private readonly IFormulaQuery _formulaQuery;
        private readonly IDatabaseContextFactory _databaseContextFactory;
        private readonly ITestQuery _testQuery;

        public TestCoordinator(IDatabaseContextFactory databaseContextFactory, ITestQuery testQuery, IFormulaQuery formulaQuery)
        {
            Guard.AgainstNull(databaseContextFactory, "databaseContextFactory");
            Guard.AgainstNull(testQuery, "testQuery");
            Guard.AgainstNull(formulaQuery, "formulaQuery");

            _databaseContextFactory = databaseContextFactory;
            _formulaQuery = formulaQuery;
            _testQuery = testQuery;
        }

        public void HandleMessage(ManageTestsMessage message)
        {
            using (_databaseContextFactory.Create())
            {
                var presenter = WorkItemManager.BuildPresenter<ISimpleListPresenter>()
                    .AddNavigationItem(NavigationItemFactory.Create<PrintTestMessage>())
                    .AddNavigationItem(NavigationItemFactory.Create<RunTestMessage>())
                    //.AddNavigationItem(NavigationItemFactory.Create(new RemoveTestMessage(message.)))
                    .AddNavigationItem(NavigationItemFactory.Create<EditTestMessage>())
                    .AddNavigationItem(NavigationItemFactory.Create(new RegisterTestMessage()))
                    .AddNavigationItem(NavigationItemFactory.Create<MarkAllMessage>())
                    .AddNavigationItem(NavigationItemFactory.Create<InvertMarksMessage>());

                var item = WorkItemManager
                    .Create($"Tests: {message.MethodName}")
                    .ControlledBy<ITestManagerController>()
                    .ShowIn<IContextToolbarPresenter>()
                    .AddPresenter(presenter)
                    //.WithModel(new SimpleListModel("TestId", _testQuery.FetchForMethodId(message.TestId)))
                    //.AddPresenter<ITestResultPresenter>()
                    .AssignInitiator(message);

                HostInWorkspace<ITabbedWorkspacePresenter>(item);
            }
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

            //switch (message.Item.Type)
            //{
            //    case Resource.ResourceType.Container:
            //    {
            //        message.NavigationItems.Add(
            //            NavigationItemFactory.Create(
            //                new ManageTestsMessage(message.Item.Key, message.Item.Text)));

            //        break;
            //    }
            //    case Resource.ResourceType.Item:
            //    {
            //        break;
            //    }
            //}
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
                    .AddPresenter<ITestPresenter>().WithModel(model)
                    .AddNavigationItem(_register).AsDefault()
                    .AssignInitiator(message);

                HostInWorkspace<ITabbedWorkspacePresenter>(item);
            }
        }

        public void HandleMessage(EditTestMessage message)
        {
            throw new NotImplementedException();

            //using (_databaseContextFactory.Create())
            //{
            //    message.FormulaId = new Guid(row["FormulaId"].ToString());
            //    message.Description = TestColumns.Name.MapFrom(row);
            //    message.ExpectedResult = TestColumns.ExpectedResult.MapFrom(row);

            //    var item = WorkItemManager
            //        .Create("Test: " + TestColumns.Name.MapFrom(row))
            //        .ControlledBy<ITestController>()
            //        .ShowIn<IContextToolbarPresenter>()
            //        .AddPresenter<ITestPresenter>()
            //        .WithModel(model)
            //        .AddNavigationItem(NavigationItemFactory.Create(new ChangeTestMessage(message)))
            //        .AsDefault()
            //        .AssignInitiator(message);

            //    HostInWorkspace<ITabbedWorkspacePresenter>(item);
            //}
        }

        public void HandleMessage(TestCreatedMessage message)
        {
            RefreshList(message.WorkItemId, message.MethodId);
        }

        public void HandleMessage(TestChangedMessage message)
        {
            RefreshList(message.WorkItemId, message.MethodId);
        }

        public void HandleMessage(TestRemovedMessage message)
        {
            RefreshList(message.WorkItemId, message.MethodId);
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

        private void RefreshList(Guid workItemId, Guid methodId)
        {
            var workItem = WorkItemManager.Get(workItemId);

            if (workItem == null)
            {
                return;
            }

            var presenter = workItem.GetPresenter<ISimpleListPresenter>();

            using (_databaseContextFactory.Create())
            {
                var modelPresenter = presenter as IPresenter<SimpleListModel>;

                if (modelPresenter == null)
                {
                    throw new InvalidOperationException();
                }

                throw new NotImplementedException();

                //modelPresenter.AssignModel(new SimpleListModel("TestId", _testQuery.FetchForMethodId(methodId)));
            }

            presenter.Refresh();
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
                        foreach (var row in _formulaQuery.All())
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
                        new Resource(ResourceKeys.TestArgumentValue, Guid.NewGuid(), "Argument values",
                            ImageResources.TestArgumentValue).AsContainer());

                    break;
                }
            }
        }
    }
}