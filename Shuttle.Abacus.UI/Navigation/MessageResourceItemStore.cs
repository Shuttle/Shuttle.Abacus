using Shuttle.Abacus.Localisation;
using Shuttle.Abacus.Shell.Messages.Core;
using Shuttle.Abacus.Shell.Messages.DecimalTable;
using Shuttle.Abacus.Shell.Messages.Formula;
using Shuttle.Abacus.Shell.Messages.Report;
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
                .RegisterResourceItem<RemoveTestMessage>()
                .RegisterResourceItem<TestExecutionMessage>(new ResourceItem("Run", "RefreshSmall"))
                .RegisterResourceItem<PrintTestMessage>(new ResourceItem("Print", "Report"))
                .RegisterResourceItem<RemoveFormulaMessage>()
                .RegisterResourceItem<ManageFormulaConstraintsMessage>(new ResourceItem("Manage", "FormulaConstraint"))
                .RegisterResourceItem<NewMatrixMessage>()
                .RegisterResourceItem<EditMatrixMessage>()
                .RegisterResourceItem<MatrixReportMessage>(new ResourceItem("ReportDecimalTable", "Report"))
                .RegisterResourceItem<NewDecimalTableFromExistingMessage>(new ResourceItem("NewFromThis", "Copy"))
                .RegisterResourceItem<CopyMessage>(new ResourceItem("Copy"));
        }
    }
}
