using System;
using Shuttle.Abacus.UI.Core.Messaging;
using Shuttle.Abacus.UI.Core.Resources;

namespace Shuttle.Abacus.UI.Messages.Calculation
{
    public class GrabCalculationsMessage : Message
    {
        public GrabCalculationsMessage(Guid grabberCalculationId, Guid methodId, string text, Resource methodResource)
        {
            GrabberCalculationId = grabberCalculationId;
            MethodId = methodId;
            Text = text;
            MethodResource = methodResource;
        }

        public Guid GrabberCalculationId { get; private set; }
        public Guid MethodId { get; private set; }
        public string Text { get; private set; }
        public Resource MethodResource { get; private set; }

        public override IPermission RequiredPermission
        {
            get { return Permissions.Method; }
        }
    }
}
