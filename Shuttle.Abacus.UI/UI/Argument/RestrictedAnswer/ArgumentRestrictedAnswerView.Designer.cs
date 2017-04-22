namespace Shuttle.Abacus.UI.UI.Argument.RestrictedAnswer
{
    partial class ArgumentRestrictedAnswerView
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
            this.Answer = new System.Windows.Forms.TextBox();
            this.AnswerListView = new System.Windows.Forms.ListView();
            this.columnAnswer = new System.Windows.Forms.ColumnHeader();
            this.removeMapping = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.AddButton = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(42, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Answer";
            // 
            // Answer
            // 
            this.Answer.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.Answer.Location = new System.Drawing.Point(16, 40);
            this.Answer.Name = "Answer";
            this.Answer.Size = new System.Drawing.Size(403, 20);
            this.Answer.TabIndex = 1;
            this.Answer.TextChanged += new System.EventHandler(this.Answer_TextChanged);
            // 
            // AnswerListView
            // 
            this.AnswerListView.AllowColumnReorder = true;
            this.AnswerListView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.AnswerListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnAnswer});
            this.AnswerListView.FullRowSelect = true;
            this.AnswerListView.Location = new System.Drawing.Point(16, 32);
            this.AnswerListView.MultiSelect = false;
            this.AnswerListView.Name = "AnswerListView";
            this.AnswerListView.Size = new System.Drawing.Size(443, 165);
            this.AnswerListView.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.AnswerListView.TabIndex = 1;
            this.AnswerListView.UseCompatibleStateImageBehavior = false;
            this.AnswerListView.View = System.Windows.Forms.View.Details;
            this.AnswerListView.SelectedIndexChanged += new System.EventHandler(this.AnswerListView_SelectedIndexChanged);
            // 
            // columnAnswer
            // 
            this.columnAnswer.Text = "Answer";
            this.columnAnswer.Width = 424;
            // 
            // removeMapping
            // 
            this.removeMapping.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.removeMapping.Location = new System.Drawing.Point(387, 205);
            this.removeMapping.Name = "removeMapping";
            this.removeMapping.Size = new System.Drawing.Size(75, 24);
            this.removeMapping.TabIndex = 2;
            this.removeMapping.Text = "Remove";
            this.removeMapping.UseVisualStyleBackColor = true;
            this.removeMapping.Click += new System.EventHandler(this.RemoveAnswer_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.Answer);
            this.groupBox1.Location = new System.Drawing.Point(16, 245);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(443, 80);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Details";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(16, 16);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(98, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Restricted Answers";
            // 
            // AddButton
            // 
            this.AddButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.AddButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.AddButton.Location = new System.Drawing.Point(387, 333);
            this.AddButton.Name = "AddButton";
            this.AddButton.Size = new System.Drawing.Size(75, 23);
            this.AddButton.TabIndex = 4;
            this.AddButton.Text = "&Add";
            this.AddButton.UseVisualStyleBackColor = true;
            this.AddButton.Click += new System.EventHandler(this.AddButton_Click);
            // 
            // ArgumentRestrictedAnswerView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.AddButton);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.removeMapping);
            this.Controls.Add(this.AnswerListView);
            this.Name = "ArgumentRestrictedAnswerView";
            this.Size = new System.Drawing.Size(478, 374);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox Answer;
        private System.Windows.Forms.ListView AnswerListView;
        private System.Windows.Forms.ColumnHeader columnAnswer;
        private System.Windows.Forms.Button removeMapping;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button AddButton;
    }
}
