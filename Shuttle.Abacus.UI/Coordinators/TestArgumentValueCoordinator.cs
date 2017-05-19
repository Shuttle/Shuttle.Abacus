using System;
using Shuttle.Abacus.DataAccess;
using Shuttle.Abacus.Infrastructure;
using Shuttle.Abacus.Localisation;
using Shuttle.Abacus.Shell.Core.Presentation;
using Shuttle.Abacus.Shell.Core.Resources;
using Shuttle.Abacus.Shell.Messages.Core;
using Shuttle.Abacus.Shell.Messages.Formula;
using Shuttle.Abacus.Shell.Messages.Resources;
using Shuttle.Abacus.Shell.Messages.Test;
using Shuttle.Abacus.Shell.Models;
using Shuttle.Abacus.Shell.Navigation;
using Shuttle.Abacus.Shell.UI.Shell.TabbedWorkspace;
using Shuttle.Abacus.Shell.UI.Test;
using Shuttle.Abacus.Shell.UI.TestArgument;
using Shuttle.Abacus.Shell.UI.WorkItem.ContextToolbar;
using Shuttle.Core.Data;
using Shuttle.Core.Infrastructure;

namespace Shuttle.Abacus.Shell.Coordinators
{
    public class TestArgumentValueCoordinator : Coordinator, ITestArgumentValueCoordinator
    {
        private readonly IArgumentQuery _argumentQuery;
        private readonly IDatabaseContextFactory _databaseContextFactory;

        private readonly INavigationItem _register = new NavigationItem(new ResourceItem("Register", "ArgumentValue"));
        private readonly INavigationItem _remove = new NavigationItem(new ResourceItem("Remove", "ArgumentValue"));
        private readonly INavigationItem _rename = new NavigationItem(new ResourceItem("Rename", "ArgumentValue"));

        private readonly ITestQuery _testQuery;

        public TestArgumentValueCoordinator(IDatabaseContextFactory databaseContextFactory, ITestQuery testQuery,
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
            if (!message.Item.ResourceKey.Equals(ResourceKeys.TestArgumentValue))
            {
                return;
            }

            switch (message.Item.Type)
            {
                case Resource.ResourceType.Container:
                {
                    message.NavigationItems.Add(_register.AssignMessage(
                        new RegisterTestArgumentValueMessage(message.RelatedItems[ResourceKeys.Test].Key)));

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
                        foreach (var row in _testQuery.ArgumentValues(message.RelatedResources[ResourceKeys.Test].Key))
                        {
                            message.Resources.Add(
                                new Resource(ResourceKeys.TestArgumentValue, Guid.Empty,
                                    TestColumns.ArgumentValueColumns.ArgumentName.MapFrom(row),
                                    ImageResources.ArgumentValue));
                        }
                    }

                    break;
                }
            }
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
                    .Create("New test argument value")
                    .ControlledBy<ITestController>()
                    .ShowIn<IContextToolbarPresenter>()
                    .AddPresenter<ITestArgumentValuePresenter>().WithModel(model)
                    .AddNavigationItem(_register).AsDefault()
                    .AssignInitiator(message);

                HostInWorkspace<ITabbedWorkspacePresenter>(item);
            }
        }
    }
}