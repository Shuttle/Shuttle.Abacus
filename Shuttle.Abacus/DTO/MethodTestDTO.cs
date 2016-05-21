using System;
using System.Collections.Generic;

namespace Shuttle.Abacus
{
    public class MethodTestDTO
    {
        public MethodTestDTO()
        {
            InputList = new Dictionary<string, string>();
        }

        public Guid MethodTestId { get; set; }

        public Guid MethodId { get; set; }

        public string Description { get; set; }

        public decimal ExpectedResult { get; set; }

        public Dictionary<string, string> InputList { get; set; }
    }
}
