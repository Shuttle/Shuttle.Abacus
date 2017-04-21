using System;

namespace Shuttle.Abacus
{
    public class ChangeMethodCommand : IChangeMethodCommand
    {
        public Guid MethodId { get; set; }
        public string MethodName { get; set; }
    }
}
