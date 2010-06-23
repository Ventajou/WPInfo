using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Drawing.Imaging;
using System.Windows.Forms.VisualStyles;
using System.Reflection;

namespace Ventajou.WPInfo
{
    /// <summary>
    /// This form is used to render the wallpaper both as a preview and prior to saving it.
    /// </summary>
    public partial class RenderForm : Form
    {
        TransparentRichTextBox _infoTextBox;
        private string _appPath;

        public RenderForm()
        {
            InitializeComponent();
        }

        #region Event Handlers
        /// <summary>
        /// Performs some initialization once the form is loaded
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FormLoaded(object sender, EventArgs e)
        {
            this.Size = Screen.PrimaryScreen.Bounds.Size;
            backgroundPictureBox.Bounds = Bounds;

            Bitmap background = GetBackground();

            if (Program.Settings.Overlays.Count > 0)
            {
                int screenWidth = Screen.PrimaryScreen.Bounds.Width;
                int screenHeight = Screen.PrimaryScreen.Bounds.Height;

                if (background == null) 
                {
                    background = new Bitmap(screenWidth, screenHeight);
                }

                Graphics backgroundGraphics = Graphics.FromImage(background);

                foreach (ImageOverlay overlay in Program.Settings.Overlays)
                {
                    try
                    {
                        using (Bitmap overlayBitmap = new Bitmap(GetRootedPath(overlay.FullPath)))
                        {
                            int top = 0;
                            int left = 0;

                            switch (overlay.HorizontalAlignment)
                            {
                                case HorizontalAlignment.Center:
                                    left = (screenWidth - overlayBitmap.Width) / 2;
                                    break;
                                case HorizontalAlignment.Left:
                                    left = overlay.Margin;
                                    break;
                                case HorizontalAlignment.Right:
                                    left = screenWidth - (overlay.Margin + overlayBitmap.Width);
                                    break;
                            }

                            switch (overlay.VerticalAlignment)
                            {
                                case VerticalAlignment.Bottom:
                                    top = screenHeight - (overlay.Margin + overlayBitmap.Height);
                                    break;
                                case VerticalAlignment.Center:
                                    top = (screenHeight - overlayBitmap.Height) / 2;
                                    break;
                                case VerticalAlignment.Top:
                                    top = overlay.Margin;
                                    break;
                            }

                            backgroundGraphics.DrawImageUnscaled(overlayBitmap, left, top);
                        }
                    }
                    catch (Exception) { }
                }

                //backgroundGraphics.Flush();
                //backgroundGraphics.Save();
                //backgroundGraphics.Dispose();
            }

            if (background != null)
            {
                backgroundPictureBox.Image = background;
            }

            _infoTextBox = new TransparentRichTextBox();
            SuspendLayout();

            _infoTextBox.AcceptsTab = true;
            _infoTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            _infoTextBox.Name = "InfoRichTextBox";
            _infoTextBox.ReadOnly = true;
            _infoTextBox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            _infoTextBox.TabStop = false;
            _infoTextBox.Cursor = System.Windows.Forms.Cursors.Default;
            _infoTextBox.MouseDown += CloseForm;
            _infoTextBox.ContentsResized += TextBoxContentsResized;
            backgroundPictureBox.Controls.Add(_infoTextBox);

            _infoTextBox.Rtf = Program.SubstituteTokens();
            BackColor = Program.Settings.BackgroundColor.ToColor();
        }

        /// <summary>
        /// Called when the rich text box's content has been modified
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextBoxContentsResized(object sender, ContentsResizedEventArgs e)
        {
            // Resizing the RichTextBox according to its content
            // The width is hard coded at 500px for now
            _infoTextBox.Size = new Size(500, e.NewRectangle.Size.Height);

            int height = Program.Settings.IgnoreTaskBar ? Screen.PrimaryScreen.Bounds.Height : Screen.PrimaryScreen.WorkingArea.Bottom - Screen.PrimaryScreen.Bounds.Top;

            // adjusting the RichTextBox location based on the user defined screen position
            switch (Program.Settings.ScreenPosition)
            {
                case ScreenPositions.TopLeft:
                    _infoTextBox.Location = new Point(ProgramSettings.Margin, ProgramSettings.Margin);
                    break;
                case ScreenPositions.TopRight:
                    _infoTextBox.Location = new Point(Screen.PrimaryScreen.Bounds.Width - (_infoTextBox.Width + ProgramSettings.Margin), ProgramSettings.Margin);
                    break;
                case ScreenPositions.BottomLeft:
                    _infoTextBox.Location = new Point(ProgramSettings.Margin, height - (_infoTextBox.Height + ProgramSettings.Margin));
                    break;
                case ScreenPositions.BottomRight:
                    _infoTextBox.Location = new Point(Screen.PrimaryScreen.Bounds.Width - (_infoTextBox.Width + ProgramSettings.Margin), height - (_infoTextBox.Height + ProgramSettings.Margin));
                    break;
            }
        }

        /// <summary>
        /// Called when the mouse button is pressed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CloseForm(object sender, MouseEventArgs e)
        {
            Close();
            Dispose();
        }
        #endregion

        #region Helpers
        private Bitmap GetBackground()
        {
            if (!Program.Settings.UseBackgroundsFolder) return null;

            int screenWidth = Screen.PrimaryScreen.Bounds.Width;
            int screenHeight = Screen.PrimaryScreen.Bounds.Height;

            string[] resourceFiles = Directory.GetFiles(GetRootedPath(Program.Settings.BackgroundsFolder));

            int bestWidth = int.MaxValue;
            int bestHeight = int.MaxValue;
            string bestImageName = null;
            int maxWidth = 0;
            int maxHeight = 0;
            string biggestImage = null;

            foreach (string file in resourceFiles)
            {
                try
                {
                    using (Bitmap image = new Bitmap(GetRootedPath(file)))
                    {
                        if (image.Width >= screenWidth && image.Height >= screenHeight)
                        {
                            if (image.Width <= bestWidth && image.Height <= bestHeight ||
                                image.Width * image.Height <= bestWidth * bestHeight)
                            {
                                bestWidth = image.Width;
                                bestHeight = image.Height;
                                bestImageName = file;
                            }
                        }

                        if (image.Width >= maxWidth && image.Height >= maxHeight)
                        {
                            maxWidth = image.Width;
                            maxHeight = image.Height;
                            biggestImage = file;
                        }
                    }
                }
                catch (Exception) { }
            }

            if (string.IsNullOrEmpty(bestImageName)) return null;

            Bitmap bestImage = new Bitmap(bestImageName);

            Bitmap outputBitmap = new Bitmap(bestImage, screenWidth, screenHeight);
            return outputBitmap;
        }

        public string GetRootedPath(string path)
        {
            if (Path.IsPathRooted(path)) return path;

            if (string.IsNullOrEmpty(_appPath))
            {
                _appPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            }

            return Path.Combine(_appPath, path);
        }
        #endregion
    }
}
