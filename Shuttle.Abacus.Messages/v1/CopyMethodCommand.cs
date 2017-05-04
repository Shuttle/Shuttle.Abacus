using System;

namespace Shuttle.Abacus.Messages.v1
{
    public class CopyMethodCommand 
    {
        public Guid MethodId { get; set; }
        public string MethodName { get; set; }
    }
}
