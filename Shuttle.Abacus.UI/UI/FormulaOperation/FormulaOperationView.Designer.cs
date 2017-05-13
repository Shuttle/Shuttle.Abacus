﻿namespace Shuttle.Abacus.UI.UI.FormulaOperation
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
            this.ApplyButton = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // MoveUpButton
            // 
            this.MoveUpButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.MoveUpButton.Enabled = false;
            this.MoveUpButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.MoveUpButton.Location = new System.Drawing.Point(8, 216);
            this.MoveUpButton.Name = "MoveUpButton";
            this.MoveUpButton.Size = new System.Drawing.Size(72, 24);
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
            this.MoveDownButton.Location = new System.Drawing.Point(88, 216);
            this.MoveDownButton.Name = "MoveDownButton";
            this.MoveDownButton.Size = new System.Drawing.Size(72, 24);
            this.MoveDownButton.TabIndex = 3;
            this.MoveDownButton.Text = "Move &down";
            this.MoveDownButton.UseVisualStyleBackColor = true;
            this.MoveDownButton.Click += new System.EventHandler(this.MoveDownButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 13);
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
            this.OperationsListView.Location = new System.Drawing.Point(8, 24);
            this.OperationsListView.MultiSelect = false;
            this.OperationsListView.Name = "OperationsListView";
            this.OperationsListView.Size = new System.Drawing.Size(449, 184);
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
            this.RemoveButton.Location = new System.Drawing.Point(384, 216);
            this.RemoveButton.Name = "RemoveButton";
            this.RemoveButton.Size = new System.Drawing.Size(72, 24);
            this.RemoveButton.TabIndex = 4;
            this.RemoveButton.Text = "&Remove";
            this.RemoveButton.UseVisualStyleBackColor = true;
            this.RemoveButton.Click += new System.EventHandler(this.RemoveButton_Click);
            // 
            // AddButton
            // 
            this.AddButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.AddButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.AddButton.Location = new System.Drawing.Point(304, 344);
            this.AddButton.Name = "AddButton";
            this.AddButton.Size = new System.Drawing.Size(72, 24);
            this.AddButton.TabIndex = 6;
            this.AddButton.Text = "&Add";
            this.AddButton.UseVisualStyleBackColor = true;
            this.AddButton.Click += new System.EventHandler(this.AddButton_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 24);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 13);
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
            "Subtraction"});
            this.Operation.Location = new System.Drawing.Point(12, 40);
            this.Operation.Name = "Operation";
            this.Operation.Size = new System.Drawing.Size(112, 21);
            this.Operation.TabIndex = 1;
            this.Operation.SelectedIndexChanged += new System.EventHandler(this.OperationType_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(144, 24);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(69, 13);
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
            "Decimal",
            "Matrix",
            "RunningTotal",
            "FormulaResult"});
            this.ValueSource.Location = new System.Drawing.Point(144, 40);
            this.ValueSource.Name = "ValueSource";
            this.ValueSource.Size = new System.Drawing.Size(128, 21);
            this.ValueSource.TabIndex = 3;
            this.ValueSource.SelectedIndexChanged += new System.EventHandler(this.ValueSource_SelectedIndexChanged);
            // 
            // ValueSelectionLabel
            // 
            this.ValueSelectionLabel.AutoSize = true;
            this.ValueSelectionLabel.Location = new System.Drawing.Point(292, 24);
            this.ValueSelectionLabel.Name = "ValueSelectionLabel";
            this.ValueSelectionLabel.Size = new System.Drawing.Size(0, 13);
            this.ValueSelectionLabel.TabIndex = 4;
            // 
            // ValueSelection
            // 
            this.ValueSelection.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ValueSelection.DisplayMember = "Text";
            this.ValueSelection.Enabled = false;
            this.ValueSelection.FormattingEnabled = true;
            this.ValueSelection.Location = new System.Drawing.Point(292, 40);
            this.ValueSelection.Name = "ValueSelection";
            this.ValueSelection.Size = new System.Drawing.Size(144, 21);
            this.ValueSelection.TabIndex = 5;
            this.ValueSelection.TextChanged += new System.EventHandler(this.ValueSelection_TextChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.ValueSelection);
            this.groupBox1.Controls.Add(this.ValueSelectionLabel);
            this.groupBox1.Controls.Add(this.ValueSource);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.Operation);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(8, 260);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(449, 76);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Operation details";
            // 
            // ApplyButton
            // 
            this.ApplyButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.ApplyButton.Enabled = false;
            this.ApplyButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.ApplyButton.Location = new System.Drawing.Point(384, 344);
            this.ApplyButton.Name = "ApplyButton";
            this.ApplyButton.Size = new System.Drawing.Size(72, 24);
            this.ApplyButton.TabIndex = 7;
            this.ApplyButton.Text = "A&pply";
            this.ApplyButton.UseVisualStyleBackColor = true;
            this.ApplyButton.Click += new System.EventHandler(this.ApplyButton_Click);
            // 
            // FormulaOperationView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.ApplyButton);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.AddButton);
            this.Controls.Add(this.MoveDownButton);
            this.Controls.Add(this.MoveUpButton);
            this.Controls.Add(this.RemoveButton);
            this.Controls.Add(this.OperationsListView);
            this.Controls.Add(this.label1);
            this.Name = "FormulaOperationView";
            this.Size = new System.Drawing.Size(471, 381);
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
    }
}
