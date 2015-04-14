using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Text;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using System.Xml.Serialization;
using mshtml;

namespace Ventajou.WPInfo
{
    /// <summary>
    /// The main program window
    /// </summary>
    public partial class MainForm : Form
    {
        // Reference to the HTML document
        private IHTMLDocument2 htmlDoc;

        // Reference to the form used to render the wallbpaper
        private RenderForm _renderForm;

        public Size Resolution { get; set; }

        // Name of the configuration file.
        private string _fileName;
        public string FileName { get { return _fileName; } set { _fileName = value; } }         // Turn _fileName into a public property

        // Path where the program executable is located.
        private string _appPath;

        // Will contain the values that will replace the various tokens when the wallpaper is rendered.
        private Dictionary<string, string[]> _tokens;

        private void initHTMLEditor()
        {
            htmlEdit.DocumentText = "<html><body></body></html>";
            htmlDoc = htmlEdit.Document.DomDocument as IHTMLDocument2;    // Make the web 'browser' an editable HTML field
            htmlDoc.designMode = "on";
            //htmlDoc.body.innerHTML = Program.Settings.InfoHtml;            // Load blank or actual data
        }

        public MainForm()
        {
            InitializeComponent();
            Resolution = new Size(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
            initHTMLEditor();
        }

        public MainForm(Size Res)
        {
            InitializeComponent();
            Resolution = Res;
            initHTMLEditor();
        }

        #region Event Handlers
        /// <summary>
        /// Called when the main form is shown.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void ShowMainForm(object sender, EventArgs e)
        {
            // Populating the list of installed fonts.
            InstalledFontCollection fonts = new InstalledFontCollection();

            foreach (FontFamily fontFamily in fonts.Families)
            {
                FontComboBox.Items.Add(fontFamily.Name);
            }

            // Retrieve the available tokens and populate the toolstrip with them.
            _tokens = Program.GetTokens();
            foreach (KeyValuePair<string, string[]> token in _tokens)
            {
                ToolStripButton button = new ToolStripButton(token.Key);
                button.Click += new EventHandler(InsertToken);
                TokensToolStrip.Items.Add(button);
            }

            // Load configuration.
            LoadSettings();
        }

        /// <summary> 
        /// Insert the clicked token in the rich text box. 
        /// </summary> 
        /// <param name="sender">The sender.</param> 
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param> 
        private void InsertToken(object sender, EventArgs e)
        {
            ToolStripButton button = sender as ToolStripButton;

            if (button != null)
            {
                switch (button.Text)
                {
                    case Tokens.WMIData:
                        using (WMIQueryForm WQF = new WMIQueryForm())
                        {
                            if (WQF.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                                htmlInsert(Program.WrapTokenKey(TokenIDs.WMIData + "[" + WQF.listQueries.SelectedItem + "]"));
                            WQF.Close();
                        }
                        break;
                    case Tokens.WSHScript:
                        using (WSHScriptForm WSHF = new WSHScriptForm())
                        {
                            if (WSHF.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                                htmlInsert(Program.WrapTokenKey(TokenIDs.WSHScript + "[" + WSHF.listQueries.SelectedItem + "]"));
                            WSHF.Close();
                        }
                        break;
                    case Tokens.RegistryValue:
                        using (RegistryForm RF = new RegistryForm())
                        {
                            if (RF.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                                htmlInsert(Program.WrapTokenKey(TokenIDs.Registry + "[" + RF.listQueries.SelectedItem + "]"));
                            RF.Close();
                        }
                        break;
                    default:
                        htmlInsert(Program.WrapTokenKey(button.Text));
                        break;
                }
            }
        }

        private void htmlInsert(string s)
        {
            IHTMLTxtRange range = htmlDoc.selection.createRange() as IHTMLTxtRange;
            range.pasteHTML(s);
            range.collapse(false);
            range.select();
        }

        #region Toolbar Buttons

        private void OpenTextColorDialog(object sender, EventArgs e)
        {
            DialogResult result = TextColorDialog.ShowDialog();
            if (result == DialogResult.OK)
                htmlEdit.Document.ExecCommand("ForeColor", false, string.Format("#{0:X2}{1:X2}{2:X2}", TextColorDialog.Color.R, TextColorDialog.Color.G, TextColorDialog.Color.B));
        }

        private void AlignTextLeft(object sender, EventArgs e)
        {
            SetAlignmentButton(HorizontalAlignment.Left);
            htmlEdit.Document.ExecCommand("JustifyLeft", false, null);
        }

        private void AlignTextCenter(object sender, EventArgs e)
        {
            SetAlignmentButton(HorizontalAlignment.Center);
            htmlEdit.Document.ExecCommand("JustifyCenter", false, null);
        }

        private void AlignTextRight(object sender, EventArgs e)
        {
            SetAlignmentButton(HorizontalAlignment.Right);
            htmlEdit.Document.ExecCommand("JustifyRight", false, null);
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
            htmlEdit.Document.ExecCommand("Bold", false, null);
        }

        private void ItalicSelection(object sender, EventArgs e)
        {
            htmlEdit.Document.ExecCommand("Italic", false, null);
        }

        private void UnderlineSelection(object sender, EventArgs e)
        {
            htmlEdit.Document.ExecCommand("Underline", false, null);
        }

        private void SetFontSize(object sender, EventArgs e)
        {
            string sSize = FontSizeComboBox.SelectedItem.ToString();
            htmlEdit.Document.ExecCommand("FontSize", false, Int32.Parse(sSize.Substring(0, (sSize.IndexOf("-")>0 ? sSize.IndexOf("-") : sSize.Length))));
        }

        private void SetFont(object sender, EventArgs e)
        {
            htmlEdit.Document.ExecCommand("FontName", false, FontComboBox.SelectedItem.ToString());
        }

        private void ShowPreview(object sender, EventArgs e)
        {
            //Program.Settings.InfoText = LayoutRichTextBox.Rtf;
            Program.Settings.InfoHtml = htmlDoc.body.innerHTML;

            if (Resolution.IsEmpty)
                _renderForm = new RenderForm();
            else
                _renderForm = new RenderForm(Resolution);
            _renderForm.Show();
            _renderForm.Location = new Point(0, 0);
        }

        private void SetBackgroundColor(object sender, EventArgs e)
        {
            DialogResult result = TextColorDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                Program.Settings.BackgroundColor = SerializableColor.FromColor(TextColorDialog.Color);
                htmlDoc.bgColor = string.Format("#{0:X2}{1:X2}{2:X2}", TextColorDialog.Color.R, TextColorDialog.Color.G, TextColorDialog.Color.B);
            }
        }

        private void SetWallpaper(object sender, EventArgs e)
        {
            Program.Settings.InfoHtml = htmlDoc.body.innerHTML;
            _renderForm = new RenderForm();
            _renderForm.Show();
            _renderForm.Location = new Point(0, 0);

            UseWaitCursor = true;
            Program.SetWallpaper(_renderForm);
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
            htmlEdit.Document.ExecCommand("Cut", false, null);
        }

        private void CopyText(object sender, EventArgs e)
        {
            htmlEdit.Document.ExecCommand("Copy", false, null);
        }

        private void PasteText(object sender, EventArgs e)
        {
            htmlEdit.Document.ExecCommand("Paste", false, null);
        }

        private void SelectAllText(object sender, EventArgs e)
        {
            htmlEdit.Document.ExecCommand("SelectAll", false, null);
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
            using (BackgroundsFolderForm backgroundsFolder = new BackgroundsFolderForm())
            {
                backgroundsFolder.ShowDialog();
            }
        }

        private void OpenOverlaysForm(object sender, EventArgs e)
        {
            using (OverlaysForm overlaysForm = new OverlaysForm())
            {
                overlaysForm.ShowDialog();
            }
        }
        #endregion
        #endregion

        #region Helpers
        //private void FormatSelection()
        //{
        //  if (string.IsNullOrEmpty(FontComboBox.SelectedItem as string)
        //      || FontComboBox.SelectedItem == null
        //      || FontSizeComboBox.SelectedItem == null) return;

        //  FontStyle style = FontStyle.Regular;

        //  if (UnderlineToolStripButton.Checked) style |= FontStyle.Underline;
        //  if (BoldToolStripButton.Checked) style |= FontStyle.Bold;
        //  if (ItalicToolStripButton.Checked) style |= FontStyle.Italic;

        //  LayoutRichTextBox.SelectionFont = new Font(FontComboBox.SelectedItem as string,
        //      float.Parse(FontSizeComboBox.SelectedItem as string),
        //      style);
        //}

        private void LoadSettings()
        {
            htmlEdit.DocumentText = Program.Settings.InfoHtml;
            SetPositionButton(Program.Settings.ScreenPosition);
            //htmlDoc.BackColor = Program.Settings.BackgroundColor.ToColor();

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
            Program.Settings.InfoHtml = htmlDoc.body.innerHTML;
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

        private void editMarginToolStripButton_Click(object sender, EventArgs e)
        {
            var form = new TextMarginsForm();
            form.ShowDialog();
        }

        private void wMIQueriesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            WMIQueryForm WMI = new WMIQueryForm();
            DialogResult result = WMI.ShowDialog();
        }

        private void windowsScriptsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            WSHScriptForm WSH = new WSHScriptForm();
            DialogResult result = WSH.ShowDialog();
        }

        private void registryKeysToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RegistryForm R = new RegistryForm();
            DialogResult result = R.ShowDialog();
        }

        private void htmlEdit_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            (((sender as WebBrowser).Document.DomDocument as IHTMLDocument2).body as HTMLBody).contentEditable = "true";
        }

    }
}
