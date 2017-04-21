using System.Data;
using Abacus.Data;
using Abacus.Localisation;

namespace Abacus.UI
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
