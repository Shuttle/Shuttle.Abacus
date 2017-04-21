using System;
using System.Windows.Forms;
using Abacus.Infrastructure;
using Abacus.Validation;

namespace Abacus.UI
{
    public interface IControlValidator
    {
        Type HandlesType { get; }
        IResult Validate(Control control, IRuleCollection<object> rules);
        void WireValidationRequired(Control control, IControlValidation validation);
    }
}
