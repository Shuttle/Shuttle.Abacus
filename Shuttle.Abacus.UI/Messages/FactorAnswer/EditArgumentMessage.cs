using System;
using Abacus.Infrastructure;

namespace Abacus.UI
{
    public class EditArgumentMessage : Message
    {
        public EditArgumentMessage(Guid argumentId)
        {
            ArgumentId = argumentId;
        }

        public override IPermission RequiredPermission
        {
            get { return Permissions.Argument; }
        }

        public Guid ArgumentId { get; private set; }
    }
}
