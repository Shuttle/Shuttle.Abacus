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
    public class MatrixPresenter : Presenter<IMatrixView, MatrixModel>, IMatrixPresenter
    {
        private readonly IArgumentQuery _argumentQuery;
        private readonly IConstraintTypeQuery _constraintTypeQuery;

        private readonly IDatabaseContextFactory _databaseContextFactory;
        private readonly IMatrixQuery _matrixQuery;
        private readonly IMatrixRules _matrixRules;
        private readonly IValueTypeValidatorProvider _valueTypeValidatorProvider;
        private IEnumerable<ConstraintTypeModel> _constraintTypes = new List<ConstraintTypeModel>();
        private bool _previousColumnArgumentWasText;
        private bool _previousRowArgumentWasText;

        public MatrixPresenter(IDatabaseContextFactory databaseContextFactory, IArgumentQuery argumentQuery,
            IMatrixQuery matrixQuery, IConstraintTypeQuery constraintTypeQuery, IMatrixView view,
            IMatrixRules matrixRules,
            IValueTypeValidatorProvider valueTypeValidatorProvider)
            : base(view)
        {
            Guard.AgainstNull(databaseContextFactory, "databaseContextFactory");
            Guard.AgainstNull(argumentQuery, "argumentQuery");
            Guard.AgainstNull(matrixQuery, "matrixQuery");
            Guard.AgainstNull(constraintTypeQuery, "constraintTypeQuery");
            Guard.AgainstNull(matrixRules, "matrixRules");
            Guard.AgainstNull(valueTypeValidatorProvider, "valueTypeValidatorProvider");

            _databaseContextFactory = databaseContextFactory;
            _argumentQuery = argumentQuery;
            _matrixQuery = matrixQuery;
            _constraintTypeQuery = constraintTypeQuery;
            _matrixRules = matrixRules;
            _valueTypeValidatorProvider = valueTypeValidatorProvider;

            Text = "Matrix";
            Image = Resources.Image_Matrix;
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
                    _argumentQuery.GetValues(argumentModel.Id)
                        .Map(row => ArgumentColumns.ValueColumns.Value.MapFrom(row))
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
                    _argumentQuery.GetValues(id)
                        .Map(row => ArgumentColumns.ValueColumns.Value.MapFrom(row));
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

            View.DecimalTableNameRules = _matrixRules.DecimalTableNameRules();
            View.RowArgumentRules = _matrixRules.RowArgumentRules();

            using (_databaseContextFactory.Create())
            {
                _constraintTypes = _constraintTypeQuery.All().Map(row => new ConstraintTypeModel(row));

                View.PopulateArguments(_argumentQuery.All().Map(row => new ArgumentModel(row)));

                View.DecimalTableNameValue = Model.Name;

                var rowArgumentRow = _argumentQuery.Get(Model.RowArgumentName);

                if (rowArgumentRow == null)
                {
                    return;
                }

                View.RowArgumentValue = ArgumentColumns.Name.MapFrom(rowArgumentRow);

                var columnArumentRow = _argumentQuery.Get(Model.ColumnArgumentName);

                if (columnArumentRow != null)
                {
                    View.ColumnArgumentValue = ArgumentColumns.Name.MapFrom(columnArumentRow);
                }

                foreach (DataRow row in _matrixQuery.GetValues(Model.Id).Rows)
                {
                    View.AddElement(
                        MatrixColumns.ElementColumns.ColumnIndex.MapFrom(row),
                        MatrixColumns.ElementColumns.RowIndex.MapFrom(row),
                        MatrixColumns.ElementColumns.DecimalValue.MapFrom(row)
                    );
                }
            }
        }
    }
}