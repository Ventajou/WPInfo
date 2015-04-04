using System;
using System.Windows.Forms;

namespace Ventajou.WPInfo
{
    /// <summary>
    /// This form allows the users to pick where the generated image will be located.
    /// </summary>
    public partial class OutputSettingsForm : Form
    {
        #region Private Members
        OutputDestinations _outputDestination;
        #endregion

        public OutputSettingsForm()
        {
            InitializeComponent();
        }

        #region Event Handlers
        /// <summary>
        /// Called when the Windows Folder radio button is selected
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void WindowsFolderSelected(object sender, EventArgs e)
        {
            SelectOutputFolder(OutputDestinations.WindowsFolder);
        }

        /// <summary>
        /// Called when the User's Application Data folder radio button is selected
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AppDataFolderSelected(object sender, EventArgs e)
        {
            SelectOutputFolder(OutputDestinations.AppDataFolder);
        }

        /// <summary>
        /// Called when the User's Temporary Folder radio button is selected
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TempFolderSelected(object sender, EventArgs e)
        {
            SelectOutputFolder(OutputDestinations.TempFolder);
        }

        /// <summary>
        /// Called when the Custom Folder radio button is selected
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CustomFolderSelected(object sender, EventArgs e)
        {
            SelectOutputFolder(OutputDestinations.OtherFolder);
        }

        /// <summary>
        /// Called when the Ok button is pressed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UpdateOutputSettings(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(outputFileNameTextBox.Text))
            {
                MessageBox.Show("Please specify a file name", "Invalid data", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if (_outputDestination == OutputDestinations.OtherFolder)
            {
                if (string.IsNullOrEmpty(customFolderTextBox.Text))
                {
                    MessageBox.Show("Please specify a destination path", "Invalid data", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                Program.Settings.OtherFolder = customFolderTextBox.Text;
            }

            Program.Settings.OutputDestination = _outputDestination;
            Program.Settings.OutputFileName = outputFileNameTextBox.Text;

            Close();
        }

        /// <summary>
        /// Called when the Cancel Button is pressed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CancelPressed(object sender, EventArgs e)
        {
            Close();
        }

        /// <summary>
        /// Called when the form is loaded.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FormLoaded(object sender, EventArgs e)
        {
            outputFileNameTextBox.Text = Program.Settings.OutputFileName;
            customFolderTextBox.Text = Program.Settings.OtherFolder;

            windowsFolderRadioButton.Checked = Program.Settings.OutputDestination == OutputDestinations.WindowsFolder;
            appDataFolderRadioButton.Checked = Program.Settings.OutputDestination == OutputDestinations.AppDataFolder;
            tempFolderRadioButton.Checked = Program.Settings.OutputDestination == OutputDestinations.TempFolder;
            customFolderRadioButton.Checked = Program.Settings.OutputDestination == OutputDestinations.OtherFolder;

            radioBGCentered.Checked = Program.Settings.ImageMode == ImageModes.Centered;
            radioBGFit.Checked = Program.Settings.ImageMode == ImageModes.Fit;
            radioBGFill.Checked = Program.Settings.ImageMode == ImageModes.Fill;
            radioBGStretched.Checked = Program.Settings.ImageMode == ImageModes.Stretched;
            radioBGTiled.Checked = Program.Settings.ImageMode == ImageModes.Tiled;
        }
        #endregion

        #region Helpers
        /// <summary>
        /// Selects the output folder.
        /// </summary>
        /// <param name="destination">The destination.</param>
        private void SelectOutputFolder(OutputDestinations destination)
        {
            customFolderTextBox.Enabled = customFolderRadioButton.Checked;
            _outputDestination = destination;
        }
        #endregion

        private void radioBGCentered_CheckedChanged(object sender, EventArgs e)
        {
            Program.Settings.ImageMode = ImageModes.Centered;
        }

        private void radioBGStretched_CheckedChanged(object sender, EventArgs e)
        {
            Program.Settings.ImageMode = ImageModes.Stretched;
        }

        private void radioBGFit_CheckedChanged(object sender, EventArgs e)
        {
            Program.Settings.ImageMode = ImageModes.Fit;
        }

        private void radioBGFill_CheckedChanged(object sender, EventArgs e)
        {
            Program.Settings.ImageMode = ImageModes.Fill;
        }

        private void radioBGTiled_CheckedChanged(object sender, EventArgs e)
        {
            Program.Settings.ImageMode = ImageModes.Tiled;
        }

    }
}
