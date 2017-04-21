using Abacus.Data;
using Abacus.Localisation;
using Abacus.Validation;

namespace Abacus.UI
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
            var table = Model.Table;

            View.MethodNameValue = MethodColumns.Name.MapFrom(table.Rows[0]);
        }

        public override void OnInitialize()
        {
            base.OnInitialize();

            View.MethodNameRules = methodRules.MethodNameRules();
            methodRules.MethodNameRules();
        }
    }
}