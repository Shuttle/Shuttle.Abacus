namespace Shuttle.Abacus.UI.UI.Argument
{
    partial class ArgumentView
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
            this.ArgumentName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.AnswerType = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // ArgumentName
            // 
            this.ArgumentName.Location = new System.Drawing.Point(8, 24);
            this.ArgumentName.Name = "ArgumentName";
            this.ArgumentName.Size = new System.Drawing.Size(214, 20);
            this.ArgumentName.TabIndex = 1;
            this.ArgumentName.Leave += new System.EventHandler(this.ArgumentName_Leave);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(81, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Argument name";
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
            this.AnswerType.Location = new System.Drawing.Point(8, 72);
            this.AnswerType.Name = "AnswerType";
            this.AnswerType.Size = new System.Drawing.Size(214, 21);
            this.AnswerType.Sorted = true;
            this.AnswerType.TabIndex = 5;
            this.AnswerType.SelectedIndexChanged += new System.EventHandler(this.AnswerType_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 56);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Value type";
            // 
            // ArgumentView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label3);
            this.Controls.Add(this.AnswerType);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ArgumentName);
            this.Name = "ArgumentView";
            this.Size = new System.Drawing.Size(263, 132);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private System.Windows.Forms.ComboBox AnswerType;
        private System.Windows.Forms.TextBox ArgumentName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
    }
}
