using System;
using System.Collections.Generic;
using Shuttle.Abacus.DTO;
using Shuttle.Abacus.Infrastructure;
using Shuttle.Abacus.Messages.v1;
using Shuttle.Abacus.Messages.v1.TransferObjects;
using Shuttle.Abacus.UI.Core.Messaging;
using Shuttle.Abacus.UI.Core.WorkItem;
using Shuttle.Abacus.UI.Messages.Calculation;
using Shuttle.Abacus.UI.Messages.Core;
using Shuttle.Abacus.UI.Messages.Resources;
using Shuttle.Abacus.UI.Messages.WorkItem;
using Shuttle.Abacus.UI.UI.Calculation;
using Shuttle.Abacus.UI.UI.Calculation.GraphNodeArgument;
using Shuttle.Abacus.UI.UI.Constraint;
using Shuttle.Abacus.UI.UI.List;
using Shuttle.Abacus.UI.WorkItemControllers.Interfaces;
using Shuttle.Esb;

namespace Shuttle.Abacus.UI.WorkItemControllers
{
    public class CalculationController : WorkItemController, ICalculationController
    {
        public CalculationController(IServiceBus serviceBus, IMessageBus messageBus,
            ICallbackRepository callbackRepository)
            : base(serviceBus, messageBus, callbackRepository)
        {
        }

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
                GraphNodeArguments = new List<GraphNodeDataRow>(graphNodeArgumentView.GraphNodeArguments)
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
                MethodId = message.MethodId
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
                Constraints = constraintView.Constraints.Map(item => new Constraint
                {
                    Name = item.Name,
                    Answer = item.Answer,
                    ArgumentId = item.ArgumentId
                })
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
                GraphNodeArguments = new List<GraphNodeDataRow>(graphNodeArgumentView.GraphNodeArguments)
            };

            Send(command,
                () =>
                    MessageBus.Publish(
                        new RefreshWorkItemDispatcherTextMessage(WorkItem.Initiator.WorkItemInitiatorId)));
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