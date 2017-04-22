using System;
using Shuttle.Abacus.UI.Core.Messaging;

namespace Shuttle.Abacus.UI.Messages.Limit
{
    public class EditLimitMessage : Message
    {
        public Guid LimitId { get; private set; }
        public string Text { get; private set; }

        public EditLimitMessage(Guid limitId, string text)
        {
            LimitId = limitId;
            Text = text;
        }

        public override IPermission RequiredPermission
        {
            get { return Permissions.Method; }
        }
    }
}
