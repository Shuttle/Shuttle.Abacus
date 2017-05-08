﻿using Shuttle.Abacus.UI.Core.Messaging;
using Shuttle.Abacus.UI.Core.WorkItem;
using Shuttle.Abacus.UI.Messages.Core;
using Shuttle.Abacus.UI.Messages.TestCase;

namespace Shuttle.Abacus.UI.WorkItemControllers.Interfaces
{
    public interface IMethodTestManagerController :
        IWorkItemController,
        IMessageHandler<NewTestMessage>,
        IMessageHandler<NewTestFromExistingMessage>,
        IMessageHandler<EditTestMessage>,
        IMessageHandler<RemoveTestMessage>,
        IMessageHandler<RunTestMessage>,
        IMessageHandler<ListReadyMessage>,
        IMessageHandler<MarkAllMessage>,
        IMessageHandler<InvertMarksMessage>,
        IMessageHandler<PrintTestMessage>
    {
    }
}
