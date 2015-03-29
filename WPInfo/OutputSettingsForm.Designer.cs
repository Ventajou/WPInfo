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
            this.customFolderTextBox = new System.Windows.Forms.TextBox();
            this.customFolderRadioButton = new System.Windows.Forms.RadioButton();
            this.tempFolderRadioButton = new System.Windows.Forms.RadioButton();
            this.appDataFolderRadioButton = new System.Windows.Forms.RadioButton();
            this.windowsFolderRadioButton = new System.Windows.Forms.RadioButton();
            this.outputFileNameTextBox = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            label1 = new System.Windows.Forms.Label();
            groupBox1 = new System.Windows.Forms.GroupBox();
            groupBox1.SuspendLayout();
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
            groupBox1.Size = new System.Drawing.Size(257, 145);
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
            // outputFileNameTextBox
            // 
            this.outputFileNameTextBox.Location = new System.Drawing.Point(15, 26);
            this.outputFileNameTextBox.Name = "outputFileNameTextBox";
            this.outputFileNameTextBox.Size = new System.Drawing.Size(257, 20);
            this.outputFileNameTextBox.TabIndex = 1;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(197, 204);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 3;
            this.button1.Text = "Cancel";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.CancelPressed);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(116, 204);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 4;
            this.button2.Text = "Ok";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.UpdateOutputSettings);
            // 
            // OutputSettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 241);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
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
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
    }
}