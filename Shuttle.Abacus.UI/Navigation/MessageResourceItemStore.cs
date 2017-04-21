using Abacus.Localisation;

namespace Abacus.UI
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
                .RegisterResourceItem<NewCalculationMessage>()
                .RegisterResourceItem<EditCalculationMessage>()
                .RegisterResourceItem<DeleteCalculationMessage>()
                .RegisterResourceItem<GrabCalculationsMessage>(new ResourceItem("GrabCalculations", "Grab"))
                .RegisterResourceItem<ChangeCalculationOrderMessage>(new ResourceItem("ChangeOrder"))
                .RegisterResourceItem<MoveUpMessage>(new ResourceItem("MoveUp", "UpArrow"))
                .RegisterResourceItem<MoveDownMessage>(new ResourceItem("MoveDown", "DownArrow"))
                .RegisterResourceItem<NewArgumentMessage>()
                .RegisterResourceItem<EditArgumentMessage>()
                .RegisterResourceItem<DeleteArgumentMessage>()
                .RegisterResourceItem<NewMethodMessage>()
                .RegisterResourceItem<NewMethodFromExistingMessage>(new ResourceItem("NewFromThis", "Copy"))
                .RegisterResourceItem<EditMethodMessage>()
                .RegisterResourceItem<DeleteMethodMessage>()
                .RegisterResourceItem<ManageMethodTestsMessage>(new ResourceItem("Manage", "MethodTest"))
                .RegisterResourceItem<NewMethodTestMessage>()
                .RegisterResourceItem<NewMethodTestFromExistingMessage>(new ResourceItem("NewFromSelected", "Copy"))
                .RegisterResourceItem<EditMethodTestMessage>()
                .RegisterResourceItem<ChangeMethodTestMessage>(ResourceItems.Submit)
                .RegisterResourceItem<RemoveMethodTestMessage>()
                .RegisterResourceItem<RunMethodTestMessage>(new ResourceItem("Run", "RefreshSmall"))
                .RegisterResourceItem<PrintMethodTestMessage>(new ResourceItem("Print", "Report"))
                .RegisterResourceItem<NewFormulaMessage>()
                .RegisterResourceItem<NewFormulaFromExistingMessage>(new ResourceItem("NewFromThis", "Copy"))
                .RegisterResourceItem<ChangeFormulaOrderMessage>(new ResourceItem("ChangeOrder"))
                .RegisterResourceItem<EditFormulaMessage>()
                .RegisterResourceItem<DeleteFormulaMessage>()
                .RegisterResourceItem<PasteFormulaMessage>(new ResourceItem("Paste"))
                .RegisterResourceItem<ManageCalculationConstraintsMessage>(new ResourceItem("Manage", "Constraint"))
                .RegisterResourceItem<NewLimitMessage>()
                .RegisterResourceItem<EditLimitMessage>()
                .RegisterResourceItem<DeleteLimitMessage>()
                .RegisterResourceItem<NewDecimalTableMessage>()
                .RegisterResourceItem<EditDecimalTableMessage>()
                .RegisterResourceItem<DecimalTableReportMessage>(new ResourceItem("ReportDecimalTable", "Report"))
                .RegisterResourceItem<NewDecimalTableFromExistingMessage>(new ResourceItem("NewFromThis", "Copy"))
                .RegisterResourceItem<CopyMessage>(new ResourceItem("Copy"))
                ;
        }
    }
}
