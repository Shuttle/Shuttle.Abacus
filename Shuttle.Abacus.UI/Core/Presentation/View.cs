using System;
using System.ComponentModel;
using System.Windows.Forms;
using Shuttle.Abacus.UI.Core.Binding;
using Shuttle.Abacus.UI.Core.Validation;

namespace Shuttle.Abacus.UI.Core.Presentation
{
    public class View<TPresenter> : UserControl, IView where TPresenter : class, IPresenter
    {
        private readonly IViewValidationManager viewValidationManager;
        private IContainer components;
        private ErrorProvider errorProvider;
        private IPresenter ipresenter;
        private IViewValidator viewValidator;

        public IBinderProvider BinderProvider { get; set; }

        public View()
        {
            InitializeComponent();

            viewValidationManager = new ViewValidationManager(errorProvider);
        }

        protected void ClearError(Control control)
        {
            errorProvider.SetError(control, string.Empty);
        }

        protected void SetError(Control control, string message)
        {
            errorProvider.SetError(control, message);
        }

        public TPresenter Presenter
        {
            get
            {
                var result = IPresenter as TPresenter;

                if (result == null)
                {
                    throw new InvalidCastException(string.Format(Resources.NullSafeCasting,
                                                                 IPresenter.GetType().FullName,
                                                                 typeof (TPresenter).FullName));
                }

                return result;
            }
        }

        protected IViewValidator ViewValidator
        {
            get
            {
                if (viewValidator == null)
                {
                    throw new NullReferenceException(string.Format(Resources.NullReferenceException,
                                                                   "View.ViewValidator"));
                }

                return viewValidator;
            }
        }

        public IPresenter IPresenter
        {
            get
            {
                if (ipresenter == null)
                {
                    throw new NullReferenceException(string.Format(Resources.NullReferenceException, "View.IPresenter"));
                }

                return ipresenter;
            }
        }

        public IViewValidationManager ViewValidationManager
        {
            get { return viewValidationManager; }
        }

        public bool IsValid
        {
            get { return ViewValidator.IsValid; }
        }

        public void AttachPresenter(IPresenter presenter)
        {
            Guard.AgainstNull(presenter, "presenter");

            ipresenter = presenter;
        }

        public void AttachViewValidator(IViewValidator validator)
        {
            viewValidator = validator;
        }

        public void ValidateView()
        {
            ViewValidator.ValidateView();
        }

        public bool Confirmed(string message)
        {
            return
                MessageBox.Show(message, "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                                MessageBoxDefaultButton.Button2) == DialogResult.Yes;
        }

        public void ShowView()
        {
            Presenter.Show();
        }

        protected override sealed void OnLoad(System.EventArgs e)
        {
            base.OnLoad(e);

            if (DesignMode)
            {
                return;
            }

            Presenter.ViewReady();

            ValidateView();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }

            if (ipresenter != null)
            {
                IPresenter.Dispose();

                ipresenter = null;
            }

            base.Dispose(disposing);
        }

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            ((System.ComponentModel.ISupportInitialize) (this.errorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // errorProvider
            // 
            this.errorProvider.ContainerControl = this;
            // 
            // View
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "View";
            ((System.ComponentModel.ISupportInitialize) (this.errorProvider)).EndInit();
            this.ResumeLayout(false);
        }

        protected void Invoke(Action action)
        {
            if (InvokeRequired)
            {
                Invoke(new MethodInvoker(action));
            }
            else
            {
                action.Invoke();
            }
        }
    }
}
