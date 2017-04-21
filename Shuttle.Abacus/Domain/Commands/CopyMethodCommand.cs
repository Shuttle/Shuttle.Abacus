using System;

namespace Shuttle.Abacus
{
    public class CopyMethodCommand : ICopyMethodCommand
    {
        public Guid MethodId { get; set; }
        public string MethodName { get; set; }
    }
}
