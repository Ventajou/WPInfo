﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Text;
using System.IO;
using System.Windows.Forms;
using System.Xml.Serialization;
using System.Reflection;

namespace Ventajou.WPInfo
{
    public partial class MainForm : Form
    {
        private RenderForm _renderForm;
        private string _fileName;
        private string _appPath;

        private Dictionary<string, string[]> _tokens;
        public MainForm()
        {
            InitializeComponent();
        }

        #region Event Handlers
        private void ShowMainForm(object sender, EventArgs e)
        {
            InstalledFontCollection fonts = new InstalledFontCollection();

            foreach (FontFamily fontFamily in fonts.Families)
            {
                FontComboBox.Items.Add(fontFamily.Name);
            }

            _tokens = Program.GetTokens();
            foreach (KeyValuePair<string, string[]> token in _tokens)
            {
                ToolStripButton button = new ToolStripButton(token.Key);
                button.Click += new EventHandler(InsertToken);
                TokensToolStrip.Items.Add(button);
            }

            LoadSettings();
        }

        private void InsertToken(object sender, EventArgs e)
        {
            ToolStripButton button = sender as ToolStripButton;

            if (button != null)
            {
                LayoutRichTextBox.SelectedText = Program.WrapTokenKey(button.Text);
            }
        }

        private void TextBoxSelectionChanged(object sender, EventArgs e)
        {
            //HACK: I had exceptions on Windows XP which I couldn't reproduce on my Windows 7 development box. There's probably a cleaner way to handle them though.
            try
            {
                BoldToolStripButton.Checked = LayoutRichTextBox.SelectionFont.Bold;
                ItalicToolStripButton.Checked = LayoutRichTextBox.SelectionFont.Italic;
                UnderlineToolStripButton.Checked = LayoutRichTextBox.SelectionFont.Underline;

                SetAlignmentButton(LayoutRichTextBox.SelectionAlignment);

                FontComboBox.SelectedIndex = FontComboBox.FindString(LayoutRichTextBox.SelectionFont.FontFamily.Name);
                FontSizeComboBox.SelectedIndex = FontSizeComboBox.FindString(LayoutRichTextBox.SelectionFont.SizeInPoints.ToString());
            }
            catch (Exception) { }
        }

        #region Toolbar Buttons

        private void OpenTextColorDialog(object sender, EventArgs e)
        {
            DialogResult result = TextColorDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                LayoutRichTextBox.SelectionColor = TextColorDialog.Color;
            }
        }

        private void AlignTextLeft(object sender, EventArgs e)
        {
            SetAlignmentButton(HorizontalAlignment.Left);
            LayoutRichTextBox.SelectionAlignment = HorizontalAlignment.Left;
        }

        private void AlignTextCenter(object sender, EventArgs e)
        {
            SetAlignmentButton(HorizontalAlignment.Center);
            LayoutRichTextBox.SelectionAlignment = HorizontalAlignment.Center;
        }

        private void AlignTextRight(object sender, EventArgs e)
        {
            SetAlignmentButton(HorizontalAlignment.Right);
            LayoutRichTextBox.SelectionAlignment = HorizontalAlignment.Right;
        }

        private void PlaceInfoTextTopLeft(object sender, EventArgs e)
        {
            SetPositionButton(ScreenPositions.TopLeft);
        }

        private void PlaceInfoTextTopRight(object sender, EventArgs e)
        {
            SetPositionButton(ScreenPositions.TopRight);
        }

        private void PlaceInfoTextBottomLeft(object sender, EventArgs e)
        {
            SetPositionButton(ScreenPositions.BottomLeft);
        }

        private void PlaceInfoTextBottomRight(object sender, EventArgs e)
        {
            SetPositionButton(ScreenPositions.BottomRight);
        }

        private void BoldSelection(object sender, EventArgs e)
        {
            LayoutRichTextBox.SelectionFont = new Font(LayoutRichTextBox.SelectionFont,
                LayoutRichTextBox.SelectionFont.Style ^ FontStyle.Bold);
        }

        private void ItalicSelection(object sender, EventArgs e)
        {
            LayoutRichTextBox.SelectionFont = new Font(LayoutRichTextBox.SelectionFont,
                LayoutRichTextBox.SelectionFont.Style ^ FontStyle.Italic);
        }

        private void UnderlineSelection(object sender, EventArgs e)
        {
            LayoutRichTextBox.SelectionFont = new Font(LayoutRichTextBox.SelectionFont,
                LayoutRichTextBox.SelectionFont.Style ^ FontStyle.Underline);
        }

        private void SetFontSize(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(FontSizeComboBox.SelectedItem as string)) return;

            LayoutRichTextBox.SelectionFont = new Font(LayoutRichTextBox.SelectionFont.FontFamily.Name,
                float.Parse(FontSizeComboBox.SelectedItem as string),
                LayoutRichTextBox.SelectionFont.Style);
        }

        private void SetFont(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(FontComboBox.SelectedItem as string)) return;

            LayoutRichTextBox.SelectionFont = new Font(FontComboBox.SelectedItem as string,
                LayoutRichTextBox.SelectionFont.Size,
                LayoutRichTextBox.SelectionFont.Style);
        }

        private void ShowPreview(object sender, EventArgs e)
        {
            Program.Settings.InfoText = LayoutRichTextBox.Rtf;
            _renderForm = new RenderForm();
            _renderForm.Show();
            _renderForm.Location = new Point(0, 0);
        }

        private void SetBackgroundColor(object sender, EventArgs e)
        {
            DialogResult result = TextColorDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                Program.Settings.BackgroundColor = SerializableColor.FromColor(TextColorDialog.Color);
                LayoutRichTextBox.BackColor = TextColorDialog.Color;
            }
        }

        private void SetWallpaper(object sender, EventArgs e)
        {
            UseWaitCursor = true;
            Program.Settings.InfoText = LayoutRichTextBox.Rtf;
            Program.SetWallpaper();
            UseWaitCursor = false;
        }
        #endregion

        #region File Menu
        private void NewDocument(object sender, EventArgs e)
        {
            Program.Settings.ResetValues();
            LoadSettings();
        }

        private void OpenDocument(object sender, EventArgs e)
        {
            DialogResult result = SettingsOpenFileDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                try
                {
                    _fileName = SettingsOpenFileDialog.FileName;
                    Program.LoadFile(_fileName);
                    LoadSettings();
                }
                catch (Exception)
                {
                    MessageBox.Show("An error has occured while attempting to load the settings file.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void SaveDocument(object sender, EventArgs e)
        {
            SaveDocument(_fileName);
        }

        private void SaveDocumentAs(object sender, EventArgs e)
        {
            SaveDocument(null);
        }

        private void ExitProgram(object sender, EventArgs e)
        {
            Application.Exit();
        }
        #endregion

        #region Edit Menu
        private void CutText(object sender, EventArgs e)
        {
            LayoutRichTextBox.Cut();
        }

        private void CopyText(object sender, EventArgs e)
        {
            LayoutRichTextBox.Copy();
        }

        private void PasteText(object sender, EventArgs e)
        {
            LayoutRichTextBox.Paste();
        }

        private void SelectAllText(object sender, EventArgs e)
        {
            LayoutRichTextBox.SelectAll();
        }
        #endregion

        #region Options Menu
        private void Set16bppOutput(object sender, EventArgs e)
        {
            SetOutputDepth(OutputDepths.SixteenBitsPerPixel);
        }

        private void Set24bppOutput(object sender, EventArgs e)
        {
            SetOutputDepth(OutputDepths.TwentyFourBitsPerPixel);
        }

        private void SetCurrentDepthOutput(object sender, EventArgs e)
        {
            SetOutputDepth(OutputDepths.CurrentDepth);
        }

        private void OpenOutputSettings(object sender, EventArgs e)
        {
            OutputSettingsForm settingsForm = new OutputSettingsForm();
            settingsForm.ShowDialog();
        }

        private void ignoreTaskbarHeightToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Program.Settings.IgnoreTaskBar = ignoreTaskbarHeightToolStripMenuItem.Checked;
        }
        #endregion

        #region Images Menu
        private void OpenBackgroundsFolderOptions(object sender, EventArgs e)
        {
            using (BackgroundsFolder backgroundsFolder = new BackgroundsFolder())
            {
                backgroundsFolder.ShowDialog();
            }
        }

        private void OpenOverlaysForm(object sender, EventArgs e)
        {
            using(OverlaysForm overlaysForm = new OverlaysForm())
            {
                overlaysForm.ShowDialog();
            }
        }
        #endregion
        #endregion

        #region Helpers
        private void LoadSettings()
        {
            LayoutRichTextBox.Rtf = Program.Settings.InfoText;
            SetPositionButton(Program.Settings.ScreenPosition);
            LayoutRichTextBox.BackColor = Program.Settings.BackgroundColor.ToColor();

            FontComboBox.SelectedIndex = FontComboBox.FindString("Arial");
            FontSizeComboBox.SelectedIndex = 2;

            SetOutputDepth(Program.Settings.OutputDepth);

            ignoreTaskbarHeightToolStripMenuItem.Checked = Program.Settings.IgnoreTaskBar;

            trySavingRelativePathsToolStripMenuItem.Checked = Program.Settings.SaveRelativeImagePaths;
        }

        private void SetOutputDepth(OutputDepths depth)
        {
            sixteenBppToolStripMenuItem.Checked = depth == OutputDepths.SixteenBitsPerPixel;
            twentyFourBppToolStripMenuItem1.Checked = depth == OutputDepths.TwentyFourBitsPerPixel;
            currentDepthToolStripMenuItem.Checked = depth == OutputDepths.CurrentDepth;
        }

        private void SetAlignmentButton(HorizontalAlignment alignment)
        {
            AlignCenterToolStripButton.Checked = alignment == HorizontalAlignment.Center;
            AlignLeftToolStripButton.Checked = alignment == HorizontalAlignment.Left;
            AlignRightToolStripButton.Checked = alignment == HorizontalAlignment.Right;
        }

        private void SetPositionButton(ScreenPositions position)
        {
            TopLeftToolStripButton.Checked = position == ScreenPositions.TopLeft;
            TopRightToolStripButton.Checked = position == ScreenPositions.TopRight;
            BottomLeftToolStripButton.Checked = position == ScreenPositions.BottomLeft;
            BottomRightToolStripButton.Checked = position == ScreenPositions.BottomRight;

            Program.Settings.ScreenPosition = position;
        }

        private void SaveDocument(string fileName)
        {
            Program.Settings.InfoText = LayoutRichTextBox.Rtf;
            Program.Settings.SaveRelativeImagePaths = trySavingRelativePathsToolStripMenuItem.Checked;

            if (Program.Settings.SaveRelativeImagePaths)
            {
                if (Program.Settings.UseBackgroundsFolder) Program.Settings.BackgroundsFolder = MakePathRelative(Program.Settings.BackgroundsFolder);
                foreach (ImageOverlay overlay in Program.Settings.Overlays)
                {
                    overlay.FullPath = MakePathRelative(overlay.FullPath);
                }
            }

            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(ProgramSettings));
                Stream file;

                if (string.IsNullOrEmpty(fileName))
                {
                    DialogResult result = SettingsSaveFileDialog.ShowDialog();

                    if (result == DialogResult.OK)
                    {

                        file = SettingsSaveFileDialog.OpenFile();
                        _fileName = SettingsSaveFileDialog.FileName;
                        serializer.Serialize(file, Program.Settings);
                        file.Close();
                    }
                }
                else
                {
                    file = new FileStream(fileName, FileMode.Create);
                    serializer.Serialize(file, Program.Settings);
                    file.Close();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("An error has occured while attempting to save the settings file.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string MakePathRelative(string path)
        {
            if (string.IsNullOrEmpty(_appPath))
            {
                _appPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            }

            if (path.StartsWith(_appPath)) return path.Substring(_appPath.Length + 1);

            return path;
        }
        #endregion
    }
}