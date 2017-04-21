using System.Collections.Generic;

namespace Abacus.UI
{
    public interface ISummaryPresenter : IPresenter
    {
        void Populate(IEnumerable<NamedQueryResult> namedQueryResults);
    }
}
