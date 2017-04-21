﻿using System;
using Abacus.Data;
using Abacus.Localisation;

namespace Abacus.UI
{
    public class MethodTestCoordinator :
        Coordinator,
        IMethodTestManagerCoordinator
    {
        private readonly IArgumentQuery argumentQuery;

        private readonly IMethodTestQuery testQuery;

        public MethodTestCoordinator(IMethodTestQuery testQuery, IArgumentQuery argumentQuery)
        {
            this.argumentQuery = argumentQuery;
            this.testQuery = testQuery;
        }

        public void HandleMessage(ManageMethodTestsMessage message)
        {
            var presenter = WorkItemManager.BuildPresenter<ISimpleListPresenter>()
                .AddNavigationItem(NavigationItemFactory.Create<PrintMethodTestMessage>())
                .AddNavigationItem(NavigationItemFactory.Create<RunMethodTestMessage>())
                .AddNavigationItem(NavigationItemFactory.Create(new RemoveMethodTestMessage(message.MethodId)))
                .AddNavigationItem(NavigationItemFactory.Create<EditMethodTestMessage>())
                .AddNavigationItem(NavigationItemFactory.Create(new NewMethodTestMessage(message.MethodId)))
                .AddNavigationItem(NavigationItemFactory.Create(new NewMethodTestFromExistingMessage(message.MethodId)))
                .AddNavigationItem(NavigationItemFactory.Create<MarkAllMessage>())
                .AddNavigationItem(NavigationItemFactory.Create<InvertMarksMessage>());

            var item = WorkItemManager
                .Create(string.Format("Tests: {0}", message.MethodName))
                .ControlledBy<IMethodTestManagerController>()
                .ShowIn<IContextToolbarPresenter>()
                .AddPresenter(presenter).WithModel(new SimpleListModel(testQuery.FetchForMethodId(message.MethodId)))
                .AddPresenter<IMethodTestResultPresenter>()
                .AssignInitiator(message);

            HostInWorkspace<ITabbedWorkspacePresenter>(item);
        }

        public void HandleMessage(ResourceMenuRequestMessage message)
        {
            if (message.Item.ResourceKey != ResourceKeys.MethodTest)
            {
                return;
            }

            switch (message.Item.Type)
            {
                case Resource.ResourceType.Container:
                    {
                        message.NavigationItems.Add(
                            NavigationItemFactory.Create(
                                new ManageMethodTestsMessage(message.RelatedItems[ResourceKeys.Method].Key,
                                                           message.RelatedItems[ResourceKeys.Method].Text)));

                        break;
                    }
                case Resource.ResourceType.Item:
                    {
                        break;
                    }
            }
        }

        public void HandleMessage(NewMethodTestMessage message)
        {
            var item = WorkItemManager
                .Create("New test case")
                .ControlledBy<IMethodTestController>()
                .ShowIn<IContextToolbarPresenter>()
                .AddPresenter<IMethodTestPresenter>().WithModel(BuildModel())
                .AddNavigationItem(NavigationItemFactory.Create(message).AssignResourceItem(ResourceItems.Submit)).
                AsDefault()
                .AssignInitiator(message);

            HostInWorkspace<ITabbedWorkspacePresenter>(item);
        }

        public void HandleMessage(EditMethodTestMessage message)
        {
            var model = BuildModel();

            var testRow = testQuery.Get(message.MethodTestId);

            model.MethodTestRow = testRow.Row;
            model.ArgumentAnswers = testQuery.GetArgumentAnswers(message.MethodTestId).Table;

            message.MethodId = new Guid(testRow.Row["MethodId"].ToString());
            message.Description = MethodTestColumns.Description.MapFrom(testRow.Row);
            message.ExpectedResult = MethodTestColumns.ExpectedResult.MapFrom(testRow.Row);

            var item = WorkItemManager
                .Create("Test: " + MethodTestColumns.Description.MapFrom(testRow.Row))
                .ControlledBy<IMethodTestController>()
                .ShowIn<IContextToolbarPresenter>()
                .AddPresenter<IMethodTestPresenter>().WithModel(model)
                .AddNavigationItem(NavigationItemFactory.Create(new ChangeMethodTestMessage(message))).AsDefault()
                .AssignInitiator(message);

            HostInWorkspace<ITabbedWorkspacePresenter>(item);
        }

        public void HandleMessage(MethodTestCreatedMessage message)
        {
            RefreshList(message.WorkItemId, message.MethodId);
        }

        public void HandleMessage(MethodTestChangedMessage message)
        {
            RefreshList(message.WorkItemId, message.MethodId);
        }

        public void HandleMessage(MethodTestRemovedMessage message)
        {
            RefreshList(message.WorkItemId, message.MethodId);
        }

        public void HandleMessage(MethodTestRunMessage message)
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

            var view = workItem.GetView<IMethodTestResultView>();

            view.AddRun(
                message.Event.MethodTestId,
                message.Event.MethodTestDescription,
                message.Event.ExpectedResult,
                message.Event.MethodContext);

            view.ShowView();
        }

        private MethodTestModel BuildModel()
        {
            return new MethodTestModel
                   {
                       Arguments = argumentQuery.AllDTOs()
                   };
        }

        private void RefreshList(Guid workItemId, Guid methodId)
        {
            var workItem = WorkItemManager.Get(workItemId);

            if (workItem == null)
            {
                return;
            }

            var presenter = workItem.GetPresenter<ISimpleListPresenter>();

            presenter.AssignModel(new SimpleListModel(testQuery.FetchForMethodId(methodId)));

            presenter.Refresh();
        }

        public void HandleMessage(NewMethodTestFromExistingMessage message)
        {
            var model = BuildModel();

            var testRow = testQuery.Get(message.MethodTestId);

            model.MethodTestRow = testRow.Row;
            model.ArgumentAnswers = testQuery.GetArgumentAnswers(message.MethodTestId).Table;

            var item = WorkItemManager
                .Create(string.Format("New test case from '{0}'", MethodTestColumns.Description.MapFrom(testRow.Row)))
                .ControlledBy<IMethodTestController>()
                .ShowIn<IContextToolbarPresenter>()
                .AddPresenter<IMethodTestPresenter>().WithModel(model)
                .AddNavigationItem(NavigationItemFactory.Create(new NewMethodTestMessage(message)).AssignResourceItem(ResourceItems.Submit)).AsDefault()
                .AssignInitiator(message);

            HostInWorkspace<ITabbedWorkspacePresenter>(item);
        }

        public void HandleMessage(SummaryViewRequestedMessage message)
        {
            if (SummaryViewManager.CanIgnore(message, ResourceKeys.MethodTest))
            {
                return;
            }

            switch (message.Item.Type)
            {
                case Resource.ResourceType.Container:
                    {
                        message.AddTable("Test Cases", testQuery.FetchForMethodId(message.RelatedItems[ResourceKeys.Method].Key));

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
