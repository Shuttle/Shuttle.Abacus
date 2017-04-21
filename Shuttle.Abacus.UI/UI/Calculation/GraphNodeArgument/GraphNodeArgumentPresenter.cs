using System.Data;
using Abacus.Data;
using Abacus.Infrastructure;
using Abacus.Localisation;

namespace Abacus.UI
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

            Guard.AgainstNullDependency(Model, "Model");

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