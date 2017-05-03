using System;

namespace Shuttle.Abacus.Domain
{
    public class CopyMethodCommand 
    {
        public Guid MethodId { get; set; }
        public string MethodName { get; set; }
    }
}
