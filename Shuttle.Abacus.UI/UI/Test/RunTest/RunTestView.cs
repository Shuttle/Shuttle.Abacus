using Shuttle.Abacus.Shell.Core.Presentation;

namespace Shuttle.Abacus.Shell.UI.Test.RunTest
{
    public partial class RunTestView : GenericRunTestView, IRunTestView
    {
        public RunTestView()
        {
            InitializeComponent();
        }
    }

    public class GenericRunTestView : View<IRunTestPresenter>
    {
    }
}