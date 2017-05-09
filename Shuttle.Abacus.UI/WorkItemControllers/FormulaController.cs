using System;
using Shuttle.Abacus.Infrastructure;
using Shuttle.Abacus.Messages.v1;
using Shuttle.Abacus.Messages.v1.TransferObjects;
using Shuttle.Abacus.UI.Core.Messaging;
using Shuttle.Abacus.UI.Core.WorkItem;
using Shuttle.Abacus.UI.Messages.Core;
using Shuttle.Abacus.UI.Messages.Formula;
using Shuttle.Abacus.UI.Messages.Resources;
using Shuttle.Abacus.UI.Messages.WorkItem;
using Shuttle.Abacus.UI.UI.Constraint;
using Shuttle.Abacus.UI.UI.Formula;
using Shuttle.Abacus.UI.UI.List;
using Shuttle.Abacus.UI.WorkItemControllers.Interfaces;
using Shuttle.Esb;

namespace Shuttle.Abacus.UI.WorkItemControllers
{
    public class FormulaController : WorkItemController, IFormulaController
    {
        public FormulaController(IServiceBus serviceBus, IMessageBus messageBus, ICallbackRepository callbackRepository)
            : base(serviceBus, messageBus, callbackRepository)
        {
        }

        public void HandleMessage(NewFormulaMessage message)
        {
            if (!WorkItem.PresentationValid())
            {
                return;
            }

            var formulaView = WorkItem.GetView<IFormulaView>();

            Send(new CreateFormulaCommand
            {
                Name = formulaView.NameValue,
                MaximumFormulaName = formulaView.MaximumFormulaNameValue,
                MinimumFormulaName = formulaView.MinimumFormulaNameValue,
                ExecutionType = formulaView.ExecutionTypeValue
            });
        }

        public void HandleMessage(EditFormulaMessage message)
        {
            if (!WorkItem.PresentationValid())
            {
                return;
            }

            var formulaView = WorkItem.GetView<IFormulaView>();
            var constraintView = WorkItem.GetView<IConstraintView>();

            Send(new ChangeFormulaCommand
            {
                FormulaId = message.FormulaId,

            }, () =>
                MessageBus.Publish(new RefreshWorkItemDispatcherTextMessage(WorkItem.Initiator.WorkItemInitiatorId)));
        }

        public void HandleMessage(MoveUpMessage message)
        {
            var view = WorkItem.GetView<ISimpleListView>();

            view.MoveUp(view.SelectedItem());
        }

        public void HandleMessage(MoveDownMessage message)
        {
            var view = WorkItem.GetView<ISimpleListView>();

            view.MoveDown(view.SelectedItem());
        }

        public void HandleMessage(DeleteFormulaMessage message)
        {
            Send(new DeleteFormulaCommand
                {
                    FormulaId = message.FormulaId
                },
                () => MessageBus.Publish(new ResourceRefreshItemMessage(message.OwnerResource)));
        }
    }
}