using System;
using System.Collections.Generic;
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

namespace Shuttle.Abacus.UI.UI.DecimalTable
{
    public class DecimalTablePresenter : Presenter<IDecimalTableView, DecimalTableModel>, IDecimalTablePresenter
    {
        private readonly IArgumentQuery _argumentQuery;
        private readonly IConstraintTypeQuery _constraintTypeQuery;

        private readonly IDatabaseContextFactory _databaseContextFactory;
        private readonly IDecimalTableQuery _decimalTableQuery;
        private readonly IDecimalTableRules _decimalTableRules;
        private readonly IValueTypeValidatorProvider _valueTypeValidatorProvider;
        private IEnumerable<ConstraintTypeModel> _constraintTypes = new List<ConstraintTypeModel>();
        private bool _previousColumnArgumentWasText;
        private bool _previousRowArgumentWasText;

        public DecimalTablePresenter(IDatabaseContextFactory databaseContextFactory, IArgumentQuery argumentQuery,
            IDecimalTableQuery decimalTableQuery, IConstraintTypeQuery constraintTypeQuery, IDecimalTableView view,
            IDecimalTableRules decimalTableRules,
            IValueTypeValidatorProvider valueTypeValidatorProvider)
            : base(view)
        {
            Guard.AgainstNull(databaseContextFactory, "databaseContextFactory");
            Guard.AgainstNull(argumentQuery, "argumentQuery");
            Guard.AgainstNull(decimalTableQuery, "decimalTableQuery");
            Guard.AgainstNull(constraintTypeQuery, "constraintTypeQuery");
            Guard.AgainstNull(decimalTableRules, "decimalTableRules");
            Guard.AgainstNull(valueTypeValidatorProvider, "valueTypeValidatorProvider");

            _databaseContextFactory = databaseContextFactory;
            _argumentQuery = argumentQuery;
            _decimalTableQuery = decimalTableQuery;
            _constraintTypeQuery = constraintTypeQuery;
            _decimalTableRules = decimalTableRules;
            _valueTypeValidatorProvider = valueTypeValidatorProvider;

            Text = "Decimal Table";
            Image = Resources.Image_DecimalTable;
        }

        public void DecimalTableNameExited()
        {
            WorkItem.Text = string.Format("Decimal Table{0}",
                View.DecimalTableNameValue.Length > 0
                    ? " : " + View.DecimalTableNameValue
                    : string.Empty);
        }

        public void RowArgumentChanged()
        {
            if (!View.GridInitialized)
            {
                View.InitializeGrid();
            }

            View.EnableColumnArgument();

            var argumentModel = View.RowArgumentModel;

            IEnumerable<string> answers;

            using (_databaseContextFactory.Create())
            {
                answers =
                    _argumentQuery.Answers(argumentModel.Id)
                        .Map(row => ArgumentColumns.RestrictedAnswerColumns.Answer.MapFrom(row))
                        .ToList();
            }

            if (argumentModel.IsText())
            {
                if (argumentModel.IsText() || answers.Any())
                {
                    View.EnableRowAnswerSelection(answers);

                    _previousRowArgumentWasText = false;
                }
                else
                {
                    if (!_previousRowArgumentWasText)
                    {
                        View.EnableRowAnswerEntry();
                    }

                    _previousRowArgumentWasText = true;
                }

                View.ShowRowAnswerCatalogConstraints();
            }
            else
            {
                if (!_previousRowArgumentWasText)
                {
                    View.EnableRowAnswerEntry();

                    _previousRowArgumentWasText = true;
                }

                View.ShowRowAllConstraints();
            }
        }

        public void ColumnArgumentChanged()
        {
            if (View.HasColumnArgument)
            {
                View.ApplyColumnArgument();

                var argumentModel = View.ColumnArgumentModel;

                var answers = ColumnAnswers().ToList();

                if (argumentModel.IsText() || answers.Any())
                {
                    if (answers.Any())
                    {
                        View.EnableColumnAnswerSelection(answers);

                        _previousColumnArgumentWasText = false;
                    }
                    else
                    {
                        if (!_previousColumnArgumentWasText)
                        {
                            View.EnableColumnAnswerEntry();
                        }

                        _previousColumnArgumentWasText = true;
                    }

                    View.ShowColumnAnswerCatalogConstraints();
                }
                else
                {
                    if (!_previousColumnArgumentWasText)
                    {
                        View.EnableColumnAnswerEntry();

                        _previousColumnArgumentWasText = true;
                    }

                    View.ShowColumnAllConstraints();
                }
            }
            else
            {
                View.RowFactorsOnly();
            }
        }

        public bool IsDecimal(string value)
        {
            decimal dec;

            return decimal.TryParse(value, out dec);
        }

        public bool IsValidAnswer(ArgumentModel model, object value)
        {
            if (string.IsNullOrEmpty(model.AnswerType))
            {
                return true;
            }

            if (!HasValidArgumentAnswer(model, Convert.ToString(value)))
            {
                return false;
            }

            if (model.IsText())
            {
                return !string.IsNullOrEmpty(Convert.ToString(value));
            }

            return
                _valueTypeValidatorProvider.Get(model.AnswerType)
                    .Validate(Convert.ToString(value))
                    .OK;
        }

        public void ShowInvalidDecimalTableMessage()
        {
            MessageBus.Publish(
                Result.Create().AddFailureMessage("The decimal table has invalid values.  Please investigate."));
        }

        public IEnumerable<string> ColumnAnswers()
        {
            return ArgumentAnswers(View.ColumnArgumentModel.Id);
        }

        public IEnumerable<string> RowAnswers()
        {
            return ArgumentAnswers(View.RowArgumentModel.Id);
        }

        public IEnumerable<ConstraintTypeModel> ConstraintTypes
        {
            get { return _constraintTypes; }
        }

        private IEnumerable<string> ArgumentAnswers(Guid id)
        {
            using (_databaseContextFactory.Create())
            {
                return
                    _argumentQuery.Answers(id)
                        .Map(row => ArgumentColumns.RestrictedAnswerColumns.Answer.MapFrom(row));
            }
        }

        private bool HasValidArgumentAnswer(ArgumentModel model, string value)
        {
            var answers = ArgumentAnswers(model.Id).ToList();

            if (!answers.Any())
            {
                return true;
            }

            foreach (var answer in answers)
            {
                if (answer.Equals(value, StringComparison.InvariantCultureIgnoreCase))
                {
                    return true;
                }
            }

            return false;
        }

        public override void OnInitialize()
        {
            base.OnInitialize();

            Guard.AgainstNull(Model, "Model");

            View.DecimalTableNameRules = _decimalTableRules.DecimalTableNameRules();
            View.RowArgumentRules = _decimalTableRules.RowArgumentRules();

            using (_databaseContextFactory.Create())
            {
                _constraintTypes = _constraintTypeQuery.All().Map(row => new ConstraintTypeModel(row));

                View.PopulateArguments(_argumentQuery.All().Map(row => new ArgumentModel(row)));

                View.DecimalTableNameValue = Model.Name;

                var rowArgumentRow = _argumentQuery.Get(Model.RowArgumentId);

                if (rowArgumentRow == null)
                {
                    return;
                }

                View.RowArgumentValue = ArgumentColumns.Name.MapFrom(rowArgumentRow);

                var columnArumentRow = _argumentQuery.Get(Model.ColumnArgumentId);

                if (columnArumentRow != null)
                {
                    View.ColumnArgumentValue = ArgumentColumns.Name.MapFrom(columnArumentRow);
                }

                foreach (DataRow row in _decimalTableQuery.GetValues(Model.Id).Rows)
                {
                    View.AddDecimalValue(
                        DecimalValueColumns.ColumnIndex.MapFrom(row),
                        DecimalValueColumns.RowIndex.MapFrom(row),
                        DecimalValueColumns.DecimalValue.MapFrom(row),
                        ConstraintColumns.Name.MapFrom(row),
                        ConstraintColumns.ArgumentName.MapFrom(row),
                        ConstraintColumns.Answer.MapFrom(row)
                    );
                }
            }
        }
    }
}