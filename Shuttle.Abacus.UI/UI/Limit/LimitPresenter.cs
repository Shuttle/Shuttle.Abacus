using Shuttle.Abacus.DataAccess;
using Shuttle.Abacus.Invariants.Interfaces;
using Shuttle.Abacus.Localisation;
using Shuttle.Abacus.UI.Core.Presentation;
using Shuttle.Abacus.UI.Models;
using Shuttle.Core.Infrastructure;

namespace Shuttle.Abacus.UI.UI.Limit
{
    public class LimitPresenter : Presenter<ILimitView, LimitModel>, ILimitPresenter
    {
        private readonly ILimitRules _limitRules;

        public LimitPresenter(ILimitView view, ILimitRules limitRules) : base(view)
        {
            Guard.AgainstNull(limitRules, "limitRules");

            _limitRules = limitRules;
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

            View.LimitNameRules = _limitRules.LimitNameRules();
            View.TypeRules = _limitRules.TypeRules();

            if (Model == null)
            {
                return;
            }

            View.LimitNameValue = LimitColumns.Name.MapFrom(Model.GetRow());
            View.TypeValue = LimitColumns.Type.MapFrom(Model.GetRow());
        }
    }
}