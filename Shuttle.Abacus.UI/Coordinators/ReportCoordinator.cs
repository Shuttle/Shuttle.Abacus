using System;
using System.Data;
using Shuttle.Abacus.DataAccess;
using Shuttle.Abacus.UI.Coordinators.Interfaces;
using Shuttle.Abacus.UI.Core.Presentation;
using Shuttle.Abacus.UI.Messages.Report;
using Shuttle.Abacus.UI.Messages.TestCase;
using Shuttle.Abacus.UI.Models;

namespace Shuttle.Abacus.UI.Coordinators
{
    public class ReportCoordinator : Coordinator, IReportCoordinator
    {
        private const string tab = "   ";

        public ReportCoordinator(IDecimalTableQuery decimalTableQuery)
        {
            DecimalTableQuery = decimalTableQuery;
        }

        public IDecimalTableQuery DecimalTableQuery { get; set; }

        public void HandleMessage(DecimalTableReportMessage message)
        {
            var model = new ReportModel
                        {
                            ReportDatasetName = "ReportsDataSet_DecimalTable",
                            ReportDefinitionName = "Abacus.Reporting.Reports.DecimalTable.rdlc",
                            ReportData = DecimalTableQuery.QueryDecimalTable(message.DecimalTableId)
                        };

            //var item = WorkItemManager
            //    .Create("Decimal Table Report: " + message.DecimalTableName)
            //    .ControlledBy<IReportController>()
            //    .ShowIn<IContextToolbarPresenter>()
            //    .AddPresenter<IReportPresenter>().WithModel(model)
            //    .AssignWorkItemImage(Resources.Image_DecimalTable);

            //HostInWorkspace<ITabbedWorkspacePresenter>(item);
        }

        public void HandleMessage(MethodTestPrintMessage message)
        {
            var table = new DataTable("RunEvents");

            table.Columns.Add(new DataColumn("MethodTestName", typeof(string)));
            table.Columns.Add(new DataColumn("ExpectedResult", typeof (decimal)));
            table.Columns.Add(new DataColumn("ActualResult", typeof (decimal)));
            table.Columns.Add(new DataColumn("CalculationLog", typeof (string)));

            foreach (var testResult in message.Event.RunEvents)
            {
                var logLines = testResult.MethodContext.GetLog().Replace("\n",String.Empty).Split("\r".ToCharArray());

                foreach (var line in logLines)
                {
                    var row = table.NewRow();

                    row["MethodTestName"] = testResult.MethodTestDescription;
                    row["ExpectedResult"] = testResult.ExpectedResult;
                    row["ActualResult"] = testResult.MethodContext.Total;
                    row["CalculationLog"] = line.Replace("\t", tab);

                    table.Rows.Add(row);
                }
            }

            var model = new ReportModel
                        {
                            ReportDatasetName = "ReportsDataSet_MethodTestResult",
                            ReportDefinitionName = "Abacus.Reporting.Reports.MethodTest.rdlc",
                            ReportData = table
                        };

            //var item = WorkItemManager
            //    .Create("Test Results Report")
            //    .ControlledBy<IReportController>()
            //    .ShowIn<IContextToolbarPresenter>()
            //    .AddPresenter<IReportPresenter>().WithModel(model)
            //    .AssignWorkItemImage(Resources.Image_MethodTest);

            //HostInWorkspace<ITabbedWorkspacePresenter>(item);
        }
    }
}
