using System.Data;
using Shuttle.Abacus.UI.Core.Presentation;

namespace Shuttle.Abacus.UI.UI.Argument.RestrictedAnswer
{
    public class ArgumentRestrictedAnswerPresenter : Presenter<IArgumentRestrictedAnswerView, IQueryResult>,
                                                IArgumentRestrictedAnswerPresenter
    {
        public ArgumentRestrictedAnswerPresenter(IArgumentRestrictedAnswerView view) : base(view)
        {
            Text = "Restricted Answers Details";
            Image = Resources.Image_ArgumentRestrictedAnswer;
        }

        public bool AnswerOK()
        {
            var result = true;

            if (!View.HasAnswer())
            {
                View.ShowAnswerError("Please enter an answer.");

                result = false;
            }

            return result;
        }

        public override void OnInitialize()
        {
            base.OnInitialize();

            if (Model == null)
            {
                return;
            }

            foreach (DataRow row in Model.Table.Rows)
            {
                View.AddRestrictedAnswer(ArgumentColumns.RestrictedAnswerColumns.Answer.MapFrom(row));
            }
        }
    }
}
