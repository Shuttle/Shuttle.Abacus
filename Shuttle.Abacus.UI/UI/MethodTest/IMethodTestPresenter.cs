namespace Abacus.UI
{
    public interface IMethodTestPresenter : IPresenter
    {
        void ArgumentChanged();
        bool ArgumentAnswerOK();
        void ShowInvalidArgumentAnswersMessage();
    }
}
