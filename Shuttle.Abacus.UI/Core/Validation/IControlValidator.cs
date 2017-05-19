using System;
using System.Windows.Forms;
using Shuttle.Abacus.Infrastructure;
using Shuttle.Abacus.Invariants.Core;

namespace Shuttle.Abacus.Shell.Core.Validation
{
    public interface IControlValidator
    {
        Type HandlesType { get; }
        IResult Validate(Control control, IRuleCollection<object> rules);
        void WireValidationRequired(Control control, IControlValidation validation);
    }
}
