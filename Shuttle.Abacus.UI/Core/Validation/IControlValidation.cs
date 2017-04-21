using System.Drawing;
using System.Windows.Forms;
using Abacus.Infrastructure;

namespace Abacus.UI
{
    public interface IControlValidation
    {
        IResult Evaluate();
        Control Control { get; }
        Color OriginalControlColor { get; }
    }
}
