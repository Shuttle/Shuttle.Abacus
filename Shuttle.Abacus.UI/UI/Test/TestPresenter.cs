using Shuttle.Abacus.DataAccess;
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

        public override void OnInitialize()
        {
            base.OnInitialize();

            View.NameRules = _rules.DescriptionRules();
            View.ExpectedResultRules = _rules.ExpectedResultRules();

            View.NameValue = Model.Name;
            View.ExpectedResultValue = Model.ExpectedResult;
            View.ExpectedResultTypeValue = Model.ExpectedResultType;
            View.ComparisonValue = Model.Comparison;
        }
    }
}