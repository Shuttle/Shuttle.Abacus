using System;

namespace Shuttle.Abacus.Domain
{
    public class ChangeMethodCommand 
    {
        public Guid MethodId { get; set; }
        public string MethodName { get; set; }
    }
}
