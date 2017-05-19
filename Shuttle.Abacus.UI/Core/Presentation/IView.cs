using Shuttle.Abacus.Shell.Core.Validation;

namespace Shuttle.Abacus.Shell.Core.Presentation
{
    public interface IViewReady
    {
        void OnReady();
    }

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
