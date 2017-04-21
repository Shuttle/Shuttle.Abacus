using System;
using System.Collections.Generic;
using Abacus.DTO;
using Abacus.Messages;

namespace Abacus.UI
{
    public class CalculationController : WorkItemController, ICalculationController
    {
        public void HandleMessage(NewCalculationMessage message)
        {
            if (!WorkItem.PresentationValid())
            {
                return;
            }

            var calculationView = WorkItem.GetView<ICalculationView>();
            var graphNodeArgumentView = WorkItem.GetView<IGraphNodeArgumentView>();

            var command = new CreateCalculationCommand
                          {
                              MethodId = message.MethodId,
                              OwnerName = message.OwnerName,
                              OwnerId = message.OwnerId,
                              Type = calculationView.TypeValue,
                              Name = calculationView.CalculationNameValue,
                              Required = calculationView.RequiredValue,
                              GraphNodeArguments = new List<GraphNodeArgumentDTO>(graphNodeArgumentView.GraphNodeArguments)
                          };

            Send(command);
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

        public void HandleMessage(ChangeCalculationOrderMessage message)
        {
            var view = WorkItem.GetView<ISimpleListView>();

            var command = new ChangeCalculationOrderCommand
                          {
                              OwnerId = message.OwnerId,
                              MethodId = message.MethodId,
                          };

            foreach (var item in view.Items)
            {
                command.OrderedIds.Add(new Guid(item.Name));
            }

            Send(command);
        }

        public void HandleMessage(ManageCalculationConstraintsMessage message)
        {
            var constraintView = WorkItem.GetView<IConstraintView>();

            Send(new SetCalculationConstraintsCommand
                 {
                     CalculationId = message.CalculationId,
                     Constraints = constraintView.Constraints
                 });
        }

        public void HandleMessage(EditCalculationMessage message)
        {
            var calculationView = WorkItem.GetView<ICalculationView>();
            var graphNodeArgumentView = WorkItem.GetView<IGraphNodeArgumentView>();

            var command = new ChangeCalculationCommand
                          {
                              MethodId = message.MethodId,
                              CalculationId = message.CalculationId,
                              OwnerId = message.OwnerId,
                              OwnerName = message.OwnerName,
                              Name = calculationView.CalculationNameValue,
                              Required = calculationView.RequiredValue,
                              GraphNodeArguments = new List<GraphNodeArgumentDTO>(graphNodeArgumentView.GraphNodeArguments)
                          };

            Send(command,
                 () =>
                 MessageBus.Publish(new RefreshWorkItemDispatcherTextMessage(WorkItem.Initiator.WorkItemInitiatorId)));
        }

        public void HandleMessage(DeleteCalculationMessage message)
        {
            Send(new DeleteCalculationCommand
                 {
                     MethodId = message.MethodId,
                     CalculationId = message.CalculationId
                 },
                 () => MessageBus.Publish(new ResourceRefreshItemMessage(message.OwnerResource)));
        }

        public void HandleMessage(GrabCalculationsMessage message)
        {
            var view = WorkItem.GetView<ISimpleListView>();

            if (!view.HasCheckedItems)
            {
                return;
            }

            var command = new GrabCalculationsCommand
                          {
                              GrabberCalculationId = message.GrabberCalculationId,
                              MethodId = message.MethodId
                          };

            foreach (var item in view.CheckedItems)
            {
                command.GrabbedCalculationIds.Add(new Guid(item.Name));
            }

            Send(command,
                 () => MessageBus.Publish(new ResourceRefreshItemMessage(message.MethodResource)));
        }
    }
}
