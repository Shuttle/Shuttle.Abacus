using System;
using System.Windows.Forms;

namespace Abacus.UI
{
    public partial class ModalWorkspaceForm : Form
    {
        private readonly IModalWorkspacePresenter presenter;

        public ModalWorkspaceForm(IModalWorkspacePresenter presenter)
        {
            this.presenter = presenter;

            InitializeComponent();

            DefaultAcceptButton.Left = -300;
            DefaultCancelButton.Left = -300;

            DefaultAcceptButton.Click += AcceptButtonClicked;
            DefaultCancelButton.Click += CancelButtonClicked;
        }

        private void CancelButtonClicked(object sender, EventArgs e)
        {
            presenter.ViewCancelled();
        }

        private void AcceptButtonClicked(object sender, EventArgs e)
        {
            presenter.ViewAccepted();
        }
    }
}
