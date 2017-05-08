using System;
using System.Collections.Generic;
using Shuttle.Abacus.DTO;
using Shuttle.Abacus.UI.Core.Presentation;

namespace Shuttle.Abacus.UI.UI.MethodTest.Results
{
    public interface ITestResultView : IView
    {
        void AddRun(Guid id, string description, string expectedResult);

        void ClearResultDisplays();
        bool HasSelectedItem { get; }
        void ShowCalculationLog();
        void BuildDisplayTree(string name, List<GraphNodeDTO> items);
        void ClearDisplayList();
        bool HasSelectedGraphNode { get; }
        List<GraphNodeDTO> SelectedGraphNodes();
        void AddGraphNode(GraphNodeDTO dto);
    }
}
