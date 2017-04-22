using System;
using Shuttle.Abacus.UI.Core.Presentation;

namespace Shuttle.Abacus.UI.Core.WorkItem
{
    public class WorkItemBuilder : IWorkItemBuilder, IWorkItemBuilderController
    {
        private readonly IWorkItemManager workItemManager;
        private readonly IWorkItemControllerFactory workItemControllerFactory;
        private readonly IWorkItemPresenterFactory workItemPresenterFactory;

        internal string Text { get; private set; }
        internal Guid WorkItemId { get; private set; }
        private IWorkItemController controller;

        public WorkItemBuilder(string text, IWorkItemManager workItemManager, IWorkItemControllerFactory workItemControllerFactory, IWorkItemPresenterFactory workItemPresenterFactory)
            : this(Guid.NewGuid(), text, workItemManager, workItemControllerFactory, workItemPresenterFactory)
        {
        }

        public WorkItemBuilder(Guid workItemId, string text, IWorkItemManager workItemManager, IWorkItemControllerFactory workItemControllerFactory, IWorkItemPresenterFactory workItemPresenterFactory)
        {
            this.workItemManager = workItemManager;
            this.workItemControllerFactory = workItemControllerFactory;
            this.workItemPresenterFactory = workItemPresenterFactory;

            WorkItemId = workItemId;
            Text = text;
        }

        public IWorkItemBuilder ControlledBy<T>() where T : IWorkItemController
        {
            controller = workItemControllerFactory.Create<T>();

            return this;
        }

        public IWorkItem ShowIn<T>() where T : IWorkItemPresenter
        {
            var result = new WorkItem(WorkItemId, Text, workItemManager, controller, workItemPresenterFactory.Create<T>());

            workItemManager.Add(result);

            return result;
        }
    }
}
