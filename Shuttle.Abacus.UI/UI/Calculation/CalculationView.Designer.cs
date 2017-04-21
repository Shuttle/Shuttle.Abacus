namespace Abacus.UI
{
    partial class CalculationView
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
            this.CalculationName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.Type = new System.Windows.Forms.ComboBox();
            this.Required = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // CalculationName
            // 
            this.CalculationName.Location = new System.Drawing.Point(8, 24);
            this.CalculationName.Name = "CalculationName";
            this.CalculationName.Size = new System.Drawing.Size(264, 20);
            this.CalculationName.TabIndex = 1;
            this.CalculationName.Leave += new System.EventHandler(this.CalculationName_Leave);
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
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 56);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(31, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Type";
            // 
            // Type
            // 
            this.Type.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Type.FormattingEnabled = true;
            this.Type.Items.AddRange(new object[] {
            "Formula",
            "Collection"});
            this.Type.Location = new System.Drawing.Point(8, 72);
            this.Type.Name = "Type";
            this.Type.Size = new System.Drawing.Size(264, 21);
            this.Type.TabIndex = 3;
            this.Type.SelectedIndexChanged += new System.EventHandler(this.Type_SelectedIndexChanged);
            // 
            // Required
            // 
            this.Required.AutoSize = true;
            this.Required.Enabled = false;
            this.Required.Location = new System.Drawing.Point(8, 112);
            this.Required.Name = "Required";
            this.Required.Size = new System.Drawing.Size(96, 17);
            this.Required.TabIndex = 4;
            this.Required.Text = "Must execute?";
            this.Required.UseVisualStyleBackColor = true;
            // 
            // CalculationView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.Required);
            this.Controls.Add(this.Type);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.CalculationName);
            this.Controls.Add(this.label1);
            this.Name = "CalculationView";
            this.Size = new System.Drawing.Size(304, 157);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private System.Windows.Forms.TextBox CalculationName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox Type;
        private System.Windows.Forms.CheckBox Required;
    }
}
