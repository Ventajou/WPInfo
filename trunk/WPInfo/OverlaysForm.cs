using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Windows.Forms.VisualStyles;

namespace Ventajou.WPInfo
{
    public partial class OverlaysForm : Form
    {
        HorizontalAlignment _horizontalAlignment;
        VerticalAlignment _verticalAlignment;

        BindingList<ImageOverlay> _overlays = new BindingList<ImageOverlay>();

        public OverlaysForm()
        {
            InitializeComponent();

            overlaysListBox.DisplayMember = "FileName";
            overlaysListBox.DataSource = _overlays;
        }

        private void SaveSettings(object sender, EventArgs e)
        {
            Program.Settings.Overlays.Clear();
            Program.Settings.Overlays.AddRange(_overlays);
            Close();
        }

        private void Cancel(object sender, EventArgs e)
        {
            Close();
        }

        private void NewOverlay(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(fileTextBox.Text))
            {
                MessageBox.Show("You need to specify an image file.", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if (!topLeftRadioButton.Checked &&
                !topRadioButton.Checked &&
                !topRightRadioButton.Checked &&
                !leftRadioButton.Checked &&
                !centerRadioButton.Checked &&
                !rightRadioButton.Checked &&
                !bottomLeftRadioButton.Checked &&
                !bottomRadioButton.Checked &&
                !bottomRightRadioButton.Checked)
            {
                MessageBox.Show("You need to specify a screen position.", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            ImageOverlay overlay = new ImageOverlay();

            overlay.FullPath = fileTextBox.Text;
            overlay.FileName = Path.GetFileName(overlay.FullPath);
            overlay.VerticalAlignment = _verticalAlignment;
            overlay.HorizontalAlignment = _horizontalAlignment;
            overlay.Margin = (int)marginNumericUpDown.Value;
            _overlays.Add(overlay);

            ClearFields();
        }

        private void DeleteOverlay(object sender, EventArgs e)
        {
            if (MessageBox.Show("Remove this overlay?", "Delete Overlay", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                _overlays.Remove((ImageOverlay)overlaysListBox.SelectedItem);
                ClearFields();
            }
        }

        private void UpdateOverlay(object sender, EventArgs e)
        {
            ImageOverlay overlay = overlaysListBox.SelectedItem as ImageOverlay;
            overlay.FullPath = fileTextBox.Text;
            overlay.FileName = Path.GetFileName(overlay.FullPath);
            overlay.VerticalAlignment = _verticalAlignment;
            overlay.HorizontalAlignment = _horizontalAlignment;
            overlay.Margin = (int)marginNumericUpDown.Value;
            _overlays.ResetItem(_overlays.IndexOf(overlay));

            ClearFields();
        }

        private void FormLoaded(object sender, EventArgs e)
        {
            _overlays.Clear();
            foreach (ImageOverlay overlay in Program.Settings.Overlays) _overlays.Add((ImageOverlay)overlay.Clone());
            ClearFields();
        }

        private void PositionChanged(object sender, EventArgs e)
        {
            RadioButton radioButton = sender as RadioButton;

            string[] tags = radioButton.Tag.ToString().Split(',');
            _verticalAlignment = (VerticalAlignment)Enum.Parse(typeof(VerticalAlignment), tags[0], true);
            _horizontalAlignment = (HorizontalAlignment)Enum.Parse(typeof(HorizontalAlignment), tags[1], true);
        }

        private void BrowseForImage(object sender, EventArgs e)
        {
            if (imageOpenFileDialog.ShowDialog() == DialogResult.OK)
            {
                fileTextBox.Text = imageOpenFileDialog.FileName;
            }
        }

        private void OverlaySelected(object sender, EventArgs e)
        {
            ImageOverlay overlay = overlaysListBox.SelectedItem as ImageOverlay;

            if (overlay != null)
            {
                fileTextBox.Text = overlay.FullPath;
                marginNumericUpDown.Value = overlay.Margin;
                moveDownButton.Enabled = _overlays[_overlays.Count - 1] != overlay;
                moveUpButton.Enabled = _overlays[0] != overlay;

                // Yeah it's ugly, but it works
                switch (overlay.VerticalAlignment)
                {
                    case VerticalAlignment.Bottom:
                        switch (overlay.HorizontalAlignment)
                        {
                            case HorizontalAlignment.Center:
                                bottomRadioButton.Checked = true;
                                break;
                            case HorizontalAlignment.Left:
                                bottomLeftRadioButton.Checked = true;
                                break;
                            case HorizontalAlignment.Right:
                                bottomRightRadioButton.Checked = true;
                                break;
                        }
                        break;

                    case VerticalAlignment.Center:
                        switch (overlay.HorizontalAlignment)
                        {
                            case HorizontalAlignment.Center:
                                centerRadioButton.Checked = true;
                                break;
                            case HorizontalAlignment.Left:
                                leftRadioButton.Checked = true;
                                break;
                            case HorizontalAlignment.Right:
                                rightRadioButton.Checked = true;
                                break;
                        }
                        break;

                    case VerticalAlignment.Top:
                        switch (overlay.HorizontalAlignment)
                        {
                            case HorizontalAlignment.Center:
                                topRadioButton.Checked = true;
                                break;
                            case HorizontalAlignment.Left:
                                topLeftRadioButton.Checked = true;
                                break;
                            case HorizontalAlignment.Right:
                                topRightRadioButton.Checked = true;
                                break;
                        }
                        break;
                }
            }

            deleteButton.Enabled = true;
            updateButton.Enabled = true;
        }

        private void ClearFields()
        {
            topLeftRadioButton.Checked = false;
            topRadioButton.Checked = false;
            topRightRadioButton.Checked = false;
            leftRadioButton.Checked = false;
            centerRadioButton.Checked = false;
            rightRadioButton.Checked = false;
            bottomLeftRadioButton.Checked = false;
            bottomRadioButton.Checked = false;
            bottomRightRadioButton.Checked = false;
            fileTextBox.Text = string.Empty;
            marginNumericUpDown.Value = 0;

            overlaysListBox.SelectedItem = null;
            deleteButton.Enabled = false;
            updateButton.Enabled = false;
            moveDownButton.Enabled = false;
            moveUpButton.Enabled = false;
        }

        private void MoveOverlayUp(object sender, EventArgs e)
        {
            ImageOverlay overlay = overlaysListBox.SelectedItem as ImageOverlay;
            int index = _overlays.IndexOf(overlay);
            _overlays.Remove(overlay);
            _overlays.Insert(index - 1, overlay);
            overlaysListBox.SelectedItem = overlay;
        }

        private void MoveOverlayDown(object sender, EventArgs e)
        {
            ImageOverlay overlay = overlaysListBox.SelectedItem as ImageOverlay;
            int index = _overlays.IndexOf(overlay);
            _overlays.Remove(overlay);
            _overlays.Insert(index + 1, overlay);
            overlaysListBox.SelectedItem = overlay;
        }
    }
}
