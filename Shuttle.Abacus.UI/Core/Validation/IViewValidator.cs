using System.Windows.Forms;

namespace Abacus.UI
{
    public interface IViewValidator
    {
        IControlValidatorBuild Control(Control control);
        void ValidateView();
        bool IsValid { get; }
    }
}
