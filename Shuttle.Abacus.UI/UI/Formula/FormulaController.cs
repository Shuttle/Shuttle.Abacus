using System.Collections.Generic;
using Shuttle.Abacus.Messages.v1;
using Shuttle.Abacus.UI.Core.Messaging;
using Shuttle.Abacus.UI.Core.WorkItem;
using Shuttle.Abacus.UI.Messages.Formula;
using Shuttle.Abacus.UI.UI.FormulaOperation;
using Shuttle.Esb;

namespace Shuttle.Abacus.UI.UI.Formula
{
    public class FormulaController : WorkItemController, IFormulaController
    {
        public FormulaController(IServiceBus serviceBus, IMessageBus messageBus)
            : base(serviceBus, messageBus)
        {
        }

        public void HandleMessage(RegisterFormulaMessage message)
        {
            if (!WorkItem.PresentationValid())
            {
                return;
            }

            var formulaView = WorkItem.GetView<IFormulaView>();

            Send(new RegisterFormulaCommand
            {
                Name = formulaView.NameValue
            });
        }


        public void HandleMessage(RenameFormulaMessage message)
        {
            if (!WorkItem.PresentationValid())
            {
                return;
            }

            var formulaView = WorkItem.GetView<IFormulaView>();

            Send(new RenameFormulaCommand
            {
                FormulaId = message.FormulaId,
                Name = formulaView.NameValue
            });
        }

        public void HandleMessage(RemoveFormulaMessage message)
        {
            Send(new RemoveFormulaCommand
            {
                FormulaId = message.FormulaId
            });
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

        public void HandleMessage(ManageFormulaConstraintsMessage message)
        {
            throw new System.NotImplementedException();
        }
    }
}