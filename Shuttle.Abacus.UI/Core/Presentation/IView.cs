using Shuttle.Abacus.UI.Core.Validation;

namespace Shuttle.Abacus.UI.Core.Presentation
{
    public interface IView
    {
        IPresenter IPresenter { get; }
        IViewValidationManager ViewValidationManager { get; }
        bool IsValid { get; }
        void AttachPresenter(IPresenter presenter);
        void AttachViewValidator(IViewValidator validator);
        void ValidateView();
        bool Confirmed(string message);
        void ShowView();
    }
}
