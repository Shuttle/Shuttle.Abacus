using System;
using System.Collections.Generic;
using Abacus.DataContracts;
using Abacus.DTO;

namespace Abacus.UI
{
    public interface IMethodTestResultView : IView
    {
        void AddRun(Guid id, string description, decimal expectedResult, MethodContextDTO contextDTO);

        void ClearResultDisplays();
        bool HasSelectedItem { get; }
        MethodContextDTO SelectedDTO { get; }
        void ShowCalculationLog();
        void BuildDisplayTree(string name, List<GraphNodeDTO> items);
        void ClearDisplayList();
        bool HasSelectedGraphNode { get; }
        List<GraphNodeDTO> SelectedGraphNodes();
        void AddGraphNode(GraphNodeDTO dto);
    }
}
