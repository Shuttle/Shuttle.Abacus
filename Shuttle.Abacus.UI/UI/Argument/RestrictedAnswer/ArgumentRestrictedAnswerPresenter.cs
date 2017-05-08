using System.Collections.Generic;
using System.Data;
using Shuttle.Abacus.DataAccess;
using Shuttle.Abacus.Localisation;
using Shuttle.Abacus.UI.Core.Presentation;

namespace Shuttle.Abacus.UI.UI.Argument.RestrictedAnswer
{
    public class ArgumentRestrictedAnswerPresenter : Presenter<IArgumentRestrictedAnswerView, IEnumerable<DataRow>>,
                                                IArgumentRestrictedAnswerPresenter
    {
        public ArgumentRestrictedAnswerPresenter(IArgumentRestrictedAnswerView view) : base(view)
        {
            Text = "Restricted ArgumentValues Details";
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

            foreach (var row in Model)
            {
                View.AddRestrictedAnswer(ArgumentColumns.ValueColumns.Value.MapFrom(row));
            }
        }
    }
}
