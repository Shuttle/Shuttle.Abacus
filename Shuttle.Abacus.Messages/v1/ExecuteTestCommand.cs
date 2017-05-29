using System;

namespace Shuttle.Abacus.Messages.v1
{
    public class ExecuteTestCommand
    {
        public Guid Id { get; set; }
        public string LogLevel { get; set; }
    }
}