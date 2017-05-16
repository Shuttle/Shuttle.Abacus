using System;
using System.Data;
using Shuttle.Abacus.DataAccess;
using Shuttle.Abacus.Infrastructure;
using Shuttle.Abacus.Localisation;
using Shuttle.Abacus.UI.Coordinators.Interfaces;
using Shuttle.Abacus.UI.Core.Presentation;
using Shuttle.Abacus.UI.Core.Resources;
using Shuttle.Abacus.UI.Messages.Core;
using Shuttle.Abacus.UI.Messages.Explorer;
using Shuttle.Abacus.UI.Messages.Formula;
using Shuttle.Abacus.UI.Messages.Resources;
using Shuttle.Abacus.UI.Messages.Test;
using Shuttle.Abacus.UI.Models;
using Shuttle.Abacus.UI.Navigation;
using Shuttle.Abacus.UI.UI.Shell.TabbedWorkspace;
using Shuttle.Abacus.UI.UI.SimpleList;
using Shuttle.Abacus.UI.UI.Test;
using Shuttle.Abacus.UI.UI.Test.Results;
using Shuttle.Abacus.UI.UI.WorkItem.ContextToolbar;
using Shuttle.Core.Data;
using Shuttle.Core.Infrastructure;

namespace Shuttle.Abacus.UI.Coordinators
{
    public class TestCoordinator :
        Coordinator,
        ITestCoordinator
    {
        private readonly INavigationItem _register =
            new NavigationItem(new ResourceItem("Register", "Test")).AssignMessage(new RegisterTestMessage());

        private readonly INavigationItem _remove = new NavigationItem(new ResourceItem("Remove", "Test"));
        private readonly INavigationItem _rename = new NavigationItem(new ResourceItem("Rename", "Test"));

        private readonly IArgumentQuery _argumentQuery;
        private readonly IDatabaseContextFactory _databaseContextFactory;
        private readonly ITestQuery _testQuery;

        public TestCoordinator(IDatabaseContextFactory databaseContextFactory, ITestQuery testQuery,
            IArgumentQuery argumentQuery)
        {
            Guard.AgainstNull(databaseContextFactory, "databaseContextFactory");
            Guard.AgainstNull(testQuery, "testQuery");
            Guard.AgainstNull(argumentQuery, "argumentQuery");

            _databaseContextFactory = databaseContextFactory;
            _argumentQuery = argumentQuery;
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
                    .AddPresenter<ITestResultPresenter>()
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
            var item = WorkItemManager
                .Create("New test case")
                .ControlledBy<ITestController>()
                .ShowIn<IContextToolbarPresenter>()
                .AddPresenter<ITestPresenter>()
                .WithModel(BuildModel())
                .AddNavigationItem(NavigationItemFactory.Create(message).WithResourceItem(ResourceItems.Submit))
                .AsDefault()
                .AssignInitiator(message);

            HostInWorkspace<ITabbedWorkspacePresenter>(item);
        }

        public void HandleMessage(EditTestMessage message)
        {
            var model = BuildModel();

            DataRow row;

            using (_databaseContextFactory.Create())
            {
                row = _testQuery.Get(message.TestId);
            }

            //model.MethodTestRow = row;
            //model.ArgumentValues = _testQuery.ArgumentValues(message.TestId).CopyToDataTable();

            message.FormulaId = new Guid(row["FormulaId"].ToString());
            message.Description = TestColumns.Name.MapFrom(row);
            message.ExpectedResult = TestColumns.ExpectedResult.MapFrom(row);

            var item = WorkItemManager
                .Create("Test: " + TestColumns.Name.MapFrom(row))
                .ControlledBy<ITestController>()
                .ShowIn<IContextToolbarPresenter>()
                .AddPresenter<ITestPresenter>()
                .WithModel(model)
                .AddNavigationItem(NavigationItemFactory.Create(new ChangeTestMessage(message)))
                .AsDefault()
                .AssignInitiator(message);

            HostInWorkspace<ITabbedWorkspacePresenter>(item);
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
            var workItem = WorkItemManager.Get(message.WorkItemId);

            if (workItem == null)
            {
                return;
            }

            var list = workItem.GetView<ISimpleListView>();

            if (!list.Contains(message.Event.MethodTestId))
            {
                return;
            }

            var view = workItem.GetView<ITestResultView>();

            view.AddRun(
                message.Event.MethodTestId,
                message.Event.MethodTestDescription,
                message.Event.ExpectedResult);

            view.ShowView();
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
                        message.AddTable("Test Cases", _testQuery.All());

                        break;
                    }
                    case Resource.ResourceType.Item:
                    {
                        break;
                    }
                }
            }
        }

        private TestModel BuildModel()
        {
            throw new NotImplementedException();

            using (_databaseContextFactory.Create())
            {
                return new TestModel
                {
                    //ArgumentRows = _argumentQuery.All()
                };
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
    }
}