namespace Shuttle.Abacus.Shell.UI.Test.Execution
{
    partial class TestExecutionExecutionView
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
            this.Tabs = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.CalculationLog = new System.Windows.Forms.TextBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.DisplayTree = new System.Windows.Forms.TreeView();
            this.DisplayList = new System.Windows.Forms.ListView();
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.MethodTestListView = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Tabs.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.SuspendLayout();
            // 
            // Tabs
            // 
            this.Tabs.Appearance = System.Windows.Forms.TabAppearance.Buttons;
            this.Tabs.Controls.Add(this.tabPage1);
            this.Tabs.Controls.Add(this.tabPage2);
            this.Tabs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Tabs.Location = new System.Drawing.Point(0, 0);
            this.Tabs.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Tabs.Name = "Tabs";
            this.Tabs.Padding = new System.Drawing.Point(0, 0);
            this.Tabs.SelectedIndex = 0;
            this.Tabs.Size = new System.Drawing.Size(623, 253);
            this.Tabs.TabIndex = 2;
            // 
            // tabPage1
            // 
            this.tabPage1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tabPage1.Controls.Add(this.CalculationLog);
            this.tabPage1.Location = new System.Drawing.Point(4, 28);
            this.tabPage1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Size = new System.Drawing.Size(615, 221);
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
            this.CalculationLog.Size = new System.Drawing.Size(613, 219);
            this.CalculationLog.TabIndex = 0;
            this.CalculationLog.WordWrap = false;
            // 
            // tabPage2
            // 
            this.tabPage2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tabPage2.Controls.Add(this.splitContainer1);
            this.tabPage2.Location = new System.Drawing.Point(4, 28);
            this.tabPage2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Size = new System.Drawing.Size(615, 215);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Display";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.DisplayTree);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.DisplayList);
            this.splitContainer1.Size = new System.Drawing.Size(613, 213);
            this.splitContainer1.SplitterDistance = 191;
            this.splitContainer1.SplitterWidth = 5;
            this.splitContainer1.TabIndex = 0;
            // 
            // DisplayTree
            // 
            this.DisplayTree.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DisplayTree.Location = new System.Drawing.Point(0, 0);
            this.DisplayTree.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.DisplayTree.Name = "DisplayTree";
            this.DisplayTree.Size = new System.Drawing.Size(191, 213);
            this.DisplayTree.TabIndex = 0;
            // 
            // DisplayList
            // 
            this.DisplayList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader5});
            this.DisplayList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DisplayList.FullRowSelect = true;
            this.DisplayList.GridLines = true;
            this.DisplayList.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.DisplayList.HideSelection = false;
            this.DisplayList.Location = new System.Drawing.Point(0, 0);
            this.DisplayList.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.DisplayList.Name = "DisplayList";
            this.DisplayList.Size = new System.Drawing.Size(417, 213);
            this.DisplayList.TabIndex = 0;
            this.DisplayList.UseCompatibleStateImageBehavior = false;
            this.DisplayList.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Formula";
            this.columnHeader3.Width = 170;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Argument";
            this.columnHeader4.Width = 110;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "Value";
            this.columnHeader5.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.columnHeader5.Width = 77;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.MethodTestListView);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.Tabs);
            this.splitContainer2.Size = new System.Drawing.Size(623, 392);
            this.splitContainer2.SplitterDistance = 135;
            this.splitContainer2.TabIndex = 3;
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
            this.MethodTestListView.Margin = new System.Windows.Forms.Padding(4);
            this.MethodTestListView.MultiSelect = false;
            this.MethodTestListView.Name = "MethodTestListView";
            this.MethodTestListView.Size = new System.Drawing.Size(623, 135);
            this.MethodTestListView.TabIndex = 2;
            this.MethodTestListView.UseCompatibleStateImageBehavior = false;
            this.MethodTestListView.View = System.Windows.Forms.View.Details;
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
            // TestExecutionView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer2);
            this.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.Name = "TestExecutionExecutionView";
            this.Size = new System.Drawing.Size(623, 392);
            this.Tabs.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        private System.Windows.Forms.TabControl Tabs;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TextBox CalculationLog;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TreeView DisplayTree;
        private System.Windows.Forms.ListView DisplayList;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.ListView MethodTestListView;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
    }
}
