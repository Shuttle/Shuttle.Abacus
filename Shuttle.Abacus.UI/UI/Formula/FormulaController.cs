using Shuttle.Abacus.Messages.v1;
using Shuttle.Abacus.UI.Core.Messaging;
using Shuttle.Abacus.UI.Core.WorkItem;
using Shuttle.Abacus.UI.Messages.Formula;
using Shuttle.Abacus.UI.Messages.Resources;
using Shuttle.Abacus.UI.Messages.WorkItem;
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
    }
}