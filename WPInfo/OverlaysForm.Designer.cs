namespace Ventajou.WPInfo
{
    partial class OverlaysForm
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
            System.Windows.Forms.Button button3;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OverlaysForm));
            this.overlaysListBox = new System.Windows.Forms.ListBox();
            this.moveUpButton = new System.Windows.Forms.Button();
            this.moveDownButton = new System.Windows.Forms.Button();
            this.deleteButton = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.bottomRadioButton = new System.Windows.Forms.RadioButton();
            this.rightRadioButton = new System.Windows.Forms.RadioButton();
            this.centerRadioButton = new System.Windows.Forms.RadioButton();
            this.leftRadioButton = new System.Windows.Forms.RadioButton();
            this.topRadioButton = new System.Windows.Forms.RadioButton();
            this.bottomRightRadioButton = new System.Windows.Forms.RadioButton();
            this.topRightRadioButton = new System.Windows.Forms.RadioButton();
            this.bottomLeftRadioButton = new System.Windows.Forms.RadioButton();
            this.topLeftRadioButton = new System.Windows.Forms.RadioButton();
            this.label4 = new System.Windows.Forms.Label();
            this.marginNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.browseButton = new System.Windows.Forms.Button();
            this.fileTextBox = new System.Windows.Forms.TextBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.imageOpenFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.updateButton = new System.Windows.Forms.Button();
            button3 = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.marginNumericUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // button3
            // 
            button3.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            button3.Image = ((System.Drawing.Image)(resources.GetObject("button3.Image")));
            button3.Location = new System.Drawing.Point(170, 71);
            button3.Name = "button3";
            button3.Size = new System.Drawing.Size(23, 23);
            button3.TabIndex = 3;
            button3.UseVisualStyleBackColor = true;
            button3.Click += new System.EventHandler(this.NewOverlay);
            // 
            // overlaysListBox
            // 
            this.overlaysListBox.FormattingEnabled = true;
            this.overlaysListBox.Location = new System.Drawing.Point(13, 13);
            this.overlaysListBox.Name = "overlaysListBox";
            this.overlaysListBox.Size = new System.Drawing.Size(151, 225);
            this.overlaysListBox.TabIndex = 0;
            this.overlaysListBox.SelectedIndexChanged += new System.EventHandler(this.OverlaySelected);
            // 
            // moveUpButton
            // 
            this.moveUpButton.Enabled = false;
            this.moveUpButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.moveUpButton.Image = ((System.Drawing.Image)(resources.GetObject("moveUpButton.Image")));
            this.moveUpButton.Location = new System.Drawing.Point(170, 13);
            this.moveUpButton.Name = "moveUpButton";
            this.moveUpButton.Size = new System.Drawing.Size(23, 23);
            this.moveUpButton.TabIndex = 1;
            this.moveUpButton.UseVisualStyleBackColor = true;
            this.moveUpButton.Click += new System.EventHandler(this.MoveOverlayUp);
            // 
            // moveDownButton
            // 
            this.moveDownButton.Enabled = false;
            this.moveDownButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.moveDownButton.Image = ((System.Drawing.Image)(resources.GetObject("moveDownButton.Image")));
            this.moveDownButton.Location = new System.Drawing.Point(170, 42);
            this.moveDownButton.Name = "moveDownButton";
            this.moveDownButton.Size = new System.Drawing.Size(23, 23);
            this.moveDownButton.TabIndex = 2;
            this.moveDownButton.UseVisualStyleBackColor = true;
            this.moveDownButton.Click += new System.EventHandler(this.MoveOverlayDown);
            // 
            // deleteButton
            // 
            this.deleteButton.Enabled = false;
            this.deleteButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.deleteButton.Image = ((System.Drawing.Image)(resources.GetObject("deleteButton.Image")));
            this.deleteButton.Location = new System.Drawing.Point(170, 129);
            this.deleteButton.Name = "deleteButton";
            this.deleteButton.Size = new System.Drawing.Size(23, 23);
            this.deleteButton.TabIndex = 4;
            this.deleteButton.UseVisualStyleBackColor = true;
            this.deleteButton.Click += new System.EventHandler(this.DeleteOverlay);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.marginNumericUpDown);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.browseButton);
            this.groupBox1.Controls.Add(this.fileTextBox);
            this.groupBox1.Location = new System.Drawing.Point(200, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(270, 226);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Image Settings";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.bottomRadioButton);
            this.groupBox2.Controls.Add(this.rightRadioButton);
            this.groupBox2.Controls.Add(this.centerRadioButton);
            this.groupBox2.Controls.Add(this.leftRadioButton);
            this.groupBox2.Controls.Add(this.topRadioButton);
            this.groupBox2.Controls.Add(this.bottomRightRadioButton);
            this.groupBox2.Controls.Add(this.topRightRadioButton);
            this.groupBox2.Controls.Add(this.bottomLeftRadioButton);
            this.groupBox2.Controls.Add(this.topLeftRadioButton);
            this.groupBox2.Location = new System.Drawing.Point(64, 83);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(200, 137);
            this.groupBox2.TabIndex = 9;
            this.groupBox2.TabStop = false;
            // 
            // bottomRadioButton
            // 
            this.bottomRadioButton.AutoSize = true;
            this.bottomRadioButton.Location = new System.Drawing.Point(93, 118);
            this.bottomRadioButton.Name = "bottomRadioButton";
            this.bottomRadioButton.Size = new System.Drawing.Size(14, 13);
            this.bottomRadioButton.TabIndex = 15;
            this.bottomRadioButton.TabStop = true;
            this.bottomRadioButton.Tag = "Bottom,Center";
            this.bottomRadioButton.UseVisualStyleBackColor = true;
            this.bottomRadioButton.CheckedChanged += new System.EventHandler(this.PositionChanged);
            // 
            // rightRadioButton
            // 
            this.rightRadioButton.AutoSize = true;
            this.rightRadioButton.Location = new System.Drawing.Point(180, 65);
            this.rightRadioButton.Name = "rightRadioButton";
            this.rightRadioButton.Size = new System.Drawing.Size(14, 13);
            this.rightRadioButton.TabIndex = 14;
            this.rightRadioButton.TabStop = true;
            this.rightRadioButton.Tag = "Center,Right";
            this.rightRadioButton.UseVisualStyleBackColor = true;
            this.rightRadioButton.CheckedChanged += new System.EventHandler(this.PositionChanged);
            // 
            // centerRadioButton
            // 
            this.centerRadioButton.AutoSize = true;
            this.centerRadioButton.Location = new System.Drawing.Point(93, 65);
            this.centerRadioButton.Name = "centerRadioButton";
            this.centerRadioButton.Size = new System.Drawing.Size(14, 13);
            this.centerRadioButton.TabIndex = 13;
            this.centerRadioButton.TabStop = true;
            this.centerRadioButton.Tag = "Center,Center";
            this.centerRadioButton.UseVisualStyleBackColor = true;
            this.centerRadioButton.CheckedChanged += new System.EventHandler(this.PositionChanged);
            // 
            // leftRadioButton
            // 
            this.leftRadioButton.AutoSize = true;
            this.leftRadioButton.Location = new System.Drawing.Point(6, 65);
            this.leftRadioButton.Name = "leftRadioButton";
            this.leftRadioButton.Size = new System.Drawing.Size(14, 13);
            this.leftRadioButton.TabIndex = 12;
            this.leftRadioButton.TabStop = true;
            this.leftRadioButton.Tag = "Center,Left";
            this.leftRadioButton.UseVisualStyleBackColor = true;
            this.leftRadioButton.CheckedChanged += new System.EventHandler(this.PositionChanged);
            // 
            // topRadioButton
            // 
            this.topRadioButton.AutoSize = true;
            this.topRadioButton.Location = new System.Drawing.Point(93, 12);
            this.topRadioButton.Name = "topRadioButton";
            this.topRadioButton.Size = new System.Drawing.Size(14, 13);
            this.topRadioButton.TabIndex = 11;
            this.topRadioButton.TabStop = true;
            this.topRadioButton.Tag = "Top,Center";
            this.topRadioButton.UseVisualStyleBackColor = true;
            this.topRadioButton.CheckedChanged += new System.EventHandler(this.PositionChanged);
            // 
            // bottomRightRadioButton
            // 
            this.bottomRightRadioButton.AutoSize = true;
            this.bottomRightRadioButton.Location = new System.Drawing.Point(181, 118);
            this.bottomRightRadioButton.Name = "bottomRightRadioButton";
            this.bottomRightRadioButton.Size = new System.Drawing.Size(14, 13);
            this.bottomRightRadioButton.TabIndex = 10;
            this.bottomRightRadioButton.TabStop = true;
            this.bottomRightRadioButton.Tag = "Bottom,Right";
            this.bottomRightRadioButton.UseVisualStyleBackColor = true;
            this.bottomRightRadioButton.CheckedChanged += new System.EventHandler(this.PositionChanged);
            // 
            // topRightRadioButton
            // 
            this.topRightRadioButton.AutoSize = true;
            this.topRightRadioButton.Location = new System.Drawing.Point(180, 12);
            this.topRightRadioButton.Name = "topRightRadioButton";
            this.topRightRadioButton.Size = new System.Drawing.Size(14, 13);
            this.topRightRadioButton.TabIndex = 10;
            this.topRightRadioButton.TabStop = true;
            this.topRightRadioButton.Tag = "Top,Right";
            this.topRightRadioButton.UseVisualStyleBackColor = true;
            this.topRightRadioButton.CheckedChanged += new System.EventHandler(this.PositionChanged);
            // 
            // bottomLeftRadioButton
            // 
            this.bottomLeftRadioButton.AutoSize = true;
            this.bottomLeftRadioButton.Location = new System.Drawing.Point(6, 118);
            this.bottomLeftRadioButton.Name = "bottomLeftRadioButton";
            this.bottomLeftRadioButton.Size = new System.Drawing.Size(14, 13);
            this.bottomLeftRadioButton.TabIndex = 1;
            this.bottomLeftRadioButton.TabStop = true;
            this.bottomLeftRadioButton.Tag = "Bottom,Left";
            this.bottomLeftRadioButton.UseVisualStyleBackColor = true;
            this.bottomLeftRadioButton.CheckedChanged += new System.EventHandler(this.PositionChanged);
            // 
            // topLeftRadioButton
            // 
            this.topLeftRadioButton.AutoSize = true;
            this.topLeftRadioButton.Location = new System.Drawing.Point(6, 12);
            this.topLeftRadioButton.Name = "topLeftRadioButton";
            this.topLeftRadioButton.Size = new System.Drawing.Size(14, 13);
            this.topLeftRadioButton.TabIndex = 0;
            this.topLeftRadioButton.TabStop = true;
            this.topLeftRadioButton.Tag = "Top,Left";
            this.topLeftRadioButton.UseVisualStyleBackColor = true;
            this.topLeftRadioButton.CheckedChanged += new System.EventHandler(this.PositionChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 91);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(44, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Position";
            // 
            // marginNumericUpDown
            // 
            this.marginNumericUpDown.Location = new System.Drawing.Point(64, 54);
            this.marginNumericUpDown.Name = "marginNumericUpDown";
            this.marginNumericUpDown.Size = new System.Drawing.Size(61, 20);
            this.marginNumericUpDown.TabIndex = 7;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(131, 56);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(18, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "px";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 56);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(39, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Margin";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(23, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "File";
            // 
            // browseButton
            // 
            this.browseButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.browseButton.Image = ((System.Drawing.Image)(resources.GetObject("browseButton.Image")));
            this.browseButton.Location = new System.Drawing.Point(241, 18);
            this.browseButton.Name = "browseButton";
            this.browseButton.Size = new System.Drawing.Size(23, 23);
            this.browseButton.TabIndex = 1;
            this.browseButton.UseVisualStyleBackColor = true;
            this.browseButton.Click += new System.EventHandler(this.BrowseForImage);
            // 
            // fileTextBox
            // 
            this.fileTextBox.Location = new System.Drawing.Point(64, 20);
            this.fileTextBox.Name = "fileTextBox";
            this.fileTextBox.Size = new System.Drawing.Size(171, 20);
            this.fileTextBox.TabIndex = 0;
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(395, 244);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 6;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.Cancel);
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(314, 244);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 7;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.SaveSettings);
            // 
            // imageOpenFileDialog
            // 
            this.imageOpenFileDialog.Filter = "Image Files|*.bmp;*.jpg;*.png";
            this.imageOpenFileDialog.ReadOnlyChecked = true;
            // 
            // updateButton
            // 
            this.updateButton.Enabled = false;
            this.updateButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.updateButton.Image = ((System.Drawing.Image)(resources.GetObject("updateButton.Image")));
            this.updateButton.Location = new System.Drawing.Point(170, 100);
            this.updateButton.Name = "updateButton";
            this.updateButton.Size = new System.Drawing.Size(23, 23);
            this.updateButton.TabIndex = 8;
            this.updateButton.UseVisualStyleBackColor = true;
            this.updateButton.Click += new System.EventHandler(this.UpdateOverlay);
            // 
            // OverlaysForm
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(479, 277);
            this.Controls.Add(this.updateButton);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.deleteButton);
            this.Controls.Add(button3);
            this.Controls.Add(this.moveDownButton);
            this.Controls.Add(this.moveUpButton);
            this.Controls.Add(this.overlaysListBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "OverlaysForm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Image Overlays";
            this.Load += new System.EventHandler(this.FormLoaded);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.marginNumericUpDown)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox overlaysListBox;
        private System.Windows.Forms.Button moveUpButton;
        private System.Windows.Forms.Button moveDownButton;
        private System.Windows.Forms.Button deleteButton;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button browseButton;
        private System.Windows.Forms.TextBox fileTextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown marginNumericUpDown;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton topRadioButton;
        private System.Windows.Forms.RadioButton bottomRightRadioButton;
        private System.Windows.Forms.RadioButton topRightRadioButton;
        private System.Windows.Forms.RadioButton bottomLeftRadioButton;
        private System.Windows.Forms.RadioButton topLeftRadioButton;
        private System.Windows.Forms.RadioButton bottomRadioButton;
        private System.Windows.Forms.RadioButton rightRadioButton;
        private System.Windows.Forms.RadioButton centerRadioButton;
        private System.Windows.Forms.RadioButton leftRadioButton;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.OpenFileDialog imageOpenFileDialog;
        private System.Windows.Forms.Button updateButton;
    }
}