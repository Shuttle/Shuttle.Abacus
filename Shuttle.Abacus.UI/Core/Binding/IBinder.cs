using System;
using System.Collections.Generic;
using Abacus.Data;

namespace Abacus.UI
{
    public interface IBinder
    {
        Type ForType { get; }
    }

    public interface IBinder<T> : IBinder
    {
        void Bind(IQueryResult result, T to);
        void Bind(IQueryResult result, T to, IList<QueryColumn> visibleColumns, IList<QueryColumn> hiddenColumns);
    }
}
