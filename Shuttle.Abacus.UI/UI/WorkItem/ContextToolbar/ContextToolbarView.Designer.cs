namespace Abacus.UI
{
    partial class ContextToolbarView
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
            this.components = new System.ComponentModel.Container();
            this.ContextToolStrip = new System.Windows.Forms.ToolStrip();
            this.SplitContainer = new System.Windows.Forms.SplitContainer();
            this.PresenterListView = new System.Windows.Forms.ListView();
            this.PresenterColumnHeader = new System.Windows.Forms.ColumnHeader();
            this.PresenterImageList = new System.Windows.Forms.ImageList(this.components);
            this.SplitContainer.Panel2.SuspendLayout();
            this.SplitContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // ContextToolStrip
            // 
            this.ContextToolStrip.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.ContextToolStrip.Location = new System.Drawing.Point(0, 440);
            this.ContextToolStrip.Name = "ContextToolStrip";
            this.ContextToolStrip.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ContextToolStrip.Size = new System.Drawing.Size(622, 25);
            this.ContextToolStrip.TabIndex = 0;
            // 
            // SplitContainer
            // 
            this.SplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SplitContainer.IsSplitterFixed = true;
            this.SplitContainer.Location = new System.Drawing.Point(0, 0);
            this.SplitContainer.Name = "SplitContainer";
            // 
            // SplitContainer.Panel1
            // 
            this.SplitContainer.Panel1.BackColor = System.Drawing.SystemColors.Control;
            // 
            // SplitContainer.Panel2
            // 
            this.SplitContainer.Panel2.Controls.Add(this.PresenterListView);
            this.SplitContainer.Size = new System.Drawing.Size(622, 440);
            this.SplitContainer.SplitterDistance = 509;
            this.SplitContainer.TabIndex = 1;
            // 
            // PresenterListView
            // 
            this.PresenterListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.PresenterColumnHeader});
            this.PresenterListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PresenterListView.FullRowSelect = true;
            this.PresenterListView.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.PresenterListView.HideSelection = false;
            this.PresenterListView.Location = new System.Drawing.Point(0, 0);
            this.PresenterListView.MultiSelect = false;
            this.PresenterListView.Name = "PresenterListView";
            this.PresenterListView.Size = new System.Drawing.Size(109, 440);
            this.PresenterListView.SmallImageList = this.PresenterImageList;
            this.PresenterListView.TabIndex = 0;
            this.PresenterListView.UseCompatibleStateImageBehavior = false;
            this.PresenterListView.View = System.Windows.Forms.View.Details;
            this.PresenterListView.SelectedIndexChanged += new System.EventHandler(this.PresenterListView_SelectedIndexChanged);
            // 
            // PresenterColumnHeader
            // 
            this.PresenterColumnHeader.Text = "Presenter";
            // 
            // PresenterImageList
            // 
            this.PresenterImageList.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.PresenterImageList.ImageSize = new System.Drawing.Size(16, 16);
            this.PresenterImageList.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // ContextToolbarView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.SplitContainer);
            this.Controls.Add(this.ContextToolStrip);
            this.Name = "ContextToolbarView";
            this.Size = new System.Drawing.Size(622, 465);
            this.Resize += new System.EventHandler(this.ContextToolbarView_Resize);
            this.SplitContainer.Panel2.ResumeLayout(false);
            this.SplitContainer.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private System.Windows.Forms.ToolStrip ContextToolStrip;
        private System.Windows.Forms.SplitContainer SplitContainer;
        private System.Windows.Forms.ListView PresenterListView;
        private System.Windows.Forms.ColumnHeader PresenterColumnHeader;
        private System.Windows.Forms.ImageList PresenterImageList;
    }
}
