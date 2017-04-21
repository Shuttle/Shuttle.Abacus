using System.Collections.Generic;
using Abacus.DTO;

namespace Abacus.UI
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
