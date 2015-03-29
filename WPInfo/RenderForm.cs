using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Drawing.Imaging;
using System.Windows.Forms.VisualStyles;
using System.Reflection;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Ventajou.WPInfo
{
    /// <summary>
    /// This form is used to render the wallpaper both as a preview and prior to saving it.
    /// </summary>
    public partial class RenderForm : Form
    {
        // This control is used to render the text
        TransparentRichTextBox _infoTextBox;

        // Path to the executable, used to resolve relative paths
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
            Graphics backgroundGraphics = null;

            // take the whole screen size
            this.Size = Screen.PrimaryScreen.Bounds.Size;

            // size the background pic accordingly
            backgroundPictureBox.Bounds = Bounds;

            // Get the background image
            Bitmap background = GetBackground();

            if (background != null)
            {
                // Getting a graphics object to draw the overlays on
                backgroundGraphics = Graphics.FromImage(background);
            }

            // Renedering the overlays
            if (Program.Settings.Overlays.Count > 0)
            {
                int screenWidth = Screen.PrimaryScreen.Bounds.Width;
                int screenHeight = Screen.PrimaryScreen.Bounds.Height;

                // We have to ensure there is a background to render to
                if (background == null)
                {
                    background = new Bitmap(screenWidth, screenHeight);
                }

                // Loop through the overlay images
                foreach (ImageOverlay overlay in Program.Settings.Overlays)
                {
                    // This is not very pretty but it will keep errors from screwing up the rendering
                    try
                    {
                        using (Bitmap overlayBitmap = new Bitmap(GetRootedPath(overlay.FullPath)))
                        {
                            int top = 0;
                            int left = 0;

                            // Calculating the horizontal position of the overlay
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

                            // Calculating the vertical position of the overlay
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

                            // Drawing the overlay
                            backgroundGraphics.DrawImage(overlayBitmap, left, top, overlayBitmap.Width, overlayBitmap.Height);
                        }
                    }
                    catch (Exception) { }
                }
            }

            // Creating the transparent text box and adding it to the background
            _infoTextBox = new TransparentRichTextBox();
            _infoTextBox.AcceptsTab = true;
            _infoTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            _infoTextBox.Name = "InfoRichTextBox";
            _infoTextBox.ReadOnly = true;
            _infoTextBox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            _infoTextBox.TabStop = false;
            _infoTextBox.Cursor = System.Windows.Forms.Cursors.Default;
            _infoTextBox.MouseDown += CloseForm;
            _infoTextBox.ContentsResized += TextBoxContentsResized;
            _infoTextBox.DetectUrls = false;

            backgroundPictureBox.Controls.Add(_infoTextBox);

            // fill the text box with the information text
            SubstituteTokens();

            // Setting the form's background in case no image is specified
            BackColor = Program.Settings.BackgroundColor.ToColor();

            // applying the background image
            if (background != null)
            {
                if (Program.Settings.ShowTextBox)
                {
                    backgroundGraphics.FillRectangle(
                        new SolidBrush(Color.FromArgb(Program.Settings.TextBoxOpacity, Program.Settings.BackgroundColor.ToColor())),
                         _infoTextBox.Bounds.X - 10,
                        _infoTextBox.Bounds.Y - 10,
                        _infoTextBox.Bounds.Width + 20,
                        _infoTextBox.Bounds.Height + 20);
                }

                backgroundPictureBox.Image = background;
            }
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
                    _infoTextBox.Location = new Point(Program.Settings.HorizontalMargin, Program.Settings.VerticalMargin);
                    break;
                case ScreenPositions.TopRight:
                    _infoTextBox.Location = new Point(Screen.PrimaryScreen.Bounds.Width - (_infoTextBox.Width + Program.Settings.HorizontalMargin), Program.Settings.VerticalMargin);
                    break;
                case ScreenPositions.BottomLeft:
                    _infoTextBox.Location = new Point(Program.Settings.HorizontalMargin, height - (_infoTextBox.Height + Program.Settings.VerticalMargin));
                    break;
                case ScreenPositions.BottomRight:
                    _infoTextBox.Location = new Point(Screen.PrimaryScreen.Bounds.Width - (_infoTextBox.Width + Program.Settings.HorizontalMargin), height - (_infoTextBox.Height + Program.Settings.VerticalMargin));
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
        /// <summary>
        /// Substitutes the tokens with the correct data and renders the text.
        /// </summary>
        public void SubstituteTokens()
        {
            // RTF uses twips as measuring units. We calculate those using the main screen's DPI
            int twipPerPixel;
            using (Graphics g = CreateGraphics())
            {
                twipPerPixel = (int)(1440 / g.DpiX);
            }

            // Putting the text with tokens in the text box for calculation purpose
            _infoTextBox.Rtf = Program.Settings.InfoText;

            // retrieving the tokens and their values
            Dictionary<string, string[]> tokens = Program.GetTokens();

            // This regular expression will find tokens in the information text
            Regex tokenRegex = new Regex("<% (?<token>.*?) %>");

            // This will find the next line break after a multi value token.
            Regex nextLineRegex = new Regex(@"(?<break>\\line|\\par)[^d]");

            // Look for the first token
            Match match = tokenRegex.Match(_infoTextBox.Rtf);

            // We loop over tokens in order of appearance in the text, this ensures the indentation can be correctly calculated for multi-line values
            while (match.Success)
            {
                string[] tokenValues = tokens[match.Groups[1].Value];

                // If the token has more than one values, they will each be placed on a new text line
                if (tokenValues.Length > 1)
                {
                    // This calculates the position of the token's first character relative to the text box.
                    // The method uses the plain text for character index, not the RTF text
                    Point position = _infoTextBox.GetPositionFromCharIndex(tokenRegex.Match(_infoTextBox.Text).Index);
                    // Converting the pixel position into twips which is what RTF uses
                    int indent = twipPerPixel * (position.X - 1);

                    // Next we loop over the token values and build a string
                    StringBuilder sb = new StringBuilder();
                    foreach (string value in tokenValues)
                    {
                        // \par closes a paragraph and \pard opens a new one, \li sets the indentation to the specified number of twips
                        if (sb.Length > 0) sb.Append(@"\par\pard\li" + indent + " ");
                        sb.Append(value);
                    }

                    // Get the information text length before replacing the token
                    int oldLength = _infoTextBox.Rtf.Length;

                    // Replace the token with its values
                    _infoTextBox.Rtf = tokenRegex.Replace(_infoTextBox.Rtf, sb.ToString(), 1, match.Index);

                    // Finding the next line break in order to reset the indentation, we have to look right after the newly added values
                    Match nextLineMatch = nextLineRegex.Match(_infoTextBox.Rtf, match.Index + match.Value.Length + (_infoTextBox.Rtf.Length - oldLength));
                    if (nextLineMatch.Success)
                    {
                        // If the line break is found, add a zero indentation right after it
                        _infoTextBox.Rtf = nextLineRegex.Replace(_infoTextBox.Rtf, nextLineMatch.Groups[1].Value + @"\li0 ", 1, nextLineMatch.Index);
                    }
                }
                else if (tokenValues.Length == 1)
                {
                    // When there is a single value, all we do is replace the token with it
                    _infoTextBox.Rtf = tokenRegex.Replace(_infoTextBox.Rtf, tokenValues[0], 1, match.Index);
                }

                // Find the next token
                match = tokenRegex.Match(_infoTextBox.Rtf, match.Index);
            }
        }

        /// <summary>
        /// Gets the background image most appropriate for the current resolution of the primary display.
        /// </summary>
        /// <returns></returns>
        private Bitmap GetBackground()
        {
            // Cut it short if no folder is configured
            if (!Program.Settings.UseBackgroundsFolder || !Directory.Exists(GetRootedPath(Program.Settings.BackgroundsFolder))) return null;

            int screenWidth = Screen.PrimaryScreen.Bounds.Width;
            int screenHeight = Screen.PrimaryScreen.Bounds.Height;

            // Getting the list of possible background images
            string[] resourceFiles = Directory.GetFiles(GetRootedPath(Program.Settings.BackgroundsFolder));

            int bestWidth = int.MaxValue;
            int bestHeight = int.MaxValue;
            string bestImageName = null;
            int maxWidth = 0;
            int maxHeight = 0;
            string biggestImage = null;

            // looping through the files to find the best match
            foreach (string file in resourceFiles)
            {
                // this ensure that any file that's not a bitmap will just be skipped
                try
                {
                    // Loading the bitmap
                    using (Bitmap image = new Bitmap(GetRootedPath(file)))
                    {
                        // Checking if the bitmap is the closest match
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

                        // Also keeping track of the biggest bitmap, in case the screen resolution is bigger than any of the images
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

            // If no best match is found, we pick the biggest image
            if (string.IsNullOrEmpty(bestImageName)) bestImageName = biggestImage;
            Bitmap bestImage = new Bitmap(bestImageName);

            // Resizing the image to match the screen
            //TODO: configurable resize method (tiled, zoomed, etc)
            Bitmap outputBitmap = new Bitmap(bestImage, screenWidth, screenHeight);
            return outputBitmap;
        }

        /// <summary>
        /// Gets the rooted path of a file.
        /// </summary>
        /// <remarks>
        /// Used when the path given is relative to the program's executable file
        /// </remarks>
        /// <param name="path">The path.</param>
        /// <returns></returns>
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
