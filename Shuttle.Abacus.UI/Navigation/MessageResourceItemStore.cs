using Shuttle.Abacus.Localisation;
using Shuttle.Abacus.Shell.Messages.Core;
using Shuttle.Abacus.Shell.Messages.DecimalTable;
using Shuttle.Abacus.Shell.Messages.Formula;
using Shuttle.Abacus.Shell.Messages.Section;
using Shuttle.Abacus.Shell.Messages.SystemUser;
using Shuttle.Abacus.Shell.Messages.Test;

namespace Shuttle.Abacus.Shell.Navigation
{
    public class MessageResourceItemStore : IMessageResourceItemStore
    {
        public void Fill(INavigationItemFactory factory)
        {
            factory
                .RegisterResourceItem<MarkAllMessage>(new ResourceItem("MarkAll", "Mark"))
                .RegisterResourceItem<InvertMarksMessage>(new ResourceItem("InvertMarks", "Mark"))
                .RegisterResourceItem<ApplicationExitMessage>(new ResourceItem("Exit"))
                .RegisterResourceItem<ShowSummaryViewMessage>(new ResourceItem("ShowSummaryView", "Show"))
                .RegisterResourceItem<ListSystemUserMessage>()
                .RegisterResourceItem<NewSystemUserMessage>()
                .RegisterResourceItem<EditLoginNameMessage>(new ResourceItem("EditLoginName", "SystemUserEdit"))
                .RegisterResourceItem<EditPermissionsMessage>(new ResourceItem("EditPermissions", "PermissionsEdit"))
                .RegisterResourceItem<MoveUpMessage>(new ResourceItem("MoveUp", "UpArrow"))
                .RegisterResourceItem<MoveDownMessage>(new ResourceItem("MoveDown", "DownArrow"))
                .RegisterResourceItem<NewMethodMessage>()
                .RegisterResourceItem<RemoveTestMessage>()
                .RegisterResourceItem<TestExecutionMessage>(new ResourceItem("Run", "RefreshSmall"))
                .RegisterResourceItem<RemoveFormulaMessage>();
        }
    }
}
