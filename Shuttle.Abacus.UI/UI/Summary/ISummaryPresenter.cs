using System.Collections.Generic;
using Shuttle.Abacus.UI.Core.Presentation;

namespace Shuttle.Abacus.UI.UI.Summary
{
    public interface ISummaryPresenter : IPresenter
    {
        void Populate(IEnumerable<NamedQueryResult> namedQueryResults);
    }
}
