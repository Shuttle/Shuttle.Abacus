using System;
using System.Collections.Generic;
using System.Data;

namespace Shuttle.Abacus.UI.Models
{
    public class TestModel
    {
        public IEnumerable<DataRow> ArgumentRows { get; set; }
        public DataRow MethodTestRow { get; set; }
        public DataTable ArgumentValues { get; set; }

        public ArgumentModel GetArgument(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}