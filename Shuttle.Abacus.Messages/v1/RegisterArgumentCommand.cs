using System;

namespace Shuttle.Abacus.Messages.v1
{
    public class RegisterArgumentCommand
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string DataTypeName { get; set; }
    }
}