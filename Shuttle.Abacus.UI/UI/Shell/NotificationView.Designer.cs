namespace Shuttle.Abacus.Shell.UI.Shell
{
    partial class NotificationView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NotificationView));
            this.ContextToolStrip = new System.Windows.Forms.ToolStrip();
            this.CloseButton = new System.Windows.Forms.ToolStripButton();
            this.NotificationArea = new System.Windows.Forms.RichTextBox();
            this.ContextToolStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // ContextToolStrip
            // 
            this.ContextToolStrip.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.ContextToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.CloseButton});
            this.ContextToolStrip.Location = new System.Drawing.Point(0, 239);
            this.ContextToolStrip.Name = "ContextToolStrip";
            this.ContextToolStrip.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ContextToolStrip.Size = new System.Drawing.Size(502, 25);
            this.ContextToolStrip.TabIndex = 0;
            this.ContextToolStrip.Text = "ContextToolStrip";
            // 
            // CloseButton
            // 
            this.CloseButton.Image = ((System.Drawing.Image)(resources.GetObject("CloseButton.Image")));
            this.CloseButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.CloseButton.Name = "CloseButton";
            this.CloseButton.Size = new System.Drawing.Size(56, 22);
            this.CloseButton.Text = "Close";
            this.CloseButton.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.CloseButton.Click += new System.EventHandler(this.CloseButton_Click);
            // 
            // NotificationArea
            // 
            this.NotificationArea.Dock = System.Windows.Forms.DockStyle.Fill;
            this.NotificationArea.Location = new System.Drawing.Point(0, 0);
            this.NotificationArea.Name = "NotificationArea";
            this.NotificationArea.ReadOnly = true;
            this.NotificationArea.Size = new System.Drawing.Size(502, 239);
            this.NotificationArea.TabIndex = 1;
            this.NotificationArea.Text = "";
            // 
            // NotificationView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(502, 264);
            this.Controls.Add(this.NotificationArea);
            this.Controls.Add(this.ContextToolStrip);
            this.KeyPreview = true;
            this.MinimizeBox = false;
            this.Name = "NotificationView";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Notification";
            this.ContextToolStrip.ResumeLayout(false);
            this.ContextToolStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private System.Windows.Forms.ToolStrip ContextToolStrip;
        private System.Windows.Forms.ToolStripButton CloseButton;
        private System.Windows.Forms.RichTextBox NotificationArea;
    }
}
