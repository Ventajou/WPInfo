using System;
using System.Windows.Forms;

namespace Ventajou.WPInfo
{
    /// <summary>
    /// Form used to select the folder which contains 
    /// </summary>
    public partial class BackgroundsFolderForm : Form
    {
        public BackgroundsFolderForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Enables the backgrounds folder text box based on the state of UseFolderChecBox.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void EnableBackgroundsFolder(object sender, EventArgs e)
        {
            backgroundsFolderButton.Enabled = 
                backgroundsFolderTextBox.Enabled =
                showBoxCheckBox.Enabled =
                opacityTrackBar.Enabled = useFolderCheckBox.Checked;
            if (!useFolderCheckBox.Checked) backgroundsFolderTextBox.Text = string.Empty;
        }

        /// <summary>
        /// Opens a folder browser dialog when the browse button is clicked.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void BrowseForBackgroundsFolder(object sender, EventArgs e)
        {
            DialogResult result = folderBrowserDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                backgroundsFolderTextBox.Text = folderBrowserDialog.SelectedPath;
            }
        }

        /// <summary>
        /// Cancels the dialog.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void CancelDialog(object sender, EventArgs e)
        {
            Close();
        }

        /// <summary>
        /// Saves the settings.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void SaveSettings(object sender, EventArgs e)
        {
            Program.Settings.UseBackgroundsFolder = useFolderCheckBox.Checked;
            Program.Settings.BackgroundsFolder = backgroundsFolderTextBox.Text;
            Program.Settings.ShowTextBox = showBoxCheckBox.Checked;
            Program.Settings.TextBoxOpacity = (byte)opacityTrackBar.Value;
            Close();
        }

        /// <summary>
        /// Called when the form is shown.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void ShowForm(object sender, EventArgs e)
        {
            backgroundsFolderTextBox.Enabled = backgroundsFolderButton.Enabled = useFolderCheckBox.Checked = Program.Settings.UseBackgroundsFolder;
            backgroundsFolderTextBox.Text = Program.Settings.BackgroundsFolder;
            showBoxCheckBox.Checked = Program.Settings.ShowTextBox;
            opacityTrackBar.Value = Program.Settings.TextBoxOpacity;
        }
    }
}
