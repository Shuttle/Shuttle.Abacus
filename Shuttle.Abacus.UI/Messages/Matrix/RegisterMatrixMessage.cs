using System;
using Shuttle.Abacus.Infrastructure;
using Shuttle.Abacus.Shell.Core.Messaging;

namespace Shuttle.Abacus.Shell.Messages.DecimalTable
{
    public class RegisterMatrixMessage : Message
    {
        public Guid Id { get; }

        public RegisterMatrixMessage(Guid id)
        {
            Id = id;
        }

        public override IPermission RequiredPermission => Permissions.Matrix;
    }
}
