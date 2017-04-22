using System.Windows.Forms;

namespace Shuttle.Abacus.UI.Core.Validation
{
    public interface IViewValidator
    {
        IControlValidatorBuild Control(Control control);
        void ValidateView();
        bool IsValid { get; }
    }
}
