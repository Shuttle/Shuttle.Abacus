using System;
using Shuttle.Abacus.DataAccess;
using Shuttle.Abacus.Infrastructure;
using Shuttle.Abacus.Localisation;
using Shuttle.Abacus.UI.Coordinators.Interfaces;
using Shuttle.Abacus.UI.Core.Presentation;
using Shuttle.Abacus.UI.Core.Resources;
using Shuttle.Abacus.UI.Core.State;
using Shuttle.Abacus.UI.Messages.Core;
using Shuttle.Abacus.UI.Messages.Explorer;
using Shuttle.Abacus.UI.Messages.FactorAnswer;
using Shuttle.Abacus.UI.Messages.Resources;
using Shuttle.Abacus.UI.Models;
using Shuttle.Abacus.UI.UI.Argument;
using Shuttle.Abacus.UI.UI.Argument.RestrictedAnswer;
using Shuttle.Abacus.UI.UI.Shell.TabbedWorkspace;
using Shuttle.Abacus.UI.UI.WorkItem.ContextToolbar;
using Shuttle.Abacus.UI.WorkItemControllers.Interfaces;
using Shuttle.Core.Data;
using Shuttle.Core.Infrastructure;

namespace Shuttle.Abacus.UI.Coordinators
{
    public class ArgumentCoordinator : Coordinator, IArgumentCoordinator
    {
        private readonly IAnswerTypeQuery _answerTypeQuery;
        private readonly IArgumentQuery _argumentQuery;
        private readonly IDatabaseContextFactory _databaseContextFactory;

        public ArgumentCoordinator(IDatabaseContextFactory databaseContextFactory, IArgumentQuery argumentQuery,
            IAnswerTypeQuery answerTypeQuery)
        {
            Guard.AgainstNull(databaseContextFactory, "databaseContextFactory");
            Guard.AgainstNull(argumentQuery, "argumentQuery");
            Guard.AgainstNull(answerTypeQuery, "answerTypeQuery");

            _databaseContextFactory = databaseContextFactory;
            _argumentQuery = argumentQuery;
            _answerTypeQuery = answerTypeQuery;
        }

        public void HandleMessage(ExplorerInitializeMessage message)
        {
            if (!Permissions.Argument.IsSatisfiedBy(Session.Permissions))
            {
                return;
            }

            message.Items.Add(
                new Resource(ResourceKeys.Argument, Guid.NewGuid(), "Arguments", ImageResources.Argument).AsContainer());
        }

        public void HandleMessage(ResourceMenuRequestMessage message)
        {
            if (message.Item.ResourceKey.Equals(ResourceKeys.Argument))
            {
                return;
            }

            switch (message.Item.Type)
            {
                case Resource.ResourceType.Container:
                {
                    message.NavigationItems.Add(NavigationItemFactory.Create<NewArgumentMessage>());

                    break;
                }
                case Resource.ResourceType.Item:
                {
                    if (!message.Item.State.Get<bool>(StateKeys.IsSystemData))
                    {
                        message.NavigationItems.Add(
                            NavigationItemFactory.Create(new EditArgumentMessage(message.Item.Key)));

                        message.NavigationItems.Add(
                            NavigationItemFactory.Create(new DeleteArgumentMessage(message.Item.Key, message.Item.Text,
                                message.UpstreamItems[0])));
                    }

                    break;
                }
            }
        }

        public void HandleMessage(NewArgumentMessage message)
        {
            var item = WorkItemManager
                .Create("New Argument")
                .ControlledBy<IArgumentController>()
                .ShowIn<IContextToolbarPresenter>()
                .AddPresenter<IArgumentPresenter>().WithModel(BuildArgumentModel())
                .AddPresenter<IArgumentRestrictedAnswerPresenter>()
                .AddNavigationItem(NavigationItemFactory.Create(message).AssignResourceItem(ResourceItems.Submit)).
                AsDefault()
                .AssignInitiator(message);

            HostInWorkspace<ITabbedWorkspacePresenter>(item);
        }

        public void HandleMessage(PopulateResourceMessage message)
        {
            if (message.Resource.ResourceKey.Equals(ResourceKeys.Argument))
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
                            message.Resources.Add(new Resource(ResourceKeys.Argument,
                                ArgumentColumns.Id.MapFrom(row),
                                ArgumentColumns.Name.MapFrom(row),
                                ImageResources.Argument).AsLeaf().State.Add(
                                StateKeys.IsSystemData, ArgumentColumns.IsSystemData.MapFrom(row)));
                        }
                    }

                    break;
                }
            }
        }

        public void HandleMessage(EditArgumentMessage message)
        {
            var model = BuildArgumentModel();

            using (_databaseContextFactory.Create())
            {
                model.ArgumentRow = _argumentQuery.Get(message.ArgumentId);
            }

            var item = WorkItemManager
                .Create("Edit Argument")
                .ControlledBy<IArgumentController>()
                .ShowIn<IContextToolbarPresenter>()
                .AddPresenter<IArgumentPresenter>().WithModel(model)
                .AddPresenter<IArgumentRestrictedAnswerPresenter>()
                .WithModel(_argumentQuery.GetAnswerCatalog(message.ArgumentId))
                .AddNavigationItem(NavigationItemFactory.Create(message).AssignResourceItem(ResourceItems.Submit)).
                AsDefault()
                .AssignInitiator(message);

            HostInWorkspace<ITabbedWorkspacePresenter>(item);
        }

        public void HandleMessage(ResourceRefreshItemTextMessage message)
        {
            if (message.Item.ResourceKey.Equals(ResourceKeys.Argument) ||
                message.Item.Type != Resource.ResourceType.Item)
            {
                return;
            }

            message.Item.AssignText(ArgumentColumns.Name.MapFrom(_argumentQuery.Get(message.Item.Key)));
        }

        public void HandleMessage(DeleteArgumentMessage message)
        {
            if (!UIService.Confirm(string.Format("Are you sure that you want to delete argument '{0}'?", message.Text)))
            {
                return;
            }

            WorkItemControllerFactory.Create<IArgumentController>().HandleMessage(message);
        }

        public void HandleMessage(SummaryViewRequestedMessage message)
        {
            if (SummaryViewManager.CanIgnore(message, ResourceKeys.Argument))
            {
                return;
            }

            using (_databaseContextFactory.Create())
            {
                switch (message.Item.Type)
                {
                    case Resource.ResourceType.Container:
                    {
                        message.AddTable("Arguments", _argumentQuery.All());

                        break;
                    }
                    case Resource.ResourceType.Item:
                    {
                        message.AddRow(message.Item.Text, _argumentQuery.Get(message.Item.Key));

                        message.AddTable("Answer Catalog", _argumentQuery.GetAnswerCatalog(message.Item.Key));

                        break;
                    }
                }
            }
        }

        private ArgumentModel BuildArgumentModel()
        {
            using (_databaseContextFactory.Create())
            {
                return new ArgumentModel
                {
                    AnswerTypes = _answerTypeQuery.All()
                };
            }
        }

        public static class StateKeys
        {
            public static readonly StateKey IsSystemData = new StateKey("IsSystemData");
        }
    }
}