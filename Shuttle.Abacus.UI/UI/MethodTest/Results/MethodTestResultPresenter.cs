using System;
using Shuttle.Abacus.Localisation;
using Shuttle.Abacus.UI.Core.Presentation;

namespace Shuttle.Abacus.UI.UI.MethodTest.Results
{
    public class MethodTestResultPresenter : Presenter<IMethodTestResultView>, IMethodTestResultPresenter
    {
        public MethodTestResultPresenter(IMethodTestResultView view) : base(view)
        {
            Text = "Results";
            Image = Resources.Image_MethodTestRun;

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
