using System;
using System.Collections.Generic;
using Shuttle.Abacus.DTO;

namespace Shuttle.Abacus.Domain
{
    public class ChangeCalculationCommand
    {
        public Guid MethodId { get; set; }
        public Guid CalculationId { get; set; }
        public Guid OwnerId { get; set; }
        public string OwnerName { get; set; }

        public string Name { get; set; }
        public bool Required { get; set; }

        public List<GraphNodeArgumentDTO> GraphNodeArguments { get; set; }
    }
}
