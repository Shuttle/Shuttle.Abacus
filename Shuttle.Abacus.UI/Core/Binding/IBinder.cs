using System;
using System.Collections.Generic;
using System.Data;

namespace Shuttle.Abacus.UI.Core.Binding
{
    public interface IBinder
    {
        Type ForType { get; }
    }

    public interface IBinder<T> : IBinder
    {
        void Bind(IEnumerable<DataRow> rows, T to);
        void Bind(IEnumerable<DataRow> rows, T to, IEnumerable<string> visibleColumns, IEnumerable<string> hiddenColumns);
    }
}
