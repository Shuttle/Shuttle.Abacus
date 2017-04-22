using Shuttle.Abacus.UI.Core.Presentation;

namespace Shuttle.Abacus.UI.UI.Limit
{
    public class LimitPresenter : Presenter<ILimitView>, ILimitPresenter
    {
        private readonly ILimitRules limitRules;

        public LimitPresenter(ILimitView view, ILimitRules limitRules) : base(view)
        {
            this.limitRules = limitRules;
            Text = "Limit Details";
            Image = Resources.Image_Limit;
        }

        public void LimitNameExited()
        {
            WorkItem.Text = string.Format("Limit{0}",
                                          View.LimitNameValue.Length > 0
                                              ? " : " + View.LimitNameValue
                                              : string.Empty);
        }

        public override void OnInitialize()
        {
            base.OnInitialize();

            View.LimitNameRules = limitRules.LimitNameRules();
            View.TypeRules = limitRules.TypeRules();

            if (Model == null)
            {
                return;
            }

            View.LimitNameValue = LimitColumns.Name.MapFrom(Model.Row);
            View.TypeValue = LimitColumns.Type.MapFrom(Model.Row);
        }
    }
}
