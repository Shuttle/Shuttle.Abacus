using System.Collections.Generic;
using System.Data;
using Shuttle.Abacus.DataAccess;
using Shuttle.Abacus.Localisation;
using Shuttle.Abacus.UI.Core.Presentation;

namespace Shuttle.Abacus.UI.UI.Argument.RestrictedAnswer
{
    public class ArgumentValuePresenter : Presenter<IArgumentValueView, IEnumerable<DataRow>>,
                                                IArgumentValuePresenter
    {
        public ArgumentValuePresenter(IArgumentValueView view) : base(view)
        {
            Text = "Values";
            Image = Resources.Image_ArgumentValue;
        }

        public bool IsValueOK()
        {
            var result = true;

            if (!View.HasValue())
            {
                View.ShowValueError("Please enter a value.");

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
                View.AddValue(ArgumentColumns.ValueColumns.Value.MapFrom(row));
            }
        }
    }
}
