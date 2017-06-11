using System;
using Shuttle.Abacus.Domain;
using Shuttle.Abacus.Invariants.Values;
using Shuttle.Abacus.Localisation;
using Shuttle.Abacus.Shell.Core.Presentation;
using Shuttle.Abacus.Shell.Models;
using Shuttle.Core.Infrastructure;

namespace Shuttle.Abacus.Shell.UI.Formula.FormulaOperation
{
    public class FormulaOperationPresenter : Presenter<IFormulaOperationView, ManageFormulaOperationsModel>, IFormulaOperationPresenter
    {
        private readonly IValueTypeValidatorProvider valueTypeValidatorProvider;

        public FormulaOperationPresenter(IFormulaOperationView view, IValueTypeValidatorProvider valueTypeValidatorProvider)
            : base(view)
        {
            this.valueTypeValidatorProvider = valueTypeValidatorProvider;
            Text = "Operation Details";
            Image = Resources.Image_FormulaOperation;
        }

        public bool CanAddOperation()
        {
            if (!View.HasOperation)
            {
                View.ShowOperationTypeError();

                return false;
            }

            if (!View.HasValueSource)
            {
                View.ShowValueSourceError();

                return false;
            }

            switch (View.ValueSourceValue.ToLower())
            {
                case "decimal":
                    {
                        if (!View.HasValueSelection)
                        {
                            View.ShowValueSelectionError("Please enter a value.");

                            return false;
                        }

                        var result =
                            valueTypeValidatorProvider.Get(DecimalValueTypeValidator.TypeName)
                                .Validate(View.ValueSelectionValue);

                        if (!result.OK)
                        {
                            View.ShowValueSelectionError(result.ToString());

                            return false;
                        }

                        break;
                    }
                default:
                    {
                        if (!View.HasValueSelection)
                        {
                            View.ShowValueSelectionError("Please make a selection.");

                            return false;
                        }

                        break;
                    }
            }

            return true;
        }

        public override void OnInitialize()
        {
            base.OnInitialize();

            if (Model == null)
            {
                throw new NullDependencyException("Model");
            }

            if (Model.FormulaOperations == null)
            {
                return;
            }

            foreach (var formulaOperation in Model.FormulaOperations)
            {
                View.AddOperation(formulaOperation.Operation, formulaOperation.ValueSource, formulaOperation.ValueSelection);
            }
        }

        public void ValueSourceChanged()
        {
            ValueSourceType type;

            if (!Enum.TryParse(View.ValueSourceValue, out type))
            {
                return;
            }

            View.ClearValueSelection();

            switch (type)
            {
                case ValueSourceType.Argument:
                    {
                        View.PopulateArguments(Model.Arguments);

                        break;
                    }
                case ValueSourceType.Constant:
                    {
                        break;
                    }
                case ValueSourceType.Matrix:
                    {
                        View.PopulateMatrixes(Model.Matrixes);

                        break;
                    }
                case ValueSourceType.Formula:
                case ValueSourceType.RunningTotal:
                    {
                        View.PopulateFormulas(Model.Formulas);

                        break;
                    }
                default:
                    {
                        throw new InvalidOperationException();
                    }
            }
        }

    }
}
