using System;
using Abacus.Infrastructure;

namespace Abacus.UI
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
