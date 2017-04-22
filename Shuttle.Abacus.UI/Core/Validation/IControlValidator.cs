using System;
using System.Windows.Forms;

namespace Shuttle.Abacus.UI.Core.Validation
{
    public interface IControlValidator
    {
        Type HandlesType { get; }
        IResult Validate(Control control, IRuleCollection<object> rules);
        void WireValidationRequired(Control control, IControlValidation validation);
    }
}
