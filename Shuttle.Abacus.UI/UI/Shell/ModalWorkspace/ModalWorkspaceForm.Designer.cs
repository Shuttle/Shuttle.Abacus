namespace Shuttle.Abacus.Shell.UI.Shell.ModalWorkspace
{
    partial class ModalWorkspaceForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.DefaultCancelButton = new System.Windows.Forms.Button();
            this.DefaultAcceptButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // DefaultCancelButton
            // 
            this.DefaultCancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.DefaultCancelButton.Location = new System.Drawing.Point(12, 41);
            this.DefaultCancelButton.Name = "DefaultCancelButton";
            this.DefaultCancelButton.Size = new System.Drawing.Size(75, 23);
            this.DefaultCancelButton.TabIndex = 6;
            this.DefaultCancelButton.TabStop = false;
            this.DefaultCancelButton.Text = "Cancel";
            this.DefaultCancelButton.UseVisualStyleBackColor = true;
            // 
            // DefaultAcceptButton
            // 
            this.DefaultAcceptButton.Location = new System.Drawing.Point(12, 12);
            this.DefaultAcceptButton.Name = "DefaultAcceptButton";
            this.DefaultAcceptButton.Size = new System.Drawing.Size(75, 23);
            this.DefaultAcceptButton.TabIndex = 5;
            this.DefaultAcceptButton.TabStop = false;
            this.DefaultAcceptButton.Text = "Accept";
            this.DefaultAcceptButton.UseVisualStyleBackColor = true;
            // 
            // ModalWorkspaceForm
            // 
            this.AcceptButton = this.DefaultAcceptButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.DefaultCancelButton;
            this.ClientSize = new System.Drawing.Size(284, 264);
            this.Controls.Add(this.DefaultCancelButton);
            this.Controls.Add(this.DefaultAcceptButton);
            this.Name = "ModalWorkspaceForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ModalWorkspaceForm";
            this.ResumeLayout(false);

        }

        private System.Windows.Forms.Button DefaultCancelButton;
        private System.Windows.Forms.Button DefaultAcceptButton;

    }
}
