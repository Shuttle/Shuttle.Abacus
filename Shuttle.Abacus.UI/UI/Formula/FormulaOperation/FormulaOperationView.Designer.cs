namespace Shuttle.Abacus.Shell.UI.Formula.FormulaOperation
{
    partial class FormulaOperationView
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
            this.MoveUpButton = new System.Windows.Forms.Button();
            this.MoveDownButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.OperationsListView = new System.Windows.Forms.ListView();
            this.RemoveButton = new System.Windows.Forms.Button();
            this.AddButton = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.Operation = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.ValueSource = new System.Windows.Forms.ComboBox();
            this.ValueSelectionLabel = new System.Windows.Forms.Label();
            this.ValueSelection = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.ApplyButton = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // MoveUpButton
            // 
            this.MoveUpButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.MoveUpButton.Enabled = false;
            this.MoveUpButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.MoveUpButton.Location = new System.Drawing.Point(12, 263);
            this.MoveUpButton.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MoveUpButton.Name = "MoveUpButton";
            this.MoveUpButton.Size = new System.Drawing.Size(96, 30);
            this.MoveUpButton.TabIndex = 2;
            this.MoveUpButton.Text = "Move &up";
            this.MoveUpButton.UseVisualStyleBackColor = true;
            this.MoveUpButton.Click += new System.EventHandler(this.MoveUpButton_Click);
            // 
            // MoveDownButton
            // 
            this.MoveDownButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.MoveDownButton.Enabled = false;
            this.MoveDownButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.MoveDownButton.Location = new System.Drawing.Point(119, 263);
            this.MoveDownButton.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MoveDownButton.Name = "MoveDownButton";
            this.MoveDownButton.Size = new System.Drawing.Size(96, 30);
            this.MoveDownButton.TabIndex = 3;
            this.MoveDownButton.Text = "Move &down";
            this.MoveDownButton.UseVisualStyleBackColor = true;
            this.MoveDownButton.Click += new System.EventHandler(this.MoveDownButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 10);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(78, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Operations";
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Type";
            this.columnHeader1.Width = 103;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Value source";
            this.columnHeader2.Width = 147;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Value / Selection";
            this.columnHeader3.Width = 300;
            // 
            // OperationsListView
            // 
            this.OperationsListView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.OperationsListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3});
            this.OperationsListView.FullRowSelect = true;
            this.OperationsListView.HideSelection = false;
            this.OperationsListView.Location = new System.Drawing.Point(11, 30);
            this.OperationsListView.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.OperationsListView.MultiSelect = false;
            this.OperationsListView.Name = "OperationsListView";
            this.OperationsListView.Size = new System.Drawing.Size(597, 226);
            this.OperationsListView.TabIndex = 1;
            this.OperationsListView.UseCompatibleStateImageBehavior = false;
            this.OperationsListView.View = System.Windows.Forms.View.Details;
            this.OperationsListView.SelectedIndexChanged += new System.EventHandler(this.OperationsListView_SelectedIndexChanged);
            // 
            // RemoveButton
            // 
            this.RemoveButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.RemoveButton.Enabled = false;
            this.RemoveButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.RemoveButton.Location = new System.Drawing.Point(513, 263);
            this.RemoveButton.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.RemoveButton.Name = "RemoveButton";
            this.RemoveButton.Size = new System.Drawing.Size(96, 30);
            this.RemoveButton.TabIndex = 4;
            this.RemoveButton.Text = "&Remove";
            this.RemoveButton.UseVisualStyleBackColor = true;
            this.RemoveButton.Click += new System.EventHandler(this.RemoveButton_Click);
            // 
            // AddButton
            // 
            this.AddButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.AddButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.AddButton.Location = new System.Drawing.Point(407, 449);
            this.AddButton.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.AddButton.Name = "AddButton";
            this.AddButton.Size = new System.Drawing.Size(96, 30);
            this.AddButton.TabIndex = 6;
            this.AddButton.Text = "&Add";
            this.AddButton.UseVisualStyleBackColor = true;
            this.AddButton.Click += new System.EventHandler(this.AddButton_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 30);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(71, 17);
            this.label2.TabIndex = 0;
            this.label2.Text = "Operation";
            // 
            // Operation
            // 
            this.Operation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Operation.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.Operation.FormattingEnabled = true;
            this.Operation.Items.AddRange(new object[] {
            "Addition",
            "Division",
            "Multiplication",
            "Rounding",
            "Subtraction",
            "Set"});
            this.Operation.Location = new System.Drawing.Point(16, 49);
            this.Operation.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Operation.Name = "Operation";
            this.Operation.Size = new System.Drawing.Size(148, 24);
            this.Operation.TabIndex = 1;
            this.Operation.SelectedIndexChanged += new System.EventHandler(this.OperationType_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(188, 30);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(91, 17);
            this.label3.TabIndex = 2;
            this.label3.Text = "Value source";
            // 
            // ValueSource
            // 
            this.ValueSource.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ValueSource.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.ValueSource.FormattingEnabled = true;
            this.ValueSource.Items.AddRange(new object[] {
            "Argument",
            "Constant",
            "Matrix",
            "Formula",
            "RunningTotal"});
            this.ValueSource.Location = new System.Drawing.Point(192, 49);
            this.ValueSource.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.ValueSource.Name = "ValueSource";
            this.ValueSource.Size = new System.Drawing.Size(169, 24);
            this.ValueSource.TabIndex = 3;
            this.ValueSource.SelectedIndexChanged += new System.EventHandler(this.ValueSource_SelectedIndexChanged);
            // 
            // ValueSelectionLabel
            // 
            this.ValueSelectionLabel.AutoSize = true;
            this.ValueSelectionLabel.Location = new System.Drawing.Point(389, 30);
            this.ValueSelectionLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.ValueSelectionLabel.Name = "ValueSelectionLabel";
            this.ValueSelectionLabel.Size = new System.Drawing.Size(0, 17);
            this.ValueSelectionLabel.TabIndex = 4;
            // 
            // ValueSelection
            // 
            this.ValueSelection.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ValueSelection.DisplayMember = "Text";
            this.ValueSelection.FormattingEnabled = true;
            this.ValueSelection.Location = new System.Drawing.Point(389, 49);
            this.ValueSelection.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.ValueSelection.Name = "ValueSelection";
            this.ValueSelection.Size = new System.Drawing.Size(191, 24);
            this.ValueSelection.TabIndex = 5;
            this.ValueSelection.TextChanged += new System.EventHandler(this.ValueSelection_TextChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.comboBox2);
            this.groupBox1.Controls.Add(this.comboBox1);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.ValueSelection);
            this.groupBox1.Controls.Add(this.ValueSelectionLabel);
            this.groupBox1.Controls.Add(this.ValueSource);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.Operation);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(12, 300);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox1.Size = new System.Drawing.Size(599, 142);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Operation details";
            // 
            // comboBox2
            // 
            this.comboBox2.DisplayMember = "Text";
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Location = new System.Drawing.Point(389, 98);
            this.comboBox2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(191, 24);
            this.comboBox2.TabIndex = 10;
            // 
            // comboBox1
            // 
            this.comboBox1.DisplayMember = "Text";
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(192, 98);
            this.comboBox1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(169, 24);
            this.comboBox1.TabIndex = 9;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(385, 79);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(71, 17);
            this.label6.TabIndex = 8;
            this.label6.Text = "Value into";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(188, 79);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(118, 17);
            this.label5.TabIndex = 7;
            this.label5.Text = "Value source into";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(385, 30);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(44, 17);
            this.label4.TabIndex = 6;
            this.label4.Text = "Value";
            // 
            // ApplyButton
            // 
            this.ApplyButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.ApplyButton.Enabled = false;
            this.ApplyButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.ApplyButton.Location = new System.Drawing.Point(513, 449);
            this.ApplyButton.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.ApplyButton.Name = "ApplyButton";
            this.ApplyButton.Size = new System.Drawing.Size(96, 30);
            this.ApplyButton.TabIndex = 7;
            this.ApplyButton.Text = "A&pply";
            this.ApplyButton.UseVisualStyleBackColor = true;
            this.ApplyButton.Click += new System.EventHandler(this.ApplyButton_Click);
            // 
            // FormulaOperationView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.ApplyButton);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.AddButton);
            this.Controls.Add(this.MoveDownButton);
            this.Controls.Add(this.MoveUpButton);
            this.Controls.Add(this.RemoveButton);
            this.Controls.Add(this.OperationsListView);
            this.Controls.Add(this.label1);
            this.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.Name = "FormulaOperationView";
            this.Size = new System.Drawing.Size(628, 496);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private System.Windows.Forms.Button MoveUpButton;
        private System.Windows.Forms.Button MoveDownButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ListView OperationsListView;
        private System.Windows.Forms.Button RemoveButton;
        private System.Windows.Forms.Button AddButton;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox Operation;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox ValueSource;
        private System.Windows.Forms.Label ValueSelectionLabel;
        private System.Windows.Forms.ComboBox ValueSelection;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button ApplyButton;
        private System.Windows.Forms.ComboBox comboBox2;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
    }
}
