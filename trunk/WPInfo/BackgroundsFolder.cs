using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Ventajou.WPInfo
{
    public partial class BackgroundsFolder : Form
    {
        public BackgroundsFolder()
        {
            InitializeComponent();
        }

        private void EnableBackgroundsFolder(object sender, EventArgs e)
        {
            backgroundsFolderButton.Enabled = backgroundsFolderTextBox.Enabled = UseFolderCheckBox.Checked;
            if (!UseFolderCheckBox.Checked) backgroundsFolderTextBox.Text = string.Empty;
        }

        private void BrowseForBackgroundsFolder(object sender, EventArgs e)
        {
            DialogResult result = folderBrowserDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                backgroundsFolderTextBox.Text = folderBrowserDialog.SelectedPath;
            }
        }

        private void CancelDialog(object sender, EventArgs e)
        {
            Close();
        }

        private void SaveSettings(object sender, EventArgs e)
        {
            Program.Settings.UseBackgroundsFolder = UseFolderCheckBox.Checked;
            Program.Settings.BackgroundsFolder = backgroundsFolderTextBox.Text;
            Close();
        }

        private void ShowForm(object sender, EventArgs e)
        {
            backgroundsFolderTextBox.Enabled = backgroundsFolderButton.Enabled = UseFolderCheckBox.Checked = Program.Settings.UseBackgroundsFolder;
            backgroundsFolderTextBox.Text = Program.Settings.BackgroundsFolder;
        }
    }
}
