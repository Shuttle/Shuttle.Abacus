using System;

namespace Shuttle.Abacus
{
    public class ChangeLimitCommand : IChangeLimitCommand
    {
        public Guid LimitId { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
    }
}
