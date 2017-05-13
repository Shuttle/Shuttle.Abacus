namespace Shuttle.Abacus.UI.UI.Matrix
{
    partial class MatrixView
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
            this.label1 = new System.Windows.Forms.Label();
            this.DecimalTableName = new System.Windows.Forms.TextBox();
            this.ValueGridView = new System.Windows.Forms.DataGridView();
            this.GridViewMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.AddRowMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.RemoveRowMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.MoveRowUpMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MoveRowDownMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.AddColumnMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.RemoveColumnMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.MoveColumnLeftMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MoveColumnRightMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label2 = new System.Windows.Forms.Label();
            this.RowArgument = new System.Windows.Forms.ComboBox();
            this.ColumnArgument = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.ValueGridView)).BeginInit();
            this.GridViewMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Decimal table name";
            // 
            // DecimalTableName
            // 
            this.DecimalTableName.Location = new System.Drawing.Point(12, 28);
            this.DecimalTableName.Name = "DecimalTableName";
            this.DecimalTableName.Size = new System.Drawing.Size(424, 20);
            this.DecimalTableName.TabIndex = 3;
            this.DecimalTableName.Leave += new System.EventHandler(this.DecimalTableName_Leave);
            // 
            // ValueGridView
            // 
            this.ValueGridView.AllowUserToAddRows = false;
            this.ValueGridView.AllowUserToDeleteRows = false;
            this.ValueGridView.AllowUserToResizeColumns = false;
            this.ValueGridView.AllowUserToResizeRows = false;
            this.ValueGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.ValueGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.ValueGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ValueGridView.ColumnHeadersVisible = false;
            this.ValueGridView.ContextMenuStrip = this.GridViewMenu;
            this.ValueGridView.Enabled = false;
            this.ValueGridView.Location = new System.Drawing.Point(0, 156);
            this.ValueGridView.Name = "ValueGridView";
            this.ValueGridView.RowHeadersVisible = false;
            this.ValueGridView.Size = new System.Drawing.Size(476, 236);
            this.ValueGridView.TabIndex = 4;
            // 
            // GridViewMenu
            // 
            this.GridViewMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.AddRowMenuItem,
            this.RemoveRowMenuItem,
            this.toolStripSeparator1,
            this.MoveRowUpMenuItem,
            this.MoveRowDownMenuItem,
            this.toolStripSeparator2,
            this.AddColumnMenuItem,
            this.RemoveColumnMenuItem,
            this.toolStripSeparator3,
            this.MoveColumnLeftMenuItem,
            this.MoveColumnRightMenuItem});
            this.GridViewMenu.Name = "GridViewMenu";
            this.GridViewMenu.Size = new System.Drawing.Size(240, 198);
            // 
            // AddRowMenuItem
            // 
            this.AddRowMenuItem.Name = "AddRowMenuItem";
            this.AddRowMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Down)));
            this.AddRowMenuItem.Size = new System.Drawing.Size(239, 22);
            this.AddRowMenuItem.Text = "Add &Row";
            this.AddRowMenuItem.Click += new System.EventHandler(this.AddRowMenuItem_Click);
            // 
            // RemoveRowMenuItem
            // 
            this.RemoveRowMenuItem.Name = "RemoveRowMenuItem";
            this.RemoveRowMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Up)));
            this.RemoveRowMenuItem.Size = new System.Drawing.Size(239, 22);
            this.RemoveRowMenuItem.Text = "Remove Row";
            this.RemoveRowMenuItem.Click += new System.EventHandler(this.RemoveRowMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(236, 6);
            // 
            // MoveRowUpMenuItem
            // 
            this.MoveRowUpMenuItem.Name = "MoveRowUpMenuItem";
            this.MoveRowUpMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.Up)));
            this.MoveRowUpMenuItem.Size = new System.Drawing.Size(239, 22);
            this.MoveRowUpMenuItem.Text = "Move Row Up";
            this.MoveRowUpMenuItem.Click += new System.EventHandler(this.MoveRowUpMenuItem_Click);
            // 
            // MoveRowDownMenuItem
            // 
            this.MoveRowDownMenuItem.Name = "MoveRowDownMenuItem";
            this.MoveRowDownMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.Down)));
            this.MoveRowDownMenuItem.Size = new System.Drawing.Size(239, 22);
            this.MoveRowDownMenuItem.Text = "Move Row Down";
            this.MoveRowDownMenuItem.Click += new System.EventHandler(this.MoveRowDownMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(236, 6);
            // 
            // AddColumnMenuItem
            // 
            this.AddColumnMenuItem.Name = "AddColumnMenuItem";
            this.AddColumnMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Right)));
            this.AddColumnMenuItem.Size = new System.Drawing.Size(239, 22);
            this.AddColumnMenuItem.Text = "Add &Column";
            this.AddColumnMenuItem.Click += new System.EventHandler(this.AddColumnMenuItem_Click);
            // 
            // RemoveColumnMenuItem
            // 
            this.RemoveColumnMenuItem.Name = "RemoveColumnMenuItem";
            this.RemoveColumnMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Left)));
            this.RemoveColumnMenuItem.Size = new System.Drawing.Size(239, 22);
            this.RemoveColumnMenuItem.Text = "Remove Column";
            this.RemoveColumnMenuItem.Click += new System.EventHandler(this.RemoveColumnMenuItem_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(236, 6);
            // 
            // MoveColumnLeftMenuItem
            // 
            this.MoveColumnLeftMenuItem.Name = "MoveColumnLeftMenuItem";
            this.MoveColumnLeftMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.Left)));
            this.MoveColumnLeftMenuItem.Size = new System.Drawing.Size(239, 22);
            this.MoveColumnLeftMenuItem.Text = "Move Column Left";
            this.MoveColumnLeftMenuItem.Click += new System.EventHandler(this.MoveColumnLeftMenuItem_Click);
            // 
            // MoveColumnRightMenuItem
            // 
            this.MoveColumnRightMenuItem.Name = "MoveColumnRightMenuItem";
            this.MoveColumnRightMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.Right)));
            this.MoveColumnRightMenuItem.Size = new System.Drawing.Size(239, 22);
            this.MoveColumnRightMenuItem.Text = "Move Column Right";
            this.MoveColumnRightMenuItem.Click += new System.EventHandler(this.MoveColumnRightMenuItem_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 56);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(76, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Row argument";
            // 
            // RowArgument
            // 
            this.RowArgument.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.RowArgument.FormattingEnabled = true;
            this.RowArgument.Location = new System.Drawing.Point(12, 72);
            this.RowArgument.Name = "RowArgument";
            this.RowArgument.Size = new System.Drawing.Size(424, 21);
            this.RowArgument.TabIndex = 6;
            this.RowArgument.SelectedIndexChanged += new System.EventHandler(this.RowArgument_SelectedIndexChanged);
            // 
            // ColumnArgument
            // 
            this.ColumnArgument.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ColumnArgument.Enabled = false;
            this.ColumnArgument.FormattingEnabled = true;
            this.ColumnArgument.Location = new System.Drawing.Point(12, 120);
            this.ColumnArgument.Name = "ColumnArgument";
            this.ColumnArgument.Size = new System.Drawing.Size(424, 21);
            this.ColumnArgument.TabIndex = 8;
            this.ColumnArgument.SelectedIndexChanged += new System.EventHandler(this.ColumnArgument_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 104);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(89, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Column argument";
            // 
            // MatrixView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.ColumnArgument);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.RowArgument);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.ValueGridView);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.DecimalTableName);
            this.Name = "MatrixView";
            this.Size = new System.Drawing.Size(477, 396);
            ((System.ComponentModel.ISupportInitialize)(this.ValueGridView)).EndInit();
            this.GridViewMenu.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox DecimalTableName;
        private System.Windows.Forms.DataGridView ValueGridView;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox RowArgument;
        private System.Windows.Forms.ComboBox ColumnArgument;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ContextMenuStrip GridViewMenu;
        private System.Windows.Forms.ToolStripMenuItem AddRowMenuItem;
        private System.Windows.Forms.ToolStripMenuItem AddColumnMenuItem;
        private System.Windows.Forms.ToolStripMenuItem RemoveRowMenuItem;
        private System.Windows.Forms.ToolStripMenuItem RemoveColumnMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem MoveRowUpMenuItem;
        private System.Windows.Forms.ToolStripMenuItem MoveRowDownMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem MoveColumnLeftMenuItem;
        private System.Windows.Forms.ToolStripMenuItem MoveColumnRightMenuItem;
    }
}
