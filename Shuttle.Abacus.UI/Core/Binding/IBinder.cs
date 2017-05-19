using System;
using System.Collections.Generic;
using System.Data;

namespace Shuttle.Abacus.Shell.Core.Binding
{
    public interface IBinder
    {
        Type ForType { get; }
    }

    public interface IBinder<T> : IBinder
    {
        void Bind(string keyColumnName, IEnumerable<DataRow> rows, T to);
        void Bind(string keyColumnName, IEnumerable<DataRow> rows, T to, IEnumerable<string> visibleColumns, IEnumerable<string> hiddenColumns);
    }
}
