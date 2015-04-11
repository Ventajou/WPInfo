namespace Ventajou.WPInfo
{
    partial class BackgroundsFolderForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BackgroundsFolderForm));
            this.useFolderCheckBox = new System.Windows.Forms.CheckBox();
            this.backgroundsFolderTextBox = new System.Windows.Forms.TextBox();
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.backgroundsFolderButton = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.showBoxCheckBox = new System.Windows.Forms.CheckBox();
            this.opacityTrackBar = new System.Windows.Forms.TrackBar();
            this.OpacityLabel = new System.Windows.Forms.Label();
            this.pbTextBox = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.opacityTrackBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbTextBox)).BeginInit();
            this.SuspendLayout();
            // 
            // useFolderCheckBox
            // 
            this.useFolderCheckBox.AutoSize = true;
            this.useFolderCheckBox.Location = new System.Drawing.Point(13, 13);
            this.useFolderCheckBox.Name = "useFolderCheckBox";
            this.useFolderCheckBox.Size = new System.Drawing.Size(235, 17);
            this.useFolderCheckBox.TabIndex = 1;
            this.useFolderCheckBox.Text = "Find a background image in the folder below";
            this.useFolderCheckBox.UseVisualStyleBackColor = true;
            this.useFolderCheckBox.Click += new System.EventHandler(this.EnableBackgroundsFolder);
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
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(197, 138);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.CancelDialog);
            // 
            // btnOK
            // 
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(116, 138);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 5;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.SaveSettings);
            // 
            // showBoxCheckBox
            // 
            this.showBoxCheckBox.AutoSize = true;
            this.showBoxCheckBox.Location = new System.Drawing.Point(32, 64);
            this.showBoxCheckBox.Name = "showBoxCheckBox";
            this.showBoxCheckBox.Size = new System.Drawing.Size(228, 17);
            this.showBoxCheckBox.TabIndex = 6;
            this.showBoxCheckBox.Text = "Draw a colored box to improve text visibility";
            this.showBoxCheckBox.UseVisualStyleBackColor = true;
            // 
            // opacityTrackBar
            // 
            this.opacityTrackBar.Location = new System.Drawing.Point(98, 87);
            this.opacityTrackBar.Maximum = 255;
            this.opacityTrackBar.Name = "opacityTrackBar";
            this.opacityTrackBar.Size = new System.Drawing.Size(174, 45);
            this.opacityTrackBar.TabIndex = 7;
            this.opacityTrackBar.TickFrequency = 20;
            this.opacityTrackBar.Value = 255;
            this.opacityTrackBar.Scroll += new System.EventHandler(this.opacityTrackBar_Scroll);
            // 
            // OpacityLabel
            // 
            this.OpacityLabel.AutoSize = true;
            this.OpacityLabel.Location = new System.Drawing.Point(49, 87);
            this.OpacityLabel.Name = "OpacityLabel";
            this.OpacityLabel.Size = new System.Drawing.Size(43, 13);
            this.OpacityLabel.TabIndex = 8;
            this.OpacityLabel.Text = "Opacity";
            // 
            // pbTextBox
            // 
            this.pbTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbTextBox.Location = new System.Drawing.Point(32, 103);
            this.pbTextBox.Name = "pbTextBox";
            this.pbTextBox.Padding = new System.Windows.Forms.Padding(1);
            this.pbTextBox.Size = new System.Drawing.Size(60, 29);
            this.pbTextBox.TabIndex = 9;
            this.pbTextBox.TabStop = false;
            // 
            // BackgroundsFolderForm
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(283, 175);
            this.Controls.Add(this.pbTextBox);
            this.Controls.Add(this.OpacityLabel);
            this.Controls.Add(this.opacityTrackBar);
            this.Controls.Add(this.showBoxCheckBox);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.backgroundsFolderButton);
            this.Controls.Add(this.backgroundsFolderTextBox);
            this.Controls.Add(this.useFolderCheckBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "BackgroundsFolderForm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Backgrounds Folder";
            this.Shown += new System.EventHandler(this.ShowForm);
            ((System.ComponentModel.ISupportInitialize)(this.opacityTrackBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbTextBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox backgroundsFolderTextBox;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
        private System.Windows.Forms.Button backgroundsFolderButton;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.CheckBox useFolderCheckBox;
        private System.Windows.Forms.CheckBox showBoxCheckBox;
        private System.Windows.Forms.TrackBar opacityTrackBar;
        private System.Windows.Forms.Label OpacityLabel;
        private System.Windows.Forms.PictureBox pbTextBox;
    }
}