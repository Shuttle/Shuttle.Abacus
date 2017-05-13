namespace Shuttle.Abacus.UI.UI.Test.Results
{
    partial class TestResultView
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
            this.ImageList = new System.Windows.Forms.ImageList(this.components);
            this.SplitContainer = new System.Windows.Forms.SplitContainer();
            this.MethodTestListView = new System.Windows.Forms.ListView();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.Tabs = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.CalculationLog = new System.Windows.Forms.TextBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.DisplayTree = new System.Windows.Forms.TreeView();
            this.DisplayList = new System.Windows.Forms.ListView();
            this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader4 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader5 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader6 = new System.Windows.Forms.ColumnHeader();
            this.SplitContainer.Panel1.SuspendLayout();
            this.SplitContainer.Panel2.SuspendLayout();
            this.SplitContainer.SuspendLayout();
            this.Tabs.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // ImageList
            // 
            this.ImageList.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.ImageList.ImageSize = new System.Drawing.Size(16, 16);
            this.ImageList.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // SplitContainer
            // 
            this.SplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SplitContainer.Location = new System.Drawing.Point(0, 0);
            this.SplitContainer.Name = "SplitContainer";
            this.SplitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // SplitContainer.Panel1
            // 
            this.SplitContainer.Panel1.Controls.Add(this.MethodTestListView);
            // 
            // SplitContainer.Panel2
            // 
            this.SplitContainer.Panel2.Controls.Add(this.Tabs);
            this.SplitContainer.Size = new System.Drawing.Size(696, 400);
            this.SplitContainer.SplitterDistance = 105;
            this.SplitContainer.TabIndex = 0;
            // 
            // MethodTestListView
            // 
            this.MethodTestListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
            this.MethodTestListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MethodTestListView.FullRowSelect = true;
            this.MethodTestListView.HideSelection = false;
            this.MethodTestListView.Location = new System.Drawing.Point(0, 0);
            this.MethodTestListView.MultiSelect = false;
            this.MethodTestListView.Name = "MethodTestListView";
            this.MethodTestListView.Size = new System.Drawing.Size(696, 105);
            this.MethodTestListView.SmallImageList = this.ImageList;
            this.MethodTestListView.TabIndex = 1;
            this.MethodTestListView.UseCompatibleStateImageBehavior = false;
            this.MethodTestListView.View = System.Windows.Forms.View.Details;
            this.MethodTestListView.SelectedIndexChanged += new System.EventHandler(this.MethodTestListView_SelectedIndexChanged);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Description";
            this.columnHeader1.Width = 209;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Comment";
            this.columnHeader2.Width = 400;
            // 
            // Tabs
            // 
            this.Tabs.Appearance = System.Windows.Forms.TabAppearance.Buttons;
            this.Tabs.Controls.Add(this.tabPage1);
            this.Tabs.Controls.Add(this.tabPage2);
            this.Tabs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Tabs.Location = new System.Drawing.Point(0, 0);
            this.Tabs.Name = "Tabs";
            this.Tabs.Padding = new System.Drawing.Point(0, 0);
            this.Tabs.SelectedIndex = 0;
            this.Tabs.Size = new System.Drawing.Size(696, 291);
            this.Tabs.TabIndex = 1;
            // 
            // tabPage1
            // 
            this.tabPage1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tabPage1.Controls.Add(this.CalculationLog);
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Size = new System.Drawing.Size(688, 262);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Log";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // CalculationLog
            // 
            this.CalculationLog.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.CalculationLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CalculationLog.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CalculationLog.Location = new System.Drawing.Point(0, 0);
            this.CalculationLog.Margin = new System.Windows.Forms.Padding(0);
            this.CalculationLog.Multiline = true;
            this.CalculationLog.Name = "CalculationLog";
            this.CalculationLog.ReadOnly = true;
            this.CalculationLog.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.CalculationLog.Size = new System.Drawing.Size(686, 260);
            this.CalculationLog.TabIndex = 0;
            this.CalculationLog.WordWrap = false;
            // 
            // tabPage2
            // 
            this.tabPage2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tabPage2.Controls.Add(this.splitContainer1);
            this.tabPage2.Location = new System.Drawing.Point(4, 25);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Size = new System.Drawing.Size(688, 203);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Display";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.DisplayTree);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.DisplayList);
            this.splitContainer1.Size = new System.Drawing.Size(686, 201);
            this.splitContainer1.SplitterDistance = 215;
            this.splitContainer1.TabIndex = 0;
            // 
            // DisplayTree
            // 
            this.DisplayTree.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DisplayTree.Location = new System.Drawing.Point(0, 0);
            this.DisplayTree.Name = "DisplayTree";
            this.DisplayTree.Size = new System.Drawing.Size(215, 201);
            this.DisplayTree.TabIndex = 0;
            this.DisplayTree.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.DisplayTree_AfterSelect);
            // 
            // DisplayList
            // 
            this.DisplayList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader5,
            this.columnHeader6});
            this.DisplayList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DisplayList.FullRowSelect = true;
            this.DisplayList.GridLines = true;
            this.DisplayList.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.DisplayList.HideSelection = false;
            this.DisplayList.Location = new System.Drawing.Point(0, 0);
            this.DisplayList.Name = "DisplayList";
            this.DisplayList.Size = new System.Drawing.Size(467, 201);
            this.DisplayList.TabIndex = 0;
            this.DisplayList.UseCompatibleStateImageBehavior = false;
            this.DisplayList.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Calculation";
            this.columnHeader3.Width = 170;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Argument";
            this.columnHeader4.Width = 110;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "Amount";
            this.columnHeader5.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.columnHeader5.Width = 77;
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "Total";
            this.columnHeader6.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.columnHeader6.Width = 80;
            // 
            // TestResultView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.SplitContainer);
            this.Name = "TestResultView";
            this.Size = new System.Drawing.Size(696, 400);
            this.SplitContainer.Panel1.ResumeLayout(false);
            this.SplitContainer.Panel2.ResumeLayout(false);
            this.SplitContainer.ResumeLayout(false);
            this.Tabs.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        private System.Windows.Forms.ImageList ImageList;
        private System.Windows.Forms.SplitContainer SplitContainer;
        private System.Windows.Forms.ListView MethodTestListView;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.TextBox CalculationLog;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.TabControl Tabs;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TreeView DisplayTree;
        private System.Windows.Forms.ListView DisplayList;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ColumnHeader columnHeader6;
    }
}
