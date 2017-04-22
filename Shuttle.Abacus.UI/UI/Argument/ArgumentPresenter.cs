using Shuttle.Abacus.UI.Core.Presentation;
using Shuttle.Abacus.UI.Models;

namespace Shuttle.Abacus.UI.UI.Argument
{
    public class ArgumentPresenter : Presenter<IArgumentView, ArgumentModel>, IArgumentPresenter
    {
        private readonly IArgumentRules argumentRules;

        public ArgumentPresenter(IArgumentView view, IArgumentRules argumentRules) : base(view)
        {
            this.argumentRules = argumentRules;

            Text = "Argument Details";
            Image = Resources.Image_Argument;
        }

        public void ArgumentNameExited()
        {
            WorkItem.Text = string.Format("Argument{0}",
                                          View.ArgumentNameValue.Length > 0 ? " : " + View.ArgumentNameValue : string.Empty);
        }

        public override void OnInitialize()
        {
            base.OnInitialize();

            Guard.AgainstNull(Model, "Model");

            View.ArgumentNameRules = argumentRules.ArgumentNameRules();
            View.AnswerTypeRules = argumentRules.AnswerTypeRules();

            View.PopulateAnswerTypes(Model.AnswerTypes);

            if (Model.ArgumentRow == null)
            {
                return;
            }

            var row = Model.ArgumentRow;

            View.ArgumentNameValue = ArgumentColumns.Name.MapFrom(row);
            View.AnswerTypeValue = ArgumentColumns.AnswerType.MapFrom(row);
        }
    }
}
