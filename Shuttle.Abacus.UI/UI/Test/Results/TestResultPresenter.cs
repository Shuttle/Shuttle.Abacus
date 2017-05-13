using System;
using Shuttle.Abacus.Localisation;
using Shuttle.Abacus.UI.Core.Presentation;
using Shuttle.Abacus.UI.Models;

namespace Shuttle.Abacus.UI.UI.Test.Results
{
    public class TestResultPresenter : Presenter<ITestResultView, TestResultModel>, ITestResultPresenter
    {
        public TestResultPresenter(ITestResultView view) : base(view)
        {
            Text = "Results";
            Image = Resources.Image_TestRun;

            DontTrackChanges();
        }

        public void MethodTestResultSelected()
        {
            View.ClearResultDisplays();

            if (!View.HasSelectedItem)
            {
                return;
            }

            View.ShowCalculationLog();
            
            throw new NotImplementedException();
            //var dto = View.SelectedDTO;

            //View.BuildDisplayTree("Test", dto.GraphNodes);
        }

        public void GraphNodeSelected()
        {
            View.ClearDisplayList();

            if (!View.HasSelectedGraphNode)
            {
                return;
            }

            var items = View.SelectedGraphNodes();

            items.ForEach(View.AddGraphNode);
        }
    }
}
