using Shuttle.Abacus.DataAccess;
using Shuttle.Abacus.Domain;
using Shuttle.Abacus.Invariants.Interfaces;
using Shuttle.Abacus.Localisation;
using Shuttle.Abacus.UI.Core.Presentation;
using Shuttle.Abacus.UI.Models;
using Shuttle.Core.Infrastructure;

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

            if (Model.Row == null)
            {
                return;
            }

            var row = Model.Row;

            View.ArgumentNameValue = ArgumentColumns.Name.MapFrom(row);
            View.AnswerTypeValue = ArgumentColumns.AnswerType.MapFrom(row);
        }
    }
}
