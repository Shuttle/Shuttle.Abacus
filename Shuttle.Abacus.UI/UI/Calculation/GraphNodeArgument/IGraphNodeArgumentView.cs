using System.Collections.Generic;
using System.Data;
using Shuttle.Abacus.DTO;
using Shuttle.Abacus.UI.Core.Presentation;

namespace Shuttle.Abacus.UI.UI.Calculation.GraphNodeArgument
{
    public interface IGraphNodeArgumentView : IView
    {
        void PopulateArguments(IEnumerable<DataRow> rows);
        DataRow ArgumentRow { get; }
        string FormatValue { get; }
        bool HasArgument { get; }
        bool HasFormat { get; }
        IEnumerable<GraphNodeDataRow> GraphNodeArguments { get; }
        void ShowArgumentError();
        void ShowFormatError();
        void AddArgumentRow(DataRow row, string format);
    }
}
