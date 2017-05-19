using System.Drawing;
using System.Windows.Forms;
using Shuttle.Abacus.Infrastructure;

namespace Shuttle.Abacus.Shell.Core.Validation
{
    public interface IControlValidation
    {
        IResult Evaluate();
        Control Control { get; }
        Color OriginalControlColor { get; }
    }
}
