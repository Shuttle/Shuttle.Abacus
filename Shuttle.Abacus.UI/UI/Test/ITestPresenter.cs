using Shuttle.Abacus.UI.Core.Presentation;

namespace Shuttle.Abacus.UI.UI.MethodTest
{
    public interface ITestPresenter : IPresenter
    {
        void ArgumentChanged();
        bool ArgumentAnswerOK();
        void ShowInvalidArgumentAnswersMessage();
    }
}
