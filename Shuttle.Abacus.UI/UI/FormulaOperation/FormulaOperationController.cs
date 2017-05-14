using System.Collections.Generic;
using Shuttle.Abacus.Messages.v1;
using Shuttle.Abacus.UI.Core.Messaging;
using Shuttle.Abacus.UI.Core.WorkItem;
using Shuttle.Abacus.UI.Messages.Core;
using Shuttle.Abacus.UI.Messages.Formula;
using Shuttle.Abacus.UI.UI.SimpleList;
using Shuttle.Esb;

namespace Shuttle.Abacus.UI.UI.FormulaOperation
{
    public class FormulaOperationController : WorkItemController, IFormulaOperationController
    {
        public FormulaOperationController(IServiceBus serviceBus, IMessageBus messageBus)
            : base(serviceBus, messageBus)
        {
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

        public void HandleMessage(ManageFormulaOperationsMessage message)
        {
            if (!WorkItem.PresentationValid())
            {
                return;
            }

            var view = WorkItem.GetView<IFormulaOperationView>();

            var operations = new List<Abacus.Messages.v1.TransferObjects.FormulaOperation>();

            var sequenceNumber = 1;

            foreach (var model in view.FormulaOperations)
            {
                operations.Add(new Abacus.Messages.v1.TransferObjects.FormulaOperation
                {
                    SequenceNumber = sequenceNumber++,
                    Operation = model.Operation,
                    ValueSelection = model.ValueSelection,
                    ValueSource = model.ValueSource
                });
            }

            Send(new SetFormulaOperationsCommand
            {
                FormulaId = message.FormulaId,
                Operations = operations
            });
        }
    }
}