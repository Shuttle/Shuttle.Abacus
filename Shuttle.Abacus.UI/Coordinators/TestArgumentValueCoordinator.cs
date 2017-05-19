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
using Shuttle.Abacus.Shell.UI.SimpleList;
using Shuttle.Abacus.Shell.UI.Test;
using Shuttle.Abacus.Shell.UI.TestArgument;
using Shuttle.Abacus.Shell.UI.WorkItem.ContextToolbar;
using Shuttle.Core.Data;
using Shuttle.Core.Infrastructure;

namespace Shuttle.Abacus.Shell.Coordinators
{
    public class TestArgumentValueCoordinator : Coordinator, ITestArgumentValueCoordinator
    {
        private readonly INavigationItem _register =
            new NavigationItem(new ResourceItem("Register", "TestArgument")).AssignMessage(new RegisterTestArgumentValueMessage());

        private readonly INavigationItem _remove = new NavigationItem(new ResourceItem("Remove", "TestArgument"));
        private readonly INavigationItem _rename = new NavigationItem(new ResourceItem("Rename", "TestArgument"));

        private readonly IArgumentQuery _argumentQuery;
        private readonly IDatabaseContextFactory _databaseContextFactory;
        private readonly ITestQuery _testQuery;

        public TestArgumentValueCoordinator(IDatabaseContextFactory databaseContextFactory, ITestQuery testQuery, IArgumentQuery argumentQuery)
        {
            Guard.AgainstNull(databaseContextFactory, "databaseContextFactory");
            Guard.AgainstNull(testQuery, "testQuery");
            Guard.AgainstNull(argumentQuery, "formulaQuery");

            _databaseContextFactory = databaseContextFactory;
            _argumentQuery = argumentQuery;
            _testQuery = testQuery;
        }

        public void HandleMessage(ResourceMenuRequestMessage message)
        {
            if (!message.Item.ResourceKey.Equals(ResourceKeys.TestArgumentValue))
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
            //                new ManageTestArgumentsMessage(message.Item.Key, message.Item.Text)));

            //        break;
            //    }
            //    case Resource.ResourceType.Item:
            //    {
            //        break;
            //    }
            //}
        }

        public void HandleMessage(RegisterTestArgumentValueMessage message)
        {
            using (_databaseContextFactory.Create())
            {
                var model = new TestArgumentValueModel
                {
                    Arguments = _argumentQuery.All().Map(row => new ArgumentModel(row))
                };

                var item = WorkItemManager
                    .Create("New test")
                    .ControlledBy<ITestController>()
                    .ShowIn<IContextToolbarPresenter>()
                    .AddPresenter<ITestArgumentValuePresenter>().WithModel(model)
                    .AddNavigationItem(_register).AsDefault()
                    .AssignInitiator(message);

                HostInWorkspace<ITabbedWorkspacePresenter>(item);
            }
        }
        public void HandleMessage(SummaryViewRequestedMessage message)
        {
            if (SummaryViewManager.CanIgnore(message, ResourceKeys.TestArgumentValue))
            {
                return;
            }

            using (_databaseContextFactory.Create())
            {
                switch (message.Item.Type)
                {
                    case Resource.ResourceType.Container:
                    {
                        message.AddTable("TestArguments", _testQuery.All());

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

                //modelPresenter.AssignModel(new SimpleListModel("TestArgumentId", _testQuery.FetchForMethodId(methodId)));
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
                new Resource(ResourceKeys.TestArgumentValue, Guid.NewGuid(), "Argument Values", ImageResources.TestArgumentValue)
                    .AsContainer());
        }

        public void HandleMessage(PopulateResourceMessage message)
        {
            if (!message.Resource.ResourceKey.Equals(ResourceKeys.TestArgumentValue))
            {
                return;
            }

            switch (message.Resource.Type)
            {
                case Resource.ResourceType.Container:
                {
                    using (_databaseContextFactory.Create())
                    {
                        foreach (var row in _argumentQuery.All())
                        {
                            message.Resources.Add(
                                new Resource(ResourceKeys.TestArgumentValue, TestColumns.Id.MapFrom(row),
                                    TestColumns.ArgumentValueColumns.ArgumentName.MapFrom(row), ImageResources.TestArgumentValue));
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