using System.Drawing;
using System.Windows.Forms;
using Shuttle.Abacus.Infrastructure;

namespace Shuttle.Abacus.UI.Core.Validation
{
    public interface IControlValidation
    {
        IResult Evaluate();
        Control Control { get; }
        Color OriginalControlColor { get; }
    }
}
