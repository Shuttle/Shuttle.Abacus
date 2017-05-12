using Shuttle.Abacus.Localisation;
using Shuttle.Abacus.UI.Messages.Core;
using Shuttle.Abacus.UI.Messages.DecimalTable;
using Shuttle.Abacus.UI.Messages.FactorAnswer;
using Shuttle.Abacus.UI.Messages.Formula;
using Shuttle.Abacus.UI.Messages.Report;
using Shuttle.Abacus.UI.Messages.Section;
using Shuttle.Abacus.UI.Messages.SystemUser;
using Shuttle.Abacus.UI.Messages.TestCase;

namespace Shuttle.Abacus.UI.Navigation
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
                .RegisterResourceItem<DisplayHelpManualMessage>(new ResourceItem("Help"))
                .RegisterResourceItem<ListSystemUserMessage>()
                .RegisterResourceItem<NewSystemUserMessage>()
                .RegisterResourceItem<EditLoginNameMessage>(new ResourceItem("EditLoginName", "SystemUserEdit"))
                .RegisterResourceItem<EditPermissionsMessage>(new ResourceItem("EditPermissions", "PermissionsEdit"))
                .RegisterResourceItem<MoveUpMessage>(new ResourceItem("MoveUp", "UpArrow"))
                .RegisterResourceItem<MoveDownMessage>(new ResourceItem("MoveDown", "DownArrow"))
                .RegisterResourceItem<NewMethodMessage>()
                .RegisterResourceItem<NewMethodFromExistingMessage>(new ResourceItem("NewFromThis", "Copy"))
                .RegisterResourceItem<EditMethodMessage>()
                .RegisterResourceItem<DeleteMethodMessage>()
                .RegisterResourceItem<ManageTestsMessage>(new ResourceItem("Manage", "Test"))
                .RegisterResourceItem<NewTestMessage>()
                .RegisterResourceItem<NewTestFromExistingMessage>(new ResourceItem("NewFromSelected", "Copy"))
                .RegisterResourceItem<EditTestMessage>()
                .RegisterResourceItem<ChangeTestMessage>(ResourceItems.Submit)
                .RegisterResourceItem<RemoveTestMessage>()
                .RegisterResourceItem<RunTestMessage>(new ResourceItem("Run", "RefreshSmall"))
                .RegisterResourceItem<PrintTestMessage>(new ResourceItem("Print", "Report"))
                .RegisterResourceItem<RemoveFormulaMessage>()
                .RegisterResourceItem<ManageFormulaConstraintsMessage>(new ResourceItem("Manage", "Constraint"))
                .RegisterResourceItem<NewMatrixMessage>()
                .RegisterResourceItem<EditMatrixMessage>()
                .RegisterResourceItem<MatrixReportMessage>(new ResourceItem("ReportDecimalTable", "Report"))
                .RegisterResourceItem<NewDecimalTableFromExistingMessage>(new ResourceItem("NewFromThis", "Copy"))
                .RegisterResourceItem<CopyMessage>(new ResourceItem("Copy"));
        }
    }
}
