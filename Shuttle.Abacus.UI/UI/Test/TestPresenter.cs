using System.Data;
using System.Linq;
using Shuttle.Abacus.DataAccess;
using Shuttle.Abacus.Infrastructure;
using Shuttle.Abacus.Invariants.Interfaces;
using Shuttle.Abacus.Invariants.Values;
using Shuttle.Abacus.Localisation;
using Shuttle.Abacus.UI.Core.Presentation;
using Shuttle.Abacus.UI.Models;
using Shuttle.Core.Data;
using Shuttle.Core.Infrastructure;

namespace Shuttle.Abacus.UI.UI.Test
{
    public class TestPresenter : Presenter<ITestView, TestModel>, ITestPresenter
    {
        private readonly IArgumentQuery _argumentQuery;
        private readonly IDatabaseContextFactory _databaseContextFactory;
        private readonly IMethodTestRules _rules;
        private readonly IValueTypeValidatorProvider _valueTypeValidatorProvider;

        public TestPresenter(IDatabaseContextFactory databaseContextFactory, IArgumentQuery argumentQuery,
            ITestView view, IMethodTestRules rules, IValueTypeValidatorProvider valueTypeValidatorProvider)
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

            Image = Resources.Image_Test;
        }

        public void ArgumentChanged()
        {
            var model = View.ArgumentModel;

            using (_databaseContextFactory.Create())
            {
                var answers = _argumentQuery.GetValues(model.Id).ToList();

                if (answers.Any())
                {
                    View.PopulateArgumentValues(answers.Map(row=>ArgumentColumns.ValueColumns.Value.MapFrom(row)));
                }
                else
                {
                    if (model.IsMoney())
                    {
                    }
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

            if (View.ArgumentModel.IsNumber())
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

        public override void OnInitialize()
        {
            base.OnInitialize();

            View.DescriptionRules = _rules.DescriptionRules();
            View.ExpectedResultRules = _rules.ExpectedResultRules();

            foreach (var row in Model.ArgumentRows)
            {
                View.AddArgument(new ArgumentModel(row));
            }

            if (Model.MethodTestRow == null)
            {
                return;
            }

            View.DescriptionValue = TestColumns.Description.MapFrom(Model.MethodTestRow);
            View.ExpectedResultValue = TestColumns.ExpectedResult.MapFrom(Model.MethodTestRow);

            foreach (DataRow row in Model.ArgumentValues.Rows)
            {
                View.AddArgumentValue(Model.GetArgument(TestColumns.ArgumentValueColumns.ArgumentName.MapFrom(row)), TestColumns.ArgumentValueColumns.Value.MapFrom(row));
            }
        }
    }
}