using Shuttle.Abacus.DataAccess;
using Shuttle.Abacus.Invariants.Interfaces;
using Shuttle.Abacus.Localisation;
using Shuttle.Abacus.UI.Core.Presentation;
using Shuttle.Abacus.UI.Messages.Section;

namespace Shuttle.Abacus.UI.UI.Method
{
    public class MethodPresenter : Presenter<IMethodView>, IMethodPresenter
    {
        private readonly IMethodRules methodRules;

        public MethodPresenter(IMethodView view, IMethodRules methodRules)
            : base(view)
        {
            this.methodRules = methodRules;

            Text = "Method Details";

            Image = Resources.Image_Method;
        }

        public void TitleExited()
        {
            WorkItem.Text = string.Format("Method{0}",
                View.MethodNameValue.Length > 0 ? " : " + View.MethodNameValue : string.Empty);
        }

        public void HandleMessage(EditMethodMessage message)
        {
            View.MethodNameValue = MethodColumns.Name.MapFrom(Model.GetRow());
        }

        public override void OnInitialize()
        {
            base.OnInitialize();

            View.MethodNameRules = methodRules.MethodNameRules();
            methodRules.MethodNameRules();
        }
    }
}