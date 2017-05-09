using System;
using System.Data;
using Shuttle.Abacus.DataAccess;
using Shuttle.Abacus.UI.Coordinators.Interfaces;
using Shuttle.Abacus.UI.Core.Presentation;
using Shuttle.Abacus.UI.Messages.Report;
using Shuttle.Abacus.UI.Messages.TestCase;
using Shuttle.Abacus.UI.Models;
using Shuttle.Core.Data;
using Shuttle.Core.Infrastructure;

namespace Shuttle.Abacus.UI.Coordinators
{
    public class ReportCoordinator : Coordinator, IReportCoordinator
    {
        private const string Tab = "   ";
        private readonly IDatabaseContextFactory _databaseContextFactory;
        public IMatrixQuery _matrixQuery;

        public ReportCoordinator(IDatabaseContextFactory databaseContextFactory, IMatrixQuery matrixQuery)
        {
            Guard.AgainstNull(databaseContextFactory, "databaseContextFactory");
            Guard.AgainstNull(matrixQuery, "matrixQuery");

            _databaseContextFactory = databaseContextFactory;
            _matrixQuery = matrixQuery;
        }


        public void HandleMessage(DecimalTableReportMessage message)
        {
            using (_databaseContextFactory.Create())
            {
                var model = new ReportModel
                {
                    ReportDatasetName = "ReportsDataSet_DecimalTable",
                    ReportDefinitionName = "Abacus.Reporting.Reports.Matrix.rdlc",
                    ReportData = _matrixQuery.Report(message.MatrixId)
                };
            }

            //var item = WorkItemManager
            //    .Create("Decimal Table Report: " + message.DecimalTableName)
            //    .ControlledBy<IReportController>()
            //    .ShowIn<IContextToolbarPresenter>()
            //    .AddPresenter<IReportPresenter>().WithModel(model)
            //    .AssignWorkItemImage(Resources.Image_DecimalTable);

            //HostInWorkspace<ITabbedWorkspacePresenter>(item);
        }

        public void HandleMessage(TestPrintMessage message)
        {
            throw new NotImplementedException();
            //var table = new DataTable("RunEvents");

            //table.Columns.Add(new DataColumn("MethodTestName", typeof(string)));
            //table.Columns.Add(new DataColumn("ExpectedResult", typeof(decimal)));
            //table.Columns.Add(new DataColumn("ActualResult", typeof(decimal)));
            //table.Columns.Add(new DataColumn("CalculationLog", typeof(string)));

            //foreach (var testResult in message.Event.RunEvents)
            //{
            //    var logLines = testResult.MethodContext.GetLog().Replace("\n", string.Empty).Split("\r".ToCharArray());

            //    foreach (var line in logLines)
            //    {
            //        var row = table.NewRow();

            //        row["MethodTestName"] = testResult.MethodTestDescription;
            //        row["ExpectedResult"] = testResult.ExpectedResult;
            //        row["ActualResult"] = testResult.MethodContext.Total;
            //        row["CalculationLog"] = line.Replace("\t", Tab);

            //        table.Rows.Add(row);
            //    }
            //}

            //var model = new ReportModel
            //{
            //    ReportDatasetName = "ReportsDataSet_MethodTestResult",
            //    ReportDefinitionName = "Abacus.Reporting.Reports.Test.rdlc",
            //    ReportData = table
            //};

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