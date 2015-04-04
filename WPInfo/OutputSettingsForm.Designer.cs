namespace Ventajou.WPInfo
{
    partial class OutputSettingsForm
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
            System.Windows.Forms.Label label1;
            System.Windows.Forms.GroupBox groupBox1;
            System.Windows.Forms.GroupBox imageModeGroup;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OutputSettingsForm));
            this.customFolderTextBox = new System.Windows.Forms.TextBox();
            this.customFolderRadioButton = new System.Windows.Forms.RadioButton();
            this.tempFolderRadioButton = new System.Windows.Forms.RadioButton();
            this.appDataFolderRadioButton = new System.Windows.Forms.RadioButton();
            this.windowsFolderRadioButton = new System.Windows.Forms.RadioButton();
            this.pictureBox6 = new System.Windows.Forms.PictureBox();
            this.pictureBox5 = new System.Windows.Forms.PictureBox();
            this.pictureBox4 = new System.Windows.Forms.PictureBox();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox7 = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.radioBGTiled = new System.Windows.Forms.RadioButton();
            this.radioBGFill = new System.Windows.Forms.RadioButton();
            this.radioBGFit = new System.Windows.Forms.RadioButton();
            this.radioBGStretched = new System.Windows.Forms.RadioButton();
            this.radioBGCentered = new System.Windows.Forms.RadioButton();
            this.outputFileNameTextBox = new System.Windows.Forms.TextBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            label1 = new System.Windows.Forms.Label();
            groupBox1 = new System.Windows.Forms.GroupBox();
            imageModeGroup = new System.Windows.Forms.GroupBox();
            groupBox1.SuspendLayout();
            imageModeGroup.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(12, 9);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(89, 13);
            label1.TabIndex = 0;
            label1.Text = "Output File Name";
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(this.customFolderTextBox);
            groupBox1.Controls.Add(this.customFolderRadioButton);
            groupBox1.Controls.Add(this.tempFolderRadioButton);
            groupBox1.Controls.Add(this.appDataFolderRadioButton);
            groupBox1.Controls.Add(this.windowsFolderRadioButton);
            groupBox1.Location = new System.Drawing.Point(15, 52);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new System.Drawing.Size(257, 148);
            groupBox1.TabIndex = 2;
            groupBox1.TabStop = false;
            groupBox1.Text = "Output Folder";
            // 
            // customFolderTextBox
            // 
            this.customFolderTextBox.Location = new System.Drawing.Point(23, 116);
            this.customFolderTextBox.Name = "customFolderTextBox";
            this.customFolderTextBox.Size = new System.Drawing.Size(228, 20);
            this.customFolderTextBox.TabIndex = 4;
            // 
            // customFolderRadioButton
            // 
            this.customFolderRadioButton.AutoSize = true;
            this.customFolderRadioButton.Location = new System.Drawing.Point(7, 92);
            this.customFolderRadioButton.Name = "customFolderRadioButton";
            this.customFolderRadioButton.Size = new System.Drawing.Size(92, 17);
            this.customFolderRadioButton.TabIndex = 3;
            this.customFolderRadioButton.TabStop = true;
            this.customFolderRadioButton.Text = "Custom Folder";
            this.customFolderRadioButton.UseVisualStyleBackColor = true;
            this.customFolderRadioButton.CheckedChanged += new System.EventHandler(this.CustomFolderSelected);
            // 
            // tempFolderRadioButton
            // 
            this.tempFolderRadioButton.AutoSize = true;
            this.tempFolderRadioButton.Location = new System.Drawing.Point(7, 68);
            this.tempFolderRadioButton.Name = "tempFolderRadioButton";
            this.tempFolderRadioButton.Size = new System.Drawing.Size(139, 17);
            this.tempFolderRadioButton.TabIndex = 2;
            this.tempFolderRadioButton.TabStop = true;
            this.tempFolderRadioButton.Text = "User\'s Temporary Folder";
            this.tempFolderRadioButton.UseVisualStyleBackColor = true;
            this.tempFolderRadioButton.CheckedChanged += new System.EventHandler(this.TempFolderSelected);
            // 
            // appDataFolderRadioButton
            // 
            this.appDataFolderRadioButton.AutoSize = true;
            this.appDataFolderRadioButton.Location = new System.Drawing.Point(7, 44);
            this.appDataFolderRadioButton.Name = "appDataFolderRadioButton";
            this.appDataFolderRadioButton.Size = new System.Drawing.Size(172, 17);
            this.appDataFolderRadioButton.TabIndex = 1;
            this.appDataFolderRadioButton.TabStop = true;
            this.appDataFolderRadioButton.Text = "User\'s Applications Data Folder";
            this.appDataFolderRadioButton.UseVisualStyleBackColor = true;
            this.appDataFolderRadioButton.CheckedChanged += new System.EventHandler(this.AppDataFolderSelected);
            // 
            // windowsFolderRadioButton
            // 
            this.windowsFolderRadioButton.AutoSize = true;
            this.windowsFolderRadioButton.Location = new System.Drawing.Point(7, 20);
            this.windowsFolderRadioButton.Name = "windowsFolderRadioButton";
            this.windowsFolderRadioButton.Size = new System.Drawing.Size(101, 17);
            this.windowsFolderRadioButton.TabIndex = 0;
            this.windowsFolderRadioButton.TabStop = true;
            this.windowsFolderRadioButton.Text = "Windows Folder";
            this.windowsFolderRadioButton.UseVisualStyleBackColor = true;
            this.windowsFolderRadioButton.CheckedChanged += new System.EventHandler(this.WindowsFolderSelected);
            // 
            // imageModeGroup
            // 
            imageModeGroup.Controls.Add(this.pictureBox6);
            imageModeGroup.Controls.Add(this.pictureBox5);
            imageModeGroup.Controls.Add(this.pictureBox4);
            imageModeGroup.Controls.Add(this.pictureBox3);
            imageModeGroup.Controls.Add(this.pictureBox2);
            imageModeGroup.Controls.Add(this.pictureBox7);
            imageModeGroup.Controls.Add(this.pictureBox1);
            imageModeGroup.Controls.Add(this.radioBGTiled);
            imageModeGroup.Controls.Add(this.radioBGFill);
            imageModeGroup.Controls.Add(this.radioBGFit);
            imageModeGroup.Controls.Add(this.radioBGStretched);
            imageModeGroup.Controls.Add(this.radioBGCentered);
            imageModeGroup.Location = new System.Drawing.Point(278, 9);
            imageModeGroup.Name = "imageModeGroup";
            imageModeGroup.Size = new System.Drawing.Size(297, 191);
            imageModeGroup.TabIndex = 5;
            imageModeGroup.TabStop = false;
            imageModeGroup.Text = "Background Image Mode";
            // 
            // pictureBox6
            // 
            this.pictureBox6.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox6.Image")));
            this.pictureBox6.Location = new System.Drawing.Point(235, 145);
            this.pictureBox6.Name = "pictureBox6";
            this.pictureBox6.Size = new System.Drawing.Size(56, 27);
            this.pictureBox6.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox6.TabIndex = 5;
            this.pictureBox6.TabStop = false;
            // 
            // pictureBox5
            // 
            this.pictureBox5.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox5.Image")));
            this.pictureBox5.Location = new System.Drawing.Point(173, 79);
            this.pictureBox5.Name = "pictureBox5";
            this.pictureBox5.Size = new System.Drawing.Size(56, 27);
            this.pictureBox5.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox5.TabIndex = 5;
            this.pictureBox5.TabStop = false;
            // 
            // pictureBox4
            // 
            this.pictureBox4.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox4.Image")));
            this.pictureBox4.Location = new System.Drawing.Point(235, 112);
            this.pictureBox4.Name = "pictureBox4";
            this.pictureBox4.Size = new System.Drawing.Size(56, 27);
            this.pictureBox4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox4.TabIndex = 5;
            this.pictureBox4.TabStop = false;
            // 
            // pictureBox3
            // 
            this.pictureBox3.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox3.Image")));
            this.pictureBox3.Location = new System.Drawing.Point(235, 79);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(56, 27);
            this.pictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox3.TabIndex = 5;
            this.pictureBox3.TabStop = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(235, 48);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(56, 27);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox2.TabIndex = 5;
            this.pictureBox2.TabStop = false;
            // 
            // pictureBox7
            // 
            this.pictureBox7.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox7.Image")));
            this.pictureBox7.Location = new System.Drawing.Point(235, 17);
            this.pictureBox7.Name = "pictureBox7";
            this.pictureBox7.Size = new System.Drawing.Size(56, 27);
            this.pictureBox7.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox7.TabIndex = 4;
            this.pictureBox7.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(173, 17);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(56, 27);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 4;
            this.pictureBox1.TabStop = false;
            // 
            // radioBGTiled
            // 
            this.radioBGTiled.AutoSize = true;
            this.radioBGTiled.Location = new System.Drawing.Point(8, 151);
            this.radioBGTiled.Name = "radioBGTiled";
            this.radioBGTiled.Size = new System.Drawing.Size(48, 17);
            this.radioBGTiled.TabIndex = 3;
            this.radioBGTiled.Text = "Tiled";
            this.radioBGTiled.UseVisualStyleBackColor = true;
            this.radioBGTiled.CheckedChanged += new System.EventHandler(this.radioBGTiled_CheckedChanged);
            // 
            // radioBGFill
            // 
            this.radioBGFill.AutoSize = true;
            this.radioBGFill.Location = new System.Drawing.Point(8, 119);
            this.radioBGFill.Name = "radioBGFill";
            this.radioBGFill.Size = new System.Drawing.Size(74, 17);
            this.radioBGFill.TabIndex = 3;
            this.radioBGFill.Text = "Fill Screen";
            this.radioBGFill.UseVisualStyleBackColor = true;
            this.radioBGFill.CheckedChanged += new System.EventHandler(this.radioBGFill_CheckedChanged);
            // 
            // radioBGFit
            // 
            this.radioBGFit.AutoSize = true;
            this.radioBGFit.Location = new System.Drawing.Point(8, 87);
            this.radioBGFit.Name = "radioBGFit";
            this.radioBGFit.Size = new System.Drawing.Size(139, 17);
            this.radioBGFit.TabIndex = 2;
            this.radioBGFit.Text = "Fit (Pillarbox / Letterbox)";
            this.radioBGFit.UseVisualStyleBackColor = true;
            this.radioBGFit.CheckedChanged += new System.EventHandler(this.radioBGFit_CheckedChanged);
            // 
            // radioBGStretched
            // 
            this.radioBGStretched.AutoSize = true;
            this.radioBGStretched.Location = new System.Drawing.Point(8, 55);
            this.radioBGStretched.Name = "radioBGStretched";
            this.radioBGStretched.Size = new System.Drawing.Size(71, 17);
            this.radioBGStretched.TabIndex = 1;
            this.radioBGStretched.Text = "Stretched";
            this.radioBGStretched.UseVisualStyleBackColor = true;
            this.radioBGStretched.CheckedChanged += new System.EventHandler(this.radioBGStretched_CheckedChanged);
            // 
            // radioBGCentered
            // 
            this.radioBGCentered.AutoSize = true;
            this.radioBGCentered.Location = new System.Drawing.Point(8, 23);
            this.radioBGCentered.Name = "radioBGCentered";
            this.radioBGCentered.Size = new System.Drawing.Size(68, 17);
            this.radioBGCentered.TabIndex = 0;
            this.radioBGCentered.Text = "Centered";
            this.radioBGCentered.UseVisualStyleBackColor = true;
            this.radioBGCentered.CheckedChanged += new System.EventHandler(this.radioBGCentered_CheckedChanged);
            // 
            // outputFileNameTextBox
            // 
            this.outputFileNameTextBox.Location = new System.Drawing.Point(15, 26);
            this.outputFileNameTextBox.Name = "outputFileNameTextBox";
            this.outputFileNameTextBox.Size = new System.Drawing.Size(257, 20);
            this.outputFileNameTextBox.TabIndex = 1;
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(500, 206);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.CancelPressed);
            // 
            // btnOK
            // 
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(419, 206);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 4;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.UpdateOutputSettings);
            // 
            // OutputSettingsForm
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(587, 241);
            this.Controls.Add(imageModeGroup);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(groupBox1);
            this.Controls.Add(this.outputFileNameTextBox);
            this.Controls.Add(label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "OutputSettingsForm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Output Settings";
            this.Load += new System.EventHandler(this.FormLoaded);
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            imageModeGroup.ResumeLayout(false);
            imageModeGroup.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox outputFileNameTextBox;
        private System.Windows.Forms.RadioButton tempFolderRadioButton;
        private System.Windows.Forms.RadioButton appDataFolderRadioButton;
        private System.Windows.Forms.RadioButton windowsFolderRadioButton;
        private System.Windows.Forms.RadioButton customFolderRadioButton;
        private System.Windows.Forms.TextBox customFolderTextBox;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.PictureBox pictureBox6;
        private System.Windows.Forms.PictureBox pictureBox5;
        private System.Windows.Forms.PictureBox pictureBox4;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.RadioButton radioBGTiled;
        private System.Windows.Forms.RadioButton radioBGFill;
        private System.Windows.Forms.RadioButton radioBGFit;
        private System.Windows.Forms.RadioButton radioBGStretched;
        private System.Windows.Forms.RadioButton radioBGCentered;
        private System.Windows.Forms.PictureBox pictureBox7;
    }
}