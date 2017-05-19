using System.Windows.Forms;

namespace Shuttle.Abacus.Shell.Core.Validation
{
    public interface IViewValidator
    {
        IControlValidatorBuild Control(Control control);
        void ValidateView();
        bool IsValid { get; }
    }
}
