namespace Shuttle.Abacus.Shell.UI.TestArgument
{
    partial class TestArgumentView
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
            this.Description = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.ExpectedResult = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.ValueSelectionControl = new System.Windows.Forms.ComboBox();
            this.ValueSelectionLabel = new System.Windows.Forms.Label();
            this.Argument = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.ApplyButton = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.AnswerType = new System.Windows.Forms.ComboBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // Description
            // 
            this.Description.Location = new System.Drawing.Point(8, 20);
            this.Description.Name = "Description";
            this.Description.Size = new System.Drawing.Size(452, 20);
            this.Description.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Description";
            // 
            // ExpectedResult
            // 
            this.ExpectedResult.Location = new System.Drawing.Point(8, 68);
            this.ExpectedResult.Name = "ExpectedResult";
            this.ExpectedResult.Size = new System.Drawing.Size(224, 20);
            this.ExpectedResult.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 52);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(80, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Expected result";
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.ValueSelectionControl);
            this.groupBox1.Controls.Add(this.ValueSelectionLabel);
            this.groupBox1.Controls.Add(this.Argument);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Location = new System.Drawing.Point(8, 359);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(450, 83);
            this.groupBox1.TabIndex = 12;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Details";
            // 
            // ValueSelectionControl
            // 
            this.ValueSelectionControl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ValueSelectionControl.FormattingEnabled = true;
            this.ValueSelectionControl.Location = new System.Drawing.Point(244, 40);
            this.ValueSelectionControl.Name = "ValueSelectionControl";
            this.ValueSelectionControl.Size = new System.Drawing.Size(183, 21);
            this.ValueSelectionControl.TabIndex = 3;
            this.ValueSelectionControl.SelectedIndexChanged += new System.EventHandler(this.ValueSelection_SelectedIndexChanged);
            // 
            // ValueSelectionLabel
            // 
            this.ValueSelectionLabel.AutoSize = true;
            this.ValueSelectionLabel.Location = new System.Drawing.Point(241, 24);
            this.ValueSelectionLabel.Name = "ValueSelectionLabel";
            this.ValueSelectionLabel.Size = new System.Drawing.Size(34, 13);
            this.ValueSelectionLabel.TabIndex = 2;
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
            this.Argument.SelectedIndexChanged += new System.EventHandler(this.ArgumentName_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 24);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(52, 13);
            this.label5.TabIndex = 0;
            this.label5.Text = "Argument";
            // 
            // ApplyButton
            // 
            this.ApplyButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.ApplyButton.Location = new System.Drawing.Point(384, 448);
            this.ApplyButton.Name = "ApplyButton";
            this.ApplyButton.Size = new System.Drawing.Size(75, 24);
            this.ApplyButton.TabIndex = 13;
            this.ApplyButton.Text = "&Apply";
            this.ApplyButton.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(245, 51);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(103, 13);
            this.label4.TabIndex = 14;
            this.label4.Text = "Expected result type";
            // 
            // ValueType
            // 
            this.AnswerType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.AnswerType.FormattingEnabled = true;
            this.AnswerType.Items.AddRange(new object[] {
            "Boolean",
            "Date",
            "Decimal",
            "Integer",
            "Mapping",
            "Money",
            "Text"});
            this.AnswerType.Location = new System.Drawing.Point(245, 67);
            this.AnswerType.Name = "AnswerType";
            this.AnswerType.Size = new System.Drawing.Size(214, 21);
            this.AnswerType.Sorted = true;
            this.AnswerType.TabIndex = 15;
            // 
            // TestView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label4);
            this.Controls.Add(this.AnswerType);
            this.Controls.Add(this.ApplyButton);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.ExpectedResult);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.Description);
            this.Controls.Add(this.label1);
            this.MinimumSize = new System.Drawing.Size(467, 481);
            this.Name = "TestView";
            this.Size = new System.Drawing.Size(467, 481);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        System.Windows.Forms.ComboBox ValueSelectionControl;


        private System.Windows.Forms.TextBox Description;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox ExpectedResult;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label ValueSelectionLabel;
        private System.Windows.Forms.ComboBox Argument;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button ApplyButton;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox AnswerType;
    }
}
