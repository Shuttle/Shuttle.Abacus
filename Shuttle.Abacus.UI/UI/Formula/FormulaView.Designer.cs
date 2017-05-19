namespace Shuttle.Abacus.Shell.UI.Formula
{
    partial class FormulaView
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
            this.label1 = new System.Windows.Forms.Label();
            this.FormulaName = new System.Windows.Forms.TextBox();
            this.MaximumFormulaName = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.MinimumFormulaName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Name";
            // 
            // FormulaName
            // 
            this.FormulaName.Location = new System.Drawing.Point(11, 24);
            this.FormulaName.Name = "FormulaName";
            this.FormulaName.Size = new System.Drawing.Size(214, 20);
            this.FormulaName.TabIndex = 1;
            // 
            // MaximumFormulaName
            // 
            this.MaximumFormulaName.Location = new System.Drawing.Point(11, 103);
            this.MaximumFormulaName.Name = "MaximumFormulaName";
            this.MaximumFormulaName.Size = new System.Drawing.Size(214, 20);
            this.MaximumFormulaName.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(8, 87);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(117, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "Maximum formula name";
            // 
            // MinimumFormulaName
            // 
            this.MinimumFormulaName.Location = new System.Drawing.Point(11, 142);
            this.MinimumFormulaName.Name = "MinimumFormulaName";
            this.MinimumFormulaName.Size = new System.Drawing.Size(214, 20);
            this.MinimumFormulaName.TabIndex = 7;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 126);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(114, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Minimum formula name";
            // 
            // FormulaView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.MinimumFormulaName);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.MaximumFormulaName);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.FormulaName);
            this.Controls.Add(this.label1);
            this.Name = "FormulaView";
            this.Size = new System.Drawing.Size(270, 193);
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox FormulaName;
        private System.Windows.Forms.TextBox MaximumFormulaName;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox MinimumFormulaName;
        private System.Windows.Forms.Label label2;
    }
}
