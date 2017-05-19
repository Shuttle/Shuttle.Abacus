namespace Shuttle.Abacus.Shell.UI.SystemUser
{
    partial class SystemUserView
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
            this.label1 = new System.Windows.Forms.Label();
            this.LoginName = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(62, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Login name";
            // 
            // LoginName
            // 
            this.LoginName.Location = new System.Drawing.Point(6, 26);
            this.LoginName.Name = "LoginName";
            this.LoginName.Size = new System.Drawing.Size(266, 20);
            this.LoginName.TabIndex = 1;
            this.LoginName.Leave += new System.EventHandler(this.LoginName_Leave);
            // 
            // SystemUserView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.LoginName);
            this.Controls.Add(this.label1);
            this.Name = "SystemUserView";
            this.Size = new System.Drawing.Size(275, 82);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox LoginName;
    }
}
