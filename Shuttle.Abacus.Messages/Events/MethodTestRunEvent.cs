using System;
using Abacus.DTO;
using NServiceBus;

namespace Abacus.Messages
{
    public class MethodTestRunEvent : IMessage
    {
        public MethodContextDTO MethodContext { get; set; }
        public Guid WorkItemId { get; set; }
        public Guid MethodTestId { get; set; }
        public string MethodTestDescription { get; set; }
        public decimal ExpectedResult { get; set; }
    }
}
