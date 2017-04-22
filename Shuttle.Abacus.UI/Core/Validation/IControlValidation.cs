using System.Drawing;
using System.Windows.Forms;

namespace Shuttle.Abacus.UI.Core.Validation
{
    public interface IControlValidation
    {
        IResult Evaluate();
        Control Control { get; }
        Color OriginalControlColor { get; }
    }
}
