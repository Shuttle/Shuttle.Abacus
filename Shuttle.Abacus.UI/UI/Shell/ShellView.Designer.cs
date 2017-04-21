namespace Abacus.UI
{
    partial class ShellView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ShellView));
            this.NavigationMenuStrip = new System.Windows.Forms.MenuStrip();
            this.ShellSplitContainer = new System.Windows.Forms.SplitContainer();
            this.Explorer = new System.Windows.Forms.TreeView();
            this.ExplorerImageList = new System.Windows.Forms.ImageList(this.components);
            this.ShellStatusStrip = new System.Windows.Forms.StatusStrip();
            this.StatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.ShellSplitContainer.Panel1.SuspendLayout();
            this.ShellSplitContainer.SuspendLayout();
            this.ShellStatusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // NavigationMenuStrip
            // 
            this.NavigationMenuStrip.Location = new System.Drawing.Point(0, 0);
            this.NavigationMenuStrip.Name = "NavigationMenuStrip";
            this.NavigationMenuStrip.Size = new System.Drawing.Size(614, 24);
            this.NavigationMenuStrip.TabIndex = 0;
            // 
            // ShellSplitContainer
            // 
            this.ShellSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ShellSplitContainer.Location = new System.Drawing.Point(0, 24);
            this.ShellSplitContainer.Name = "ShellSplitContainer";
            // 
            // ShellSplitContainer.Panel1
            // 
            this.ShellSplitContainer.Panel1.Controls.Add(this.Explorer);
            this.ShellSplitContainer.Size = new System.Drawing.Size(614, 218);
            this.ShellSplitContainer.SplitterDistance = 204;
            this.ShellSplitContainer.TabIndex = 1;
            // 
            // Explorer
            // 
            this.Explorer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Explorer.HideSelection = false;
            this.Explorer.ImageIndex = 0;
            this.Explorer.ImageList = this.ExplorerImageList;
            this.Explorer.Location = new System.Drawing.Point(0, 0);
            this.Explorer.Name = "Explorer";
            this.Explorer.SelectedImageIndex = 0;
            this.Explorer.Size = new System.Drawing.Size(204, 218);
            this.Explorer.TabIndex = 0;
            // 
            // ExplorerImageList
            // 
            this.ExplorerImageList.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.ExplorerImageList.ImageSize = new System.Drawing.Size(16, 16);
            this.ExplorerImageList.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // ShellStatusStrip
            // 
            this.ShellStatusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.StatusLabel});
            this.ShellStatusStrip.Location = new System.Drawing.Point(0, 242);
            this.ShellStatusStrip.Name = "ShellStatusStrip";
            this.ShellStatusStrip.Size = new System.Drawing.Size(614, 22);
            this.ShellStatusStrip.TabIndex = 2;
            this.ShellStatusStrip.Text = "statusStrip1";
            // 
            // StatusLabel
            // 
            this.StatusLabel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.StatusLabel.Name = "StatusLabel";
            this.StatusLabel.Size = new System.Drawing.Size(599, 17);
            this.StatusLabel.Spring = true;
            this.StatusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ShellView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(614, 264);
            this.Controls.Add(this.ShellSplitContainer);
            this.Controls.Add(this.NavigationMenuStrip);
            this.Controls.Add(this.ShellStatusStrip);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.NavigationMenuStrip;
            this.Name = "ShellView";
            this.Text = "Abacus";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.ShellSplitContainer.Panel1.ResumeLayout(false);
            this.ShellSplitContainer.ResumeLayout(false);
            this.ShellStatusStrip.ResumeLayout(false);
            this.ShellStatusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private System.Windows.Forms.MenuStrip NavigationMenuStrip;
        private System.Windows.Forms.SplitContainer ShellSplitContainer;
        private System.Windows.Forms.StatusStrip ShellStatusStrip;
        private System.Windows.Forms.ToolStripStatusLabel StatusLabel;
        private System.Windows.Forms.TreeView Explorer;
        private System.Windows.Forms.ImageList ExplorerImageList;
    }
}

