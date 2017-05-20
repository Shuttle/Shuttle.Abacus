using System;
using Shuttle.Abacus.DataAccess;
using Shuttle.Abacus.Infrastructure;
using Shuttle.Abacus.Localisation;
using Shuttle.Abacus.Shell.Core.Presentation;
using Shuttle.Abacus.Shell.Core.Resources;
using Shuttle.Abacus.Shell.Messages.Core;
using Shuttle.Abacus.Shell.Messages.Resources;
using Shuttle.Abacus.Shell.Messages.Test;
using Shuttle.Abacus.Shell.Models;
using Shuttle.Abacus.Shell.Navigation;
using Shuttle.Abacus.Shell.UI.Shell.TabbedWorkspace;
using Shuttle.Abacus.Shell.UI.Test;
using Shuttle.Abacus.Shell.UI.Test.TestArgument;
using Shuttle.Abacus.Shell.UI.WorkItem.ContextToolbar;
using Shuttle.Core.Data;
using Shuttle.Core.Infrastructure;

namespace Shuttle.Abacus.Shell.Coordinators
{
    public class TestArgumentCoordinator : Coordinator, ITestArgumentCoordinator
    {
        private readonly IArgumentQuery _argumentQuery;
        private readonly IDatabaseContextFactory _databaseContextFactory;

        private readonly INavigationItem _register = new NavigationItem(new ResourceItem("Register", "ArgumentValue"));
        private readonly INavigationItem _remove = new NavigationItem(new ResourceItem("Remove", "ArgumentValue"));

        private readonly ITestQuery _testQuery;

        public TestArgumentCoordinator(IDatabaseContextFactory databaseContextFactory, ITestQuery testQuery,
            IArgumentQuery argumentQuery)
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
            if (!message.Item.ResourceKey.Equals(ResourceKeys.TestArgument))
            {
                return;
            }

            switch (message.Item.Type)
            {
                case Resource.ResourceType.Container:
                {
                    message.NavigationItems.Add(_register.AssignMessage(
                        new RegisterTestArgumentMessage(message.RelatedItems[ResourceKeys.Test].Key)));

                    break;
                }
                case Resource.ResourceType.Item:
                {
                    message.NavigationItems.Add(_remove.AssignMessage(
                        new RemoveTestArgumentMessage(message.RelatedItems[ResourceKeys.Test].Key, message.Item.Text)));

                    break;
                }
            }
        }

        public void HandleMessage(SummaryViewRequestedMessage message)
        {
            if (SummaryViewManager.CanIgnore(message, ResourceKeys.TestArgument))
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

        public void HandleMessage(PopulateResourceMessage message)
        {
            if (!message.Resource.ResourceKey.Equals(ResourceKeys.TestArgument))
            {
                return;
            }

            switch (message.Resource.Type)
            {
                case Resource.ResourceType.Container:
                {
                    using (_databaseContextFactory.Create())
                    {
                        foreach (var row in _testQuery.ArgumentValues(message.RelatedResources[ResourceKeys.Test].Key))
                        {
                            message.Resources.Add(
                                new Resource(ResourceKeys.TestArgument, Guid.Empty,
                                    TestColumns.ArgumentColumns.ArgumentName.MapFrom(row),
                                    ImageResources.ArgumentValue).AsLeaf());
                        }
                    }

                    break;
                }
            }
        }

        public void HandleMessage(RegisterTestArgumentMessage message)
        {
            using (_databaseContextFactory.Create())
            {
                var model = new TestArgumentModel
                {
                    Arguments = _argumentQuery.All().Map(row => new ArgumentModel(row))
                };

                var item = WorkItemManager
                    .Create("New test argument value")
                    .ControlledBy<ITestController>()
                    .ShowIn<IContextToolbarPresenter>()
                    .AddPresenter<ITestArgumentPresenter>().WithModel(model)
                    .AddNavigationItem(_register).AsDefault()
                    .AssignInitiator(message);

                HostInWorkspace<ITabbedWorkspacePresenter>(item);
            }
        }
    }
}