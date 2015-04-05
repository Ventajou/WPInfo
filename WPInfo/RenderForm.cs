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
using System.Management;        // Required for WMI support
using Microsoft.Win32;          // Required for Registry support

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

        private System.Drawing.Size Resolution;
        private bool ResolutionForced = false;
        private ImageModes iMode = Program.Settings.ImageMode;

        public RenderForm()
        {
            InitializeComponent();
            Resolution = Screen.PrimaryScreen.Bounds.Size;
            ResolutionForced = false;
        }

        public RenderForm(System.Drawing.Size ForceRes)
        {
            InitializeComponent();
            Resolution = ForceRes;
            ResolutionForced = true;
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
            this.Size = Resolution;

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
                int screenWidth = Resolution.Width;
                int screenHeight = Resolution.Height;

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

            int height = Program.Settings.IgnoreTaskBar ? Resolution.Height : Screen.PrimaryScreen.WorkingArea.Bottom - Screen.PrimaryScreen.Bounds.Top;

            // Forcing a resolution overrides the IgnoreTaskBar setting ... after all you've forced the resolution,
            // why second-guess the command line?
            if (ResolutionForced)
                height = Resolution.Height;

            // adjusting the RichTextBox location based on the user defined screen position
            switch (Program.Settings.ScreenPosition)
            {
                case ScreenPositions.TopLeft:
                    _infoTextBox.Location = new Point(Program.Settings.HorizontalMargin, Program.Settings.VerticalMargin);
                    break;
                case ScreenPositions.TopRight:
                    _infoTextBox.Location = new Point(Resolution.Width - (_infoTextBox.Width + Program.Settings.HorizontalMargin), Program.Settings.VerticalMargin);
                    break;
                case ScreenPositions.BottomLeft:
                    _infoTextBox.Location = new Point(Program.Settings.HorizontalMargin, height - (_infoTextBox.Height + Program.Settings.VerticalMargin));
                    break;
                case ScreenPositions.BottomRight:
                    _infoTextBox.Location = new Point(Resolution.Width - (_infoTextBox.Width + Program.Settings.HorizontalMargin), height - (_infoTextBox.Height + Program.Settings.VerticalMargin));
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
                // This originally assumed the values are pre-populated. For new dynamic data, they cannot be so we need to check those first and 
                // actually get the values before we allow tokenValues to be formatted
                string[] tokenValues;

                if ((match.Groups[1].Value.IndexOf("[") > 0) && (match.Groups[1].Value.IndexOf("]") > match.Groups[1].Value.IndexOf("[")))
                {
                    // We have a dynamic object - WMI data, WSH Script or Registry Value, identified by the [...] within the match
                    string DynamicID = match.Groups[1].Value.Substring(match.Groups[1].Value.IndexOf("[")+1, match.Groups[1].Value.IndexOf("]")-match.Groups[1].Value.IndexOf("[")-1);
                    switch (match.Groups[1].Value.Substring(0,match.Groups[1].Value.IndexOf("[")))
                    {
                        case TokenIDs.WMIData:
                            tokenValues = ExecWMIQuery(DynamicID);
                            break;
                        case TokenIDs.WSHScript:
                            tokenValues = ExecScript(DynamicID);
                            break;
                        case TokenIDs.Registry:
                            tokenValues = ReadRegistry(DynamicID);
                            break;
                        default:    // Unknown type 
                            tokenValues = new string[] { "Unknown dynamic object type" };
                            break;
                    }
                }
                else
                    tokenValues = tokens[match.Groups[1].Value];

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
                        // TODO: It's not clear why this is needed. Simplifying as I have done allows right-aligned, multi-line text to
                        // work properly instead of staggering left strangely
                        // if (sb.Length > 0) sb.Append(@"\par\pard\li" + indent + " ");
                        if (sb.Length > 0) sb.Append(@"\par ");
                        sb.Append(value);
                    }

                    // Get the information text length before replacing the token
                    int oldLength = _infoTextBox.Rtf.Length;

                    // Replace the token with its values
                    _infoTextBox.Rtf = tokenRegex.Replace(_infoTextBox.Rtf, sb.ToString(), 1, match.Index);

                    // Finding the next line break in order to reset the indentation, we have to look right after the newly added values
                    Match nextLineMatch = nextLineRegex.Match(_infoTextBox.Rtf, match.Index + match.Value.Length + (_infoTextBox.Rtf.Length - oldLength));
          // TODO: It's not clear why this is necessary. It doesn't appear to improve formatting for either left or right aligned text
          // and it breaks multi-line right-aligned text!
          //          if (nextLineMatch.Success)
          //          {
          //              // If the line break is found, add a zero indentation right after it
          //              _infoTextBox.Rtf = nextLineRegex.Replace(_infoTextBox.Rtf, nextLineMatch.Groups[1].Value + @"\li0 ", 1, nextLineMatch.Index);
          //          }
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
        /// Also places the image onto the background layer in the appropriate style (e.g. Centered, Fill, Tiled etc)
        /// </summary>
        /// <returns></returns>
        private Bitmap GetBackground()
        {
            // Cut it short if no folder is configured
            if (!Program.Settings.UseBackgroundsFolder || !Directory.Exists(GetRootedPath(Program.Settings.BackgroundsFolder))) return null;

            int screenWidth = Resolution.Width;
            int screenHeight = Resolution.Height;
            double screenAR = ((double)screenWidth) / ((double)screenHeight);
            int bestWidth = int.MaxValue, bestARWidth = int.MaxValue;
            int bestHeight = int.MaxValue, bestARHeight = int.MaxValue;
            string bestImageName = null, biggestImage = null, bestARImageName = null;
            int maxWidth = 0;
            int maxHeight = 0;
            double bestAR = 0;


            // Getting the list of possible background images
            string[] resourceFiles = Directory.GetFiles(GetRootedPath(Program.Settings.BackgroundsFolder));

            // looping through the files to find the best match
            foreach (string file in resourceFiles)
            {
                // this ensure that any file that's not a bitmap will just be skipped
                try
                {
                    // Loading the bitmap
                    using (Bitmap I = new Bitmap(GetRootedPath(file)))
                    {
                        double imageAR = ((double)(I.Width)) / ((double)I.Height);

                        // First check for a perfect match
                        if ((I.Width == screenWidth) && (I.Height == screenHeight))
                        {
                            // We're done. This one is a perfect match, so save its info and quit the loop
                            bestWidth = I.Width; bestHeight = I.Height; bestAR = imageAR;
                            bestImageName = file;
                            break;
                        }

                        // OK it's not perfect. Maybe it's the right Aspect, and the smallest one bigger than the screen?
                        else if ((Math.Abs(imageAR - screenAR) < 0.02)         // Comparing doubles directly is rife with rounding issues
                            && (I.Width >= screenWidth) && (I.Height >= screenHeight)
                            && ((I.Width <= bestARWidth) || (I.Height <= bestARHeight)))
                        {
                            bestARWidth = I.Width; bestARHeight = I.Height; bestAR = imageAR;
                            bestARImageName = file;
                        }

                        // Or maybe it's the right Aspect, and the biggest one SMALLER than the screen?
                        else if ((Math.Abs(imageAR - screenAR) < 0.02)         // Comparing doubles directly is rife with rounding issues
                            && (I.Width < screenWidth) && (I.Height < screenHeight)
                            && ((I.Width > bestARWidth) || (I.Height > bestARHeight)))
                        {
                            bestARWidth = I.Width; bestARHeight = I.Height; bestAR = imageAR;
                            bestARImageName = file;
                        }

                        // OK it's not even the SAME AR. If we don't already have a best AR that matches the screen, maybe it's close,
                        // and still the smallest larger than screen?
                        else if (Math.Abs(bestAR - screenAR) < 0.02           // Comparing doubles directly is rife with rounding issues. This allows (e.g.) 1366x768 to match 1600x900
                            && (Math.Abs(imageAR - screenAR) <= Math.Abs(imageAR - bestAR))
                            && (I.Width >= screenWidth) && (I.Height >= screenHeight)
                            && ((I.Width <= bestARWidth) || (I.Height <= bestARHeight)))
                        {
                            bestARWidth = I.Width; bestARHeight = I.Height; bestAR = imageAR;
                            bestARImageName = file;
                        }

                        // We're striking out. It's not the same AR and it isn't even the size of the screen!
                        // If we don't already have a best AR that matches the screen, maybe it's close,
                        // and still the biggest smaller than screen?
                        else if (Math.Abs(bestAR - screenAR) < 0.02           // Comparing doubles directly is rife with rounding issues. This allows (e.g.) 1366x768 to match 1600x900
                            && (Math.Abs(imageAR - screenAR) <= Math.Abs(imageAR - bestAR))
                            && (I.Width < screenWidth) && (I.Height < screenHeight)
                            && ((I.Width > bestARWidth) || (I.Height > bestARHeight)))
                        {
                            bestARWidth = I.Width; bestARHeight = I.Height; bestAR = imageAR;
                            bestARImageName = file;
                        }

                        // Also keep track of the biggest bitmap, in case the screen resolution is bigger than any of the images AND there's no image
                        // with the same aspect ratio as the screen. Note that "biggestARImage" if it exists will be bigger than or the same as bestARImage
                        // depending on whether there are others with the correct AR but a closer resolution match
                        if (I.Width >= maxWidth && I.Height >= maxHeight)
                        {
                            maxWidth = I.Width; maxHeight = I.Height;
                            biggestImage = file;
                        }
                    }
                }
                catch (Exception) { }
            }

            // If no perfect match was found, pick the best AR image
            if (string.IsNullOrEmpty(bestImageName)) bestImageName = bestARImageName;
            // If no best match is found yet, we pick the biggest image
            if (string.IsNullOrEmpty(bestImageName)) bestImageName = biggestImage;
            Bitmap bestImage = null;
            if (!string.IsNullOrEmpty(bestImageName)) bestImage = new Bitmap(bestImageName);

            // Resizing the image to match the screen
            //TODO: configurable resize method (tiled, zoomed, etc)

            Bitmap B = new Bitmap(screenWidth, screenHeight);
            Graphics G = Graphics.FromImage(B);
            float oX = 0, oY = 0;
            double sFactor = 0;
            int iWidth = 0, iHeight = 0;
            if (bestImage != null)
                switch (iMode)
                {
                    case ImageModes.Centered:
                        oX = (screenWidth - bestImage.Width) / 2;      // X offset, may be negative (outside G)
                        oY = (screenHeight - bestImage.Height) / 2;   // Y offset, may be negative (outside G)
                        G.DrawImage(bestImage, oX, oY, bestImage.Width, bestImage.Height);
                        break;
                    case ImageModes.Fit:
                        sFactor = Math.Min((double)screenWidth / (double)bestImage.Width, (double)screenHeight / (double)bestImage.Height);
                        iHeight = (int)Math.Floor(bestImage.Height * sFactor);
                        iWidth = (int)Math.Floor(bestImage.Width * sFactor);
                        oX = (screenWidth - iWidth) / 2;      // X offset, may be negative (outside G)
                        oY = (screenHeight - iHeight) / 2;   // Y offset, may be negative (outside G)
                        G.DrawImage(bestImage, oX, oY, iWidth, iHeight);
                        break;
                    case ImageModes.Fill:
                        sFactor = Math.Max((double)screenWidth / (double)bestImage.Width, (double)screenHeight / (double)bestImage.Height);
                        iHeight = (int)Math.Floor(bestImage.Height * sFactor);
                        iWidth = (int)Math.Floor(bestImage.Width * sFactor);
                        oX = (screenWidth - iWidth) / 2;      // X offset, may be negative (outside G)
                        oY = (screenHeight - iHeight) / 2;   // Y offset, may be negative (outside G)
                        G.DrawImage(bestImage, oX, oY, iWidth, iHeight);
                        break;
                    case ImageModes.Stretched:
                        G.DrawImage(bestImage, 0, 0, B.Width, B.Height);
                        break;
                    case ImageModes.Tiled:
                        while (oX < B.Width)
                        {
                            oY = 0;
                            while (oY < B.Height)
                            {
                                G.DrawImage(bestImage, oX, oY, bestImage.Width, bestImage.Height);
                                oY += bestImage.Height;
                            }
                            oX += bestImage.Width;
                        }
                        break;
                }
            return B;
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

        /// <summary>
        /// Executes and returns the result of the WMI query nominated by ID. The list of known IDs is
        /// stored in Program.Settings so it can be saved and restored
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        private string[] ExecWMIQuery (string ID)
        {
            // Find the query in the list of known queries
            WMIQuery W = Program.Settings.WMIQueries.Find(WMIQuery => WMIQuery.Name == ID);
            try
            {
                using (var WMI = new ManagementObjectSearcher(W.Namespace, W.Query))
                {
                    List<string> Values = new List<string>();
                    foreach (ManagementObject Result in WMI.Get())
                        foreach (var pDC in Result.Properties)
                            Values.Add(pDC.Value.ToString());
                    return Values.ToArray();
                }
            }
            catch (Exception e)
            {
                return new string[] { "WMI Query " + ID + " failed - " + e.Message };
            }
        }

        /// <summary>
        /// Executes and returns the output of the WSH Script nominated by ID. The list of known IDs is
        /// stored in Program.Settings so it can be saved and restored
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        private string[] ExecScript(string ID)
        {
            return new string[] { "Not yet implemented" };
        }

        /// <summary>
        /// Returns the value or values defined by the Registry Value query nominated by ID. The list of known IDs is
        /// stored in Program.Settings so it can be saved and restored
        /// </summary>
        /// <param name="ID">The named identifier for the Registry value to be queried</param>
        /// <returns>Array of strings (1+)</returns>
        private string[] ReadRegistry (string ID)
        {
            // Find the query information
            Ventajou.WPInfo.RegValue R = Program.Settings.RegValues.Find(RegValue => RegValue.Name == ID);
            try
            {
                RegistryKey RK;
                switch (R.Key)
                {
                    case RegValue.HKLM: RK = Registry.LocalMachine; break;
                    case RegValue.HKCU: RK = Registry.CurrentUser; break;
                    case RegValue.HKCC: RK = Registry.CurrentConfig; break;
                    case RegValue.HKCR: RK = Registry.ClassesRoot; break;
                    case RegValue.HKU: RK = Registry.Users; break;
                    default: RK = Registry.LocalMachine; break;
                }
                RegistryKey SK = RK.OpenSubKey(R.Path, false);
                if (SK == null) throw new Exception("Could not find Path in Hive");
                RegistryValueKind RVK = SK.GetValueKind(R.Value);
                // Format the value based on its type
                switch (RVK)
                {
                    case RegistryValueKind.MultiString:
                        return new string[] { SK.GetValue(R.Value).ToString() };
                    case RegistryValueKind.Binary:
                        byte[] bytes = (byte[])SK.GetValue(R.Value);
                        StringBuilder sb = new StringBuilder();
                        for (int i = 0; i < bytes.Length; i++)
                            sb.AppendFormat(" {0:X2}", bytes[i]);
                        return new string[] { sb.ToString() };
                    default:
                        return new string[] { String.Format("{0}", SK.GetValue(R.Value)) };
                }
            }
            catch (Exception e)
            {
                return new string[] { "Registry value " + R.Name + " error: " + e.Message };
            }
        }

        #endregion
    }
}
