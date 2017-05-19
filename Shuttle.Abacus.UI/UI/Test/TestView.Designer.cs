namespace Shuttle.Abacus.Shell.UI.Test
{
    partial class TestView
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
            this.TestName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.ExpectedResult = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.ExpectedResultType = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.FormulaName = new System.Windows.Forms.ComboBox();
            this.Comparison = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // TestName
            // 
            this.TestName.Location = new System.Drawing.Point(8, 20);
            this.TestName.Name = "TestName";
            this.TestName.Size = new System.Drawing.Size(452, 20);
            this.TestName.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Name";
            // 
            // ExpectedResult
            // 
            this.ExpectedResult.Location = new System.Drawing.Point(8, 154);
            this.ExpectedResult.Name = "ExpectedResult";
            this.ExpectedResult.Size = new System.Drawing.Size(452, 20);
            this.ExpectedResult.TabIndex = 9;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 138);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(80, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "Expected result";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(8, 92);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(103, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "Expected result type";
            // 
            // ExpectedResultType
            // 
            this.ExpectedResultType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ExpectedResultType.FormattingEnabled = true;
            this.ExpectedResultType.Items.AddRange(new object[] {
            "Boolean",
            "Date",
            "Decimal",
            "Integer",
            "Text"});
            this.ExpectedResultType.Location = new System.Drawing.Point(8, 108);
            this.ExpectedResultType.Name = "ExpectedResultType";
            this.ExpectedResultType.Size = new System.Drawing.Size(214, 21);
            this.ExpectedResultType.Sorted = true;
            this.ExpectedResultType.TabIndex = 5;
            this.ExpectedResultType.SelectedIndexChanged += new System.EventHandler(this.ExpectedResultType_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 47);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(73, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Formula name";
            // 
            // FormulaName
            // 
            this.FormulaName.FormattingEnabled = true;
            this.FormulaName.Location = new System.Drawing.Point(8, 63);
            this.FormulaName.Name = "FormulaName";
            this.FormulaName.Size = new System.Drawing.Size(452, 21);
            this.FormulaName.Sorted = true;
            this.FormulaName.TabIndex = 3;
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
            this.Comparison.Location = new System.Drawing.Point(251, 108);
            this.Comparison.Name = "Comparison";
            this.Comparison.Size = new System.Drawing.Size(209, 21);
            this.Comparison.TabIndex = 7;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(251, 92);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(62, 13);
            this.label5.TabIndex = 6;
            this.label5.Text = "Comparison";
            // 
            // TestView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.Comparison);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.FormulaName);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.ExpectedResultType);
            this.Controls.Add(this.ExpectedResult);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.TestName);
            this.Controls.Add(this.label1);
            this.Name = "TestView";
            this.Size = new System.Drawing.Size(467, 201);
            this.ResumeLayout(false);
            this.PerformLayout();

        }


        private System.Windows.Forms.TextBox TestName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox ExpectedResult;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox ExpectedResultType;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox FormulaName;
        private System.Windows.Forms.ComboBox Comparison;
        private System.Windows.Forms.Label label5;
    }
}
