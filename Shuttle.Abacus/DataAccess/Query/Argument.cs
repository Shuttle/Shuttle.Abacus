using System;
using System.Collections.Generic;

namespace Shuttle.Abacus.DataAccess.Query
{
    public class Argument
    {
        public Argument()
        {
            Values = new List<string>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string DataTypeName { get; set; }

        public List<string> Values { get; set; }
    }
}