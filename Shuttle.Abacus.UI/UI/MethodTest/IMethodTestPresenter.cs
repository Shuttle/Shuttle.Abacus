using Shuttle.Abacus.UI.Core.Presentation;

namespace Shuttle.Abacus.UI.UI.MethodTest
{
    public interface IMethodTestPresenter : IPresenter
    {
        void ArgumentChanged();
        bool ArgumentAnswerOK();
        void ShowInvalidArgumentAnswersMessage();
    }
}
