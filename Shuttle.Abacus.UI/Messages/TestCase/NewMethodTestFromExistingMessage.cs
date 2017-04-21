using System;
using Abacus.Infrastructure;

namespace Abacus.UI
{
    public class NewMethodTestFromExistingMessage : Message
    {
        public NewMethodTestFromExistingMessage(Guid methodId)
        {
            MethodId = methodId;
        }

        public Guid MethodId { get; private set; }
        public Guid MethodTestId { get; set; }
        public Guid WorkItemId { get; set; }

        public override IPermission RequiredPermission
        {
            get { return Permissions.MethodTest; }
        }
    }
}
