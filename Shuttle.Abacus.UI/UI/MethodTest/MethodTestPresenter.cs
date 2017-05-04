using System.Data;
using System.Linq;
using Shuttle.Abacus.DataAccess;
using Shuttle.Abacus.Infrastructure;
using Shuttle.Abacus.Invariants.Interfaces;
using Shuttle.Abacus.Invariants.Values;
using Shuttle.Abacus.Localisation;
using Shuttle.Abacus.UI.Core.Formatters;
using Shuttle.Abacus.UI.Core.Presentation;
using Shuttle.Abacus.UI.Messages.Core;
using Shuttle.Abacus.UI.Models;
using Shuttle.Core.Data;
using Shuttle.Core.Infrastructure;

namespace Shuttle.Abacus.UI.UI.MethodTest
{
    public class MethodTestPresenter : Presenter<IMethodTestView, MethodTestModel>, IMethodTestPresenter
    {
        private readonly IArgumentQuery _argumentQuery;
        private readonly IDatabaseContextFactory _databaseContextFactory;
        private readonly IMethodTestRules _rules;
        private readonly IValueTypeValidatorProvider _valueTypeValidatorProvider;

        public MethodTestPresenter(IDatabaseContextFactory databaseContextFactory, IArgumentQuery argumentQuery,
            IMethodTestView view, IMethodTestRules rules, IValueTypeValidatorProvider valueTypeValidatorProvider)
            : base(view)
        {
            Guard.AgainstNull(databaseContextFactory, "databaseContextFactory");
            Guard.AgainstNull(argumentQuery, "argumentQuery");
            Guard.AgainstNull(rules, "rules");
            Guard.AgainstNull(valueTypeValidatorProvider, "valueTypeValidatorProvider");

            _databaseContextFactory = databaseContextFactory;
            _argumentQuery = argumentQuery;
            _rules = rules;
            _valueTypeValidatorProvider = valueTypeValidatorProvider;

            Text = "Test Details";

            Image = Resources.Image_MethodTest;
        }

        public void ArgumentChanged()
        {
            var model = View.ArgumentModel;

            View.DetachValueFormatter();

            using (_databaseContextFactory.Create())
            {
                var answers = _argumentQuery.Answers(model.Id).ToList();

                if (answers.Any())
                {
                    View.EnableAnswerSelection();

                    View.PopulateAnswers(answers.Map(row=>ArgumentColumns.RestrictedAnswerColumns.Answer.MapFrom(row)));
                }
                else
                {
                    if (model.IsMoney())
                    {
                        View.AttachValueFormatter(new MoneyFormatter(View.ValueSelectionControl, View.FormattedControl));
                    }

                    View.EnableAnswerEntry();
                }
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
                View.ShowAnswerError("Please enter a value.");

                return false;
            }

            if (View.ArgumentModel.IsNumber)
            {
                var result =
                    _valueTypeValidatorProvider.Get(View.ArgumentModel.AnswerType)
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

            View.DescriptionRules = _rules.DescriptionRules();
            View.ExpectedResultRules = _rules.ExpectedResultRules();

            foreach (var row in Model.ArgumentRows)
            {
                View.AddArgument(new ArgumentModel().Using(row));
            }

            if (Model.MethodTestRow == null)
            {
                return;
            }

            View.DescriptionValue = MethodTestColumns.Description.MapFrom(Model.MethodTestRow);
            View.ExpectedResultValue = MethodTestColumns.ExpectedResult.MapFrom(Model.MethodTestRow);

            foreach (DataRow row in Model.ArgumentAnswers.Rows)
            {
                View.AddArgumentAnswer(new ArgumentAnswerModel().Using(row));
            }
        }
    }
}