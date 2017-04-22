using System.Data;
using Shuttle.Abacus.DataAccess;
using Shuttle.Abacus.Infrastructure;
using Shuttle.Abacus.Invariants.Interfaces;
using Shuttle.Abacus.Invariants.Values;
using Shuttle.Abacus.Localisation;
using Shuttle.Abacus.UI.Core.Formatters;
using Shuttle.Abacus.UI.Core.Presentation;
using Shuttle.Abacus.UI.Messages.Core;
using Shuttle.Abacus.UI.Models;

namespace Shuttle.Abacus.UI.UI.MethodTest
{
    public class MethodTestPresenter : Presenter<IMethodTestView, MethodTestModel>, IMethodTestPresenter
    {
        private readonly IMethodTestRules testRules;
        private readonly IValueTypeValidatorProvider valueTypeValidatorProvider;

        public MethodTestPresenter(IMethodTestView view, IMethodTestRules testRules, IValueTypeValidatorProvider valueTypeValidatorProvider)
            : base(view)
        {
            this.testRules = testRules;
            this.valueTypeValidatorProvider = valueTypeValidatorProvider;

            Text = "Test Details";

            Image = Resources.Image_MethodTest;
        }

        public void ArgumentChanged()
        {
            var dto = View.ArgumentDto;

            View.DetachValueFormatter();

            if (dto.HasAnswerCatalog)
            {
                View.EnableAnswerSelection();

                View.PopulateAnswerCatalog(dto.Answers);
            }
            else
            {
                if (dto.IsMoney)
                {
                    View.AttachValueFormatter(new MoneyFormatter(View.ValueSelectionControl, View.FormattedControl));
                }

                View.EnableAnswerEntry();
            }
        }

        public bool ArgumentAnswerOK()
        {
            if (!View.HasArgument)
            {
                View.ShowArgumentError();

                return false;
            }

            if (!View.HasAnswer)
            {
                if (View.ArgumentDto.HasAnswerCatalog)
                {
                    View.ShowAnswerError("Please make a selection");
                }
                else
                {
                    View.ShowAnswerError("Please enter a value.");
                }

                return false;
            }

            if (View.ArgumentDto.IsNumber)
            {
                var result =
                    valueTypeValidatorProvider.Get(View.ArgumentDto.AnswerType)
                        .Validate(View.AnswerValue);

                if (!result.OK)
                {
                    View.ShowAnswerError(result.ToString());

                    return false;
                }
            }

            return true;
        }

        public void ShowInvalidArgumentAnswersMessage()
        {
            MessageBus.Publish(
                new ResultNotificationMessage(Result.Create().AddFailureMessage("Not all argument answers are valid.")));
        }

        public override void OnInitialize()
        {
            base.OnInitialize();

            View.DescriptionRules = testRules.DescriptionRules();
            View.ExpectedResultRules = testRules.ExpectedResultRules();

            foreach (var dto in Model.Arguments)
            {
                View.AddArgument(dto);
            }

            if (Model.MethodTestRow == null)
            {
                return;
            }

            View.DescriptionValue = MethodTestColumns.Description.MapFrom(Model.MethodTestRow);
            View.ExpectedResultValue = MethodTestColumns.ExpectedResult.MapFrom(Model.MethodTestRow);

            foreach (DataRow row in Model.ArgumentAnswers.Rows)
            {
                View.AddArgumentAnswer(
                    MethodTestColumns.ArgumentAnswerColumns.ArgumentId.MapFrom(row),
                    MethodTestColumns.ArgumentAnswerColumns.Answer.MapFrom(row));
            }
        }
    }
}
