using System;
using System.Drawing;
using System.Windows.Forms;
using Abacus.Infrastructure;
using Abacus.Localisation;

namespace Abacus.UI
{
    public partial class ModalWorkspaceView : GenericModalWorkspaceView, IModalWorkspaceView
    {
        private readonly IImageService imageService;
        private ModalWorkspaceForm form;

        public ModalWorkspaceView(IImageService imageService)
        {
            InitializeComponent();

            this.imageService = imageService;
        }

        public void Add(IWorkItem workItem)
        {
            form = new ModalWorkspaceForm(Presenter);

            var control = workItem.WorkItemPresenter.IView as UserControl;

            if (control == null)
            {
                throw new NotSupportedException(string.Format(Resources.IViewNotAControl,
                                                              workItem.WorkItemPresenter.IView.GetType().FullName));
            }

            SetText(workItem.Text);

            form.ClientSize = new Size(control.Width, control.Height);

            control.Dock = DockStyle.Fill;

            control.Show();

            Controls.Add(control);

            Dock = DockStyle.Fill;

            form.Controls.Add(this);

            form.Icon = imageService.IconFrom(workItem.Image);

            form.ShowDialog();
        }

        public void Close()
        {
            Invoke(() => form.Close());
        }

        public void SetText(string text)
        {
            form.Text = text;
        }
    }

    public class GenericModalWorkspaceView : View<IModalWorkspacePresenter>
    {
    }
}
