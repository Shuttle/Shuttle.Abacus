using System;
using System.Collections.Generic;
using Shuttle.Abacus.Invariants.Core;
using Shuttle.Abacus.Invariants.Interfaces;
using Shuttle.Abacus.Shell.Core.Presentation;
using Shuttle.Core.Infrastructure;

namespace Shuttle.Abacus.Shell.UI.Test
{
    public partial class TestView : GenericTestView, ITestView
    {
        private readonly ITestRules _testRules;
        private readonly IValueTypeRules _valueTypeRules;

        public TestView(ITestRules testRules, IValueTypeRules valueTypeRules)
        {
            Guard.AgainstNull(testRules, "testRules");
            Guard.AgainstNull(valueTypeRules, "valueTypeRules");

            _testRules = testRules;
            _valueTypeRules = valueTypeRules;

            InitializeComponent();
        }

        public string NameValue
        {
            get { return TestName.Text; }
            set { TestName.Text = value; }
        }

        public string ExpectedResultValue
        {
            get { return ExpectedResult.Text; }
            set { ExpectedResult.Text = value; }
        }

        public string ExpectedResultTypeValue
        {
            get { return (string) ExpectedResultType.SelectedItem; }
            set { ExpectedResultType.SelectedItem = value; }
        }

        public string ComparisonValue {
            get { return (string)Comparison.SelectedItem; }
            set { Comparison.SelectedItem = value; }
        }

        public string FormulaNameValue
        {
            get { return FormulaName.Text; }
            set { FormulaName.Text = value; }
        }

        public void PopulateFormulas(IEnumerable<string> formulas)
        {
            Guard.AgainstNull(formulas, "formulas");

            foreach (var formula in formulas)
            {
                FormulaName.Items.Add(formula);
            }
        }

        private void ExpectedResultType_SelectedIndexChanged(object sender, EventArgs e)
        {
            ViewValidator.Control(ExpectedResult).ShouldSatisfy(_valueTypeRules.For(ExpectedResultTypeValue));
        }

        public void OnReady()
        {
            ViewValidator.Control(TestName).ShouldSatisfy(_testRules.NameRules());
            ViewValidator.Control(FormulaName).ShouldSatisfy(_testRules.FormulaNameRules());
            ViewValidator.Control(Comparison).ShouldSatisfy(RuleCollection.Required());
            ViewValidator.Control(ExpectedResultType).ShouldSatisfy(RuleCollection.Required());
        }
    }

    public class GenericTestView : View<ITestPresenter>
    {
    }
}