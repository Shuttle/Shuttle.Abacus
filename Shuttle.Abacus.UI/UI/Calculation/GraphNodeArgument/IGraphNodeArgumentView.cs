using System.Collections.Generic;
using Shuttle.Abacus.DTO;
using Shuttle.Abacus.UI.Core.Presentation;

namespace Shuttle.Abacus.UI.UI.Calculation.GraphNodeArgument
{
    public interface IGraphNodeArgumentView : IView
    {
        void PopulateFactors(IEnumerable<ArgumentDTO> list);
        ArgumentDTO ArgumentDto { get; }
        string FormatValue { get; }
        bool HasArgument { get; }
        bool HasFormat { get; }
        IEnumerable<GraphNodeArgumentDTO> GraphNodeArguments { get; }
        void ShowArgumentError();
        void ShowFormatError();
        void AddArgument(ArgumentDTO argumentDto, string format);
    }
}
