namespace Shuttle.Abacus.UI.UI.FormulaConstraint
{
    partial class FormulaConstraintView
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.ValueSelection = new System.Windows.Forms.ComboBox();
            this.ValueSelectionLabel = new System.Windows.Forms.Label();
            this.Argument = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.Comparison = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.RemoveButton = new System.Windows.Forms.Button();
            this.ConstraintsListView = new System.Windows.Forms.ListView();
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.label1 = new System.Windows.Forms.Label();
            this.ApplyButton = new System.Windows.Forms.Button();
            this.AddButton = new System.Windows.Forms.Button();
            this.MoveDownButton = new System.Windows.Forms.Button();
            this.MoveUpButton = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.ValueSelection);
            this.groupBox1.Controls.Add(this.ValueSelectionLabel);
            this.groupBox1.Controls.Add(this.Argument);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.Comparison);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(8, 273);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(570, 79);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Constraint details";
            // 
            // ValueSelection
            // 
            this.ValueSelection.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ValueSelection.FormattingEnabled = true;
            this.ValueSelection.Location = new System.Drawing.Point(396, 40);
            this.ValueSelection.Name = "ValueSelection";
            this.ValueSelection.Size = new System.Drawing.Size(152, 21);
            this.ValueSelection.TabIndex = 5;
            this.ValueSelection.SelectedIndexChanged += new System.EventHandler(this.Answer_SelectedIndexChanged);
            this.ValueSelection.TextChanged += new System.EventHandler(this.ValueSelection_TextChanged);
            // 
            // ValueSelectionLabel
            // 
            this.ValueSelectionLabel.AutoSize = true;
            this.ValueSelectionLabel.Location = new System.Drawing.Point(396, 24);
            this.ValueSelectionLabel.Name = "ValueSelectionLabel";
            this.ValueSelectionLabel.Size = new System.Drawing.Size(34, 13);
            this.ValueSelectionLabel.TabIndex = 4;
            this.ValueSelectionLabel.Text = "Value";
            // 
            // Argument
            // 
            this.Argument.DisplayMember = "Name";
            this.Argument.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Argument.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.Argument.FormattingEnabled = true;
            this.Argument.Location = new System.Drawing.Point(12, 40);
            this.Argument.Name = "Argument";
            this.Argument.Size = new System.Drawing.Size(212, 21);
            this.Argument.TabIndex = 1;
            this.Argument.ValueMember = "Id";
            this.Argument.SelectedIndexChanged += new System.EventHandler(this.ArgumentName_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 24);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(52, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Argument";
            // 
            // Comparison
            // 
            this.Comparison.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Comparison.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.Comparison.FormattingEnabled = true;
            this.Comparison.Items.AddRange(new object[] {
            "==",
            "!=",
            ">=",
            ">",
            "<=",
            "<",
            "in"});
            this.Comparison.Location = new System.Drawing.Point(248, 40);
            this.Comparison.Name = "Comparison";
            this.Comparison.Size = new System.Drawing.Size(128, 21);
            this.Comparison.TabIndex = 3;
            this.Comparison.SelectedIndexChanged += new System.EventHandler(this.Constraint_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(248, 24);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(62, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Comparison";
            // 
            // RemoveButton
            // 
            this.RemoveButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.RemoveButton.Enabled = false;
            this.RemoveButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.RemoveButton.Location = new System.Drawing.Point(501, 244);
            this.RemoveButton.Name = "RemoveButton";
            this.RemoveButton.Size = new System.Drawing.Size(75, 23);
            this.RemoveButton.TabIndex = 4;
            this.RemoveButton.Text = "&Remove";
            this.RemoveButton.UseVisualStyleBackColor = true;
            this.RemoveButton.Click += new System.EventHandler(this.RemoveButton_Click);
            // 
            // ConstraintsListView
            // 
            this.ConstraintsListView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ConstraintsListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader4,
            this.columnHeader5,
            this.columnHeader6});
            this.ConstraintsListView.FullRowSelect = true;
            this.ConstraintsListView.HideSelection = false;
            this.ConstraintsListView.Location = new System.Drawing.Point(8, 24);
            this.ConstraintsListView.Name = "ConstraintsListView";
            this.ConstraintsListView.Size = new System.Drawing.Size(570, 214);
            this.ConstraintsListView.TabIndex = 1;
            this.ConstraintsListView.UseCompatibleStateImageBehavior = false;
            this.ConstraintsListView.View = System.Windows.Forms.View.Details;
            this.ConstraintsListView.SelectedIndexChanged += new System.EventHandler(this.ConstraintsListView_SelectedIndexChanged);
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Argument";
            this.columnHeader4.Width = 101;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "Constraint";
            this.columnHeader5.Width = 150;
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "Value";
            this.columnHeader6.Width = 166;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Constraints";
            // 
            // ApplyButton
            // 
            this.ApplyButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.ApplyButton.Enabled = false;
            this.ApplyButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.ApplyButton.Location = new System.Drawing.Point(502, 358);
            this.ApplyButton.Name = "ApplyButton";
            this.ApplyButton.Size = new System.Drawing.Size(75, 23);
            this.ApplyButton.TabIndex = 7;
            this.ApplyButton.Text = "A&pply";
            this.ApplyButton.UseVisualStyleBackColor = true;
            this.ApplyButton.Click += new System.EventHandler(this.ApplyButton_Click);
            // 
            // AddButton
            // 
            this.AddButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.AddButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.AddButton.Location = new System.Drawing.Point(418, 358);
            this.AddButton.Name = "AddButton";
            this.AddButton.Size = new System.Drawing.Size(75, 23);
            this.AddButton.TabIndex = 6;
            this.AddButton.Text = "&Add";
            this.AddButton.UseVisualStyleBackColor = true;
            this.AddButton.Click += new System.EventHandler(this.AddButton_Click);
            // 
            // MoveDownButton
            // 
            this.MoveDownButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.MoveDownButton.Enabled = false;
            this.MoveDownButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.MoveDownButton.Location = new System.Drawing.Point(91, 244);
            this.MoveDownButton.Name = "MoveDownButton";
            this.MoveDownButton.Size = new System.Drawing.Size(75, 23);
            this.MoveDownButton.TabIndex = 3;
            this.MoveDownButton.Text = "Move &down";
            this.MoveDownButton.UseVisualStyleBackColor = true;
            this.MoveDownButton.Click += new System.EventHandler(this.MoveDownButton_Click);
            // 
            // MoveUpButton
            // 
            this.MoveUpButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.MoveUpButton.Enabled = false;
            this.MoveUpButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.MoveUpButton.Location = new System.Drawing.Point(7, 244);
            this.MoveUpButton.Name = "MoveUpButton";
            this.MoveUpButton.Size = new System.Drawing.Size(75, 23);
            this.MoveUpButton.TabIndex = 2;
            this.MoveUpButton.Text = "Move &up";
            this.MoveUpButton.UseVisualStyleBackColor = true;
            this.MoveUpButton.Click += new System.EventHandler(this.MoveUpButton_Click);
            // 
            // FormulaConstraintView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.MoveDownButton);
            this.Controls.Add(this.MoveUpButton);
            this.Controls.Add(this.ApplyButton);
            this.Controls.Add(this.AddButton);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.RemoveButton);
            this.Controls.Add(this.ConstraintsListView);
            this.Controls.Add(this.label1);
            this.Name = "FormulaConstraintView";
            this.Size = new System.Drawing.Size(587, 395);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label ValueSelectionLabel;
        private System.Windows.Forms.ComboBox ValueSelection;
        private System.Windows.Forms.ComboBox Comparison;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button RemoveButton;
        private System.Windows.Forms.ListView ConstraintsListView;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.Button ApplyButton;
        private System.Windows.Forms.Button AddButton;
        private System.Windows.Forms.ComboBox Argument;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button MoveDownButton;
        private System.Windows.Forms.Button MoveUpButton;
    }
}
