namespace Shuttle.Abacus.Shell.UI.Test.TestArgument
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
            this.ValueControl = new System.Windows.Forms.ComboBox();
            this.ValueSelectionLabel = new System.Windows.Forms.Label();
            this.Argument = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // ValueControl
            // 
            this.ValueControl.FormattingEnabled = true;
            this.ValueControl.Location = new System.Drawing.Point(8, 68);
            this.ValueControl.Name = "ValueControl";
            this.ValueControl.Size = new System.Drawing.Size(263, 21);
            this.ValueControl.TabIndex = 3;
            this.ValueControl.SelectedIndexChanged += new System.EventHandler(this.ValueSelection_SelectedIndexChanged);
            // 
            // ValueSelectionLabel
            // 
            this.ValueSelectionLabel.AutoSize = true;
            this.ValueSelectionLabel.Location = new System.Drawing.Point(5, 52);
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
            this.Argument.Location = new System.Drawing.Point(8, 25);
            this.Argument.Name = "Argument";
            this.Argument.Size = new System.Drawing.Size(263, 21);
            this.Argument.TabIndex = 1;
            this.Argument.SelectedIndexChanged += new System.EventHandler(this.ArgumentName_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(5, 9);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(52, 13);
            this.label5.TabIndex = 0;
            this.label5.Text = "Argument";
            // 
            // TestArgumentValueView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.ValueControl);
            this.Controls.Add(this.ValueSelectionLabel);
            this.Controls.Add(this.Argument);
            this.Controls.Add(this.label5);
            this.Name = "TestArgumentView";
            this.Size = new System.Drawing.Size(295, 113);
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        System.Windows.Forms.ComboBox ValueControl;
        private System.Windows.Forms.Label ValueSelectionLabel;
        private System.Windows.Forms.ComboBox Argument;
        private System.Windows.Forms.Label label5;
    }
}
