using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Shuttle.Abacus.DataAccess;
using Shuttle.Abacus.Infrastructure;
using Shuttle.Abacus.Invariants.Interfaces;
using Shuttle.Abacus.Invariants.Values;
using Shuttle.Abacus.Localisation;
using Shuttle.Abacus.Shell.Core.Presentation;
using Shuttle.Abacus.Shell.Models;
using Shuttle.Core.Data;
using Shuttle.Core.Infrastructure;

namespace Shuttle.Abacus.Shell.UI.Matrix
{
    public class MatrixPresenter : Presenter<IMatrixView, MatrixModel>, IMatrixPresenter
    {
        private readonly IMatrixRules _matrixRules;
        private readonly IValueTypeValidatorProvider _valueTypeValidatorProvider;
        private readonly IDatabaseContextFactory _databaseContextFactory;
        private readonly IArgumentQuery _argumentQuery;
        private bool _previousColumnArgumentWasText;
        private bool _previousRowArgumentWasText;

        public MatrixPresenter(IMatrixView view, IMatrixRules matrixRules, IValueTypeValidatorProvider valueTypeValidatorProvider,
            IDatabaseContextFactory databaseContextFactory, IArgumentQuery argumentQuery)
            : base(view)
        {
            Guard.AgainstNull(matrixRules, "matrixRules");
            Guard.AgainstNull(valueTypeValidatorProvider, "valueTypeValidatorProvider");
            Guard.AgainstNull(databaseContextFactory, "databaseContextFactory");
            Guard.AgainstNull(argumentQuery, "argumentQuery");

            _matrixRules = matrixRules;
            _valueTypeValidatorProvider = valueTypeValidatorProvider;
            _databaseContextFactory = databaseContextFactory;
            _argumentQuery = argumentQuery;

            Text = "Matrix";
            Image = Resources.Image_Matrix;
        }

        public void MatrixNameExited()
        {
            WorkItem.Text = string.Format("Matrix{0}",
                View.MatrixNameValue.Length > 0
                    ? " : " + View.MatrixNameValue
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
        }

        public void ColumnArgumentChanged()
        {
            if (View.HasColumnArgument)
            {
                View.ApplyColumnArgument();
            }
            else
            {
                View.RowValuesOnly();
            }
        }

        public bool IsDecimal(string value)
        {
            decimal dec;

            return decimal.TryParse(value, out dec);
        }

        public bool IsValidAnswer(ArgumentModel model, object value)
        {
            if (string.IsNullOrEmpty(model.ValueType))
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
                _valueTypeValidatorProvider.Get(model.ValueType)
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
                View.PopulateArguments(_argumentQuery.All().Map(row => new ArgumentModel(row)));

                View.MatrixNameValue = Model.Name;

                if (string.IsNullOrEmpty(Model.RowArgumentName))
                {
                    return;
                }

                var rowArgumentRow = _argumentQuery.Get(Model.RowArgumentName);

                if (rowArgumentRow == null)
                {
                    return;
                }

                View.RowArgumentValue = ArgumentColumns.Name.MapFrom(rowArgumentRow);

                if (string.IsNullOrEmpty(Model.ColumnArgumentName))
                {
                    return;
                }

                var columnArumentRow = _argumentQuery.Get(Model.ColumnArgumentName);

                if (columnArumentRow != null)
                {
                    View.ColumnArgumentValue = ArgumentColumns.Name.MapFrom(columnArumentRow);
                }

                //foreach (MatrixElementModel row in Model.Elements)
                //{
                //    View.AddElement(
                //        MatrixColumns.ElementColumns.ColumnIndex.MapFrom(row),
                //        MatrixColumns.ElementColumns.RowIndex.MapFrom(row),
                //        MatrixColumns.ElementColumns.DecimalValue.MapFrom(row)
                //    );
                //}
            }
        }
    }
}