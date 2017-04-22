using System.Data;
using Shuttle.Abacus.DataAccess;
using Shuttle.Abacus.Localisation;
using Shuttle.Abacus.UI.Core.Presentation;
using Shuttle.Abacus.UI.Models;
using Shuttle.Core.Infrastructure;

namespace Shuttle.Abacus.UI.UI.Calculation.GraphNodeArgument
{
    public class GraphNodeArgumentPresenter : Presenter<IGraphNodeArgumentView, ArgumentDisplayModel>,
                                                IGraphNodeArgumentPresenter
    {
        public GraphNodeArgumentPresenter(IGraphNodeArgumentView view) : base(view)
        {
            Text = "Argument Display";
            Image = Resources.Image_ArgumentRestrictedAnswer;
        }

        public bool ArgumentOK()
        {
            if (!View.HasArgument)
            {
                View.ShowArgumentError();

                return false;
            }

            if (!View.HasFormat)
            {
                View.ShowFormatError();

                return false;
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

            View.PopulateFactors(Model.Factors);

            if (!Model.HasGraphNodeArguments)
            {
                return;
            }

            foreach (DataRow row in Model.GraphNodeArguments.Rows)
            {
                View.AddArgument(Model.GetArgumentDTO(GraphNodeArgumentColumns.ArgumentId.MapFrom(row)),
                                 GraphNodeArgumentColumns.Format.MapFrom(row));
            }
        }
    }
}