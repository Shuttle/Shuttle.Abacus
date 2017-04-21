namespace Abacus.UI
{
    partial class PermissionsView
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
            this.PermissionPanel = new System.Windows.Forms.Panel();
            this.ButtonPanel = new System.Windows.Forms.Panel();
            this.RemoveButton = new System.Windows.Forms.Button();
            this.RemoveAllButton = new System.Windows.Forms.Button();
            this.AssignAllButton = new System.Windows.Forms.Button();
            this.AssignButton = new System.Windows.Forms.Button();
            this.AvailablePermissionsListView = new System.Windows.Forms.ListView();
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.AssignedPermissionsListView = new System.Windows.Forms.ListView();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.PermissionPanel.SuspendLayout();
            this.ButtonPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // PermissionPanel
            // 
            this.PermissionPanel.Controls.Add(this.ButtonPanel);
            this.PermissionPanel.Controls.Add(this.AvailablePermissionsListView);
            this.PermissionPanel.Controls.Add(this.AssignedPermissionsListView);
            this.PermissionPanel.Controls.Add(this.label3);
            this.PermissionPanel.Controls.Add(this.label2);
            this.PermissionPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PermissionPanel.Location = new System.Drawing.Point(0, 0);
            this.PermissionPanel.Name = "PermissionPanel";
            this.PermissionPanel.Size = new System.Drawing.Size(584, 278);
            this.PermissionPanel.TabIndex = 3;
            // 
            // ButtonPanel
            // 
            this.ButtonPanel.Controls.Add(this.RemoveButton);
            this.ButtonPanel.Controls.Add(this.RemoveAllButton);
            this.ButtonPanel.Controls.Add(this.AssignAllButton);
            this.ButtonPanel.Controls.Add(this.AssignButton);
            this.ButtonPanel.Location = new System.Drawing.Point(240, 70);
            this.ButtonPanel.Name = "ButtonPanel";
            this.ButtonPanel.Size = new System.Drawing.Size(35, 118);
            this.ButtonPanel.TabIndex = 5;
            // 
            // RemoveButton
            // 
            this.RemoveButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.RemoveButton.Location = new System.Drawing.Point(0, 90);
            this.RemoveButton.Name = "RemoveButton";
            this.RemoveButton.Size = new System.Drawing.Size(35, 23);
            this.RemoveButton.TabIndex = 7;
            this.RemoveButton.Text = ">";
            this.RemoveButton.UseVisualStyleBackColor = true;
            this.RemoveButton.Click += new System.EventHandler(this.RemoveButton_Click);
            // 
            // RemoveAllButton
            // 
            this.RemoveAllButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.RemoveAllButton.Location = new System.Drawing.Point(0, 61);
            this.RemoveAllButton.Name = "RemoveAllButton";
            this.RemoveAllButton.Size = new System.Drawing.Size(35, 23);
            this.RemoveAllButton.TabIndex = 6;
            this.RemoveAllButton.Text = ">>";
            this.RemoveAllButton.UseVisualStyleBackColor = true;
            this.RemoveAllButton.Click += new System.EventHandler(this.RemoveAllButton_Click);
            // 
            // AssignAllButton
            // 
            this.AssignAllButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.AssignAllButton.Location = new System.Drawing.Point(0, 32);
            this.AssignAllButton.Name = "AssignAllButton";
            this.AssignAllButton.Size = new System.Drawing.Size(35, 23);
            this.AssignAllButton.TabIndex = 5;
            this.AssignAllButton.Text = "<<";
            this.AssignAllButton.UseVisualStyleBackColor = true;
            this.AssignAllButton.Click += new System.EventHandler(this.AssignAllButton_Click);
            // 
            // AssignButton
            // 
            this.AssignButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.AssignButton.Location = new System.Drawing.Point(0, 3);
            this.AssignButton.Name = "AssignButton";
            this.AssignButton.Size = new System.Drawing.Size(35, 23);
            this.AssignButton.TabIndex = 4;
            this.AssignButton.Text = "<";
            this.AssignButton.UseVisualStyleBackColor = true;
            this.AssignButton.Click += new System.EventHandler(this.AssignButton_Click);
            // 
            // AvailablePermissionsListView
            // 
            this.AvailablePermissionsListView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.AvailablePermissionsListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader2});
            this.AvailablePermissionsListView.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.AvailablePermissionsListView.HideSelection = false;
            this.AvailablePermissionsListView.Location = new System.Drawing.Point(281, 16);
            this.AvailablePermissionsListView.Name = "AvailablePermissionsListView";
            this.AvailablePermissionsListView.Size = new System.Drawing.Size(300, 259);
            this.AvailablePermissionsListView.TabIndex = 3;
            this.AvailablePermissionsListView.UseCompatibleStateImageBehavior = false;
            this.AvailablePermissionsListView.View = System.Windows.Forms.View.Details;
            this.AvailablePermissionsListView.DoubleClick += new System.EventHandler(this.AvailablePermissionsListView_DoubleClick);
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Permission";
            this.columnHeader2.Width = 219;
            // 
            // AssignedPermissionsListView
            // 
            this.AssignedPermissionsListView.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.AssignedPermissionsListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            this.AssignedPermissionsListView.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.AssignedPermissionsListView.HideSelection = false;
            this.AssignedPermissionsListView.Location = new System.Drawing.Point(0, 16);
            this.AssignedPermissionsListView.Name = "AssignedPermissionsListView";
            this.AssignedPermissionsListView.Size = new System.Drawing.Size(234, 259);
            this.AssignedPermissionsListView.TabIndex = 2;
            this.AssignedPermissionsListView.UseCompatibleStateImageBehavior = false;
            this.AssignedPermissionsListView.View = System.Windows.Forms.View.Details;
            this.AssignedPermissionsListView.DoubleClick += new System.EventHandler(this.AssignedPermissionsListView_DoubleClick);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Permission";
            this.columnHeader1.Width = 207;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(278, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(107, 13);
            this.label3.TabIndex = 1;
            this.label3.Text = "Available permissions";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(-3, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(107, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Assigned permissions";
            // 
            // PermissionsView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.PermissionPanel);
            this.Name = "PermissionsView";
            this.Size = new System.Drawing.Size(584, 278);
            this.Resize += new System.EventHandler(this.PermissionsView_Resize);
            this.PermissionPanel.ResumeLayout(false);
            this.PermissionPanel.PerformLayout();
            this.ButtonPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        private System.Windows.Forms.Panel PermissionPanel;
        private System.Windows.Forms.Panel ButtonPanel;
        private System.Windows.Forms.Button RemoveButton;
        private System.Windows.Forms.Button RemoveAllButton;
        private System.Windows.Forms.Button AssignAllButton;
        private System.Windows.Forms.Button AssignButton;
        private System.Windows.Forms.ListView AvailablePermissionsListView;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ListView AssignedPermissionsListView;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
    }
}
