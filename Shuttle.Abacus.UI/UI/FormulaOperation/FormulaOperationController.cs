using System;
using Shuttle.Abacus.UI.Core.Messaging;
using Shuttle.Abacus.UI.Core.WorkItem;
using Shuttle.Abacus.UI.Messages.Core;
using Shuttle.Abacus.UI.Messages.Formula;
using Shuttle.Abacus.UI.UI.List;
using Shuttle.Esb;

namespace Shuttle.Abacus.UI.UI.FormulaOperation
{
    public class FormulaOperationController : WorkItemController, IFormulaOperationController
    {
        public FormulaOperationController(IServiceBus serviceBus, IMessageBus messageBus,
            ICallbackRepository callbackRepository)
            : base(serviceBus, messageBus, callbackRepository)
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
            throw new NotImplementedException();
            //Operations = formulaView.FormulaOperations.Map(item => new FormulaOperation
            //{
            //    SequenceNumber = item.SequenceNumber,
            //    ValueSelection = item.ValueSelection,
            //    Operation = item.Operation,
            //    ValueSource = item.ValueSource,
            //    Text = item.Text
            //}),
            //Constraints = constraintView.Constraints.Map(item => new FormulaConstraint
            //{
            //    ArgumentName = item.ArgumentName,
            //    Name = item.Name,
            //    Value = item.Value
            //})        
        }
    }
}