using System;
using Shuttle.Abacus.UI.Core.Messaging;

namespace Shuttle.Abacus.UI.Messages.FactorAnswer
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
