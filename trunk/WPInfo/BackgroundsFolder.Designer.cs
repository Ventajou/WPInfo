namespace Ventajou.WPInfo
{
    partial class BackgroundsFolder
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BackgroundsFolder));
            this.UseFolderCheckBox = new System.Windows.Forms.CheckBox();
            this.backgroundsFolderTextBox = new System.Windows.Forms.TextBox();
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.backgroundsFolderButton = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // UseFolderCheckBox
            // 
            this.UseFolderCheckBox.AutoSize = true;
            this.UseFolderCheckBox.Location = new System.Drawing.Point(13, 13);
            this.UseFolderCheckBox.Name = "UseFolderCheckBox";
            this.UseFolderCheckBox.Size = new System.Drawing.Size(235, 17);
            this.UseFolderCheckBox.TabIndex = 1;
            this.UseFolderCheckBox.Text = "Find a background image in the folder below";
            this.UseFolderCheckBox.UseVisualStyleBackColor = true;
            this.UseFolderCheckBox.Click += new System.EventHandler(this.EnableBackgroundsFolder);
            // 
            // backgroundsFolderTextBox
            // 
            this.backgroundsFolderTextBox.Location = new System.Drawing.Point(32, 37);
            this.backgroundsFolderTextBox.Name = "backgroundsFolderTextBox";
            this.backgroundsFolderTextBox.Size = new System.Drawing.Size(205, 20);
            this.backgroundsFolderTextBox.TabIndex = 2;
            // 
            // folderBrowserDialog
            // 
            this.folderBrowserDialog.RootFolder = System.Environment.SpecialFolder.MyComputer;
            this.folderBrowserDialog.ShowNewFolderButton = false;
            // 
            // backgroundsFolderButton
            // 
            this.backgroundsFolderButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.backgroundsFolderButton.ForeColor = System.Drawing.SystemColors.ControlText;
            this.backgroundsFolderButton.Image = ((System.Drawing.Image)(resources.GetObject("backgroundsFolderButton.Image")));
            this.backgroundsFolderButton.Location = new System.Drawing.Point(243, 37);
            this.backgroundsFolderButton.Name = "backgroundsFolderButton";
            this.backgroundsFolderButton.Size = new System.Drawing.Size(29, 20);
            this.backgroundsFolderButton.TabIndex = 3;
            this.backgroundsFolderButton.UseVisualStyleBackColor = true;
            this.backgroundsFolderButton.Click += new System.EventHandler(this.BrowseForBackgroundsFolder);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(197, 71);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 4;
            this.button2.Text = "Cancel";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.CancelDialog);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(116, 71);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 5;
            this.button3.Text = "Ok";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.SaveSettings);
            // 
            // BackgroundsFolder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 106);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.backgroundsFolderButton);
            this.Controls.Add(this.backgroundsFolderTextBox);
            this.Controls.Add(this.UseFolderCheckBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "BackgroundsFolder";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Backgrounds Folder";
            this.Shown += new System.EventHandler(this.ShowForm);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox backgroundsFolderTextBox;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
        private System.Windows.Forms.Button backgroundsFolderButton;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.CheckBox UseFolderCheckBox;
    }
}