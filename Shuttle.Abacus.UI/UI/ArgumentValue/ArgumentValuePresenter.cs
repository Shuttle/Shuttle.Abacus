using Shuttle.Abacus.Invariants.Interfaces;
using Shuttle.Abacus.Localisation;
using Shuttle.Abacus.Shell.Core.Presentation;
using Shuttle.Abacus.Shell.Models;
using Shuttle.Core.Infrastructure;

namespace Shuttle.Abacus.Shell.UI.ArgumentValue
{
    public class ArgumentValuePresenter : Presenter<IArgumentValueView, ArgumentValueModel>,
                                                IArgumentValuePresenter
    {
        private readonly IArgumentValueRules _argumentValueRules;

        public ArgumentValuePresenter(IArgumentValueView view, IArgumentValueRules argumentValueRules) : base(view)
        {
            Guard.AgainstNull(argumentValueRules, "argumentValueRules");

            _argumentValueRules = argumentValueRules;

            Text = "Values";
            Image = Resources.Image_ArgumentValue;
        }

        public override void OnInitialize()
        {
            base.OnInitialize();

            Guard.AgainstNull(Model, "Model");

            View.ValueRules = _argumentValueRules.ValueRules();

            View.ValueValue = Model.Value;
        }
    }
}
