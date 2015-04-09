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
using System.Management;         // Required for WMI support
using Microsoft.Win32;
using System.Diagnostics;
using System.Threading;          // Required for Registry support

namespace Ventajou.WPInfo
{
    /// <summary>
    /// This form is used to render the wallpaper both as a preview and prior to saving it.
    /// </summary>
    public partial class RenderForm : Form
    {
        // This control is used to render the text
        TransparentRichTextBox _irtb;

        // Path to the executable, used to resolve relative paths
        private string _appPath;

        public System.Drawing.Size Resolution;
        public Bitmap Output;
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
            // All the rendering is done in RenderLayers() now so that target size is not constrained
            // by screen resolution
            RenderLayers();

            // take the whole screen size - since we render to a bitmap of whatever size is needed,
            // we need to scale into the form (pretty easy)
            this.Size = Resolution;

            // size the background pic accordingly
            backgroundPictureBox.Bounds = Bounds;
            backgroundPictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
            backgroundPictureBox.Image = Output;
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
            _irtb.Size = new Size(500, e.NewRectangle.Size.Height);

            int height = Program.Settings.IgnoreTaskBar ? Resolution.Height : Screen.PrimaryScreen.WorkingArea.Bottom - Screen.PrimaryScreen.Bounds.Top;

            // Forcing a resolution overrides the IgnoreTaskBar setting ... after all you've forced the resolution,
            // why second-guess the command line?
            if (ResolutionForced)
                height = Resolution.Height;

            // adjusting the RichTextBox location based on the user defined screen position
            switch (Program.Settings.ScreenPosition)
            {
                case ScreenPositions.TopLeft:
                    _irtb.Location = new Point(Program.Settings.HorizontalMargin, Program.Settings.VerticalMargin);
                    break;
                case ScreenPositions.TopRight:
                    _irtb.Location = new Point(Resolution.Width - (_irtb.Width + Program.Settings.HorizontalMargin), Program.Settings.VerticalMargin);
                    break;
                case ScreenPositions.BottomLeft:
                    _irtb.Location = new Point(Program.Settings.HorizontalMargin, height - (_irtb.Height + Program.Settings.VerticalMargin));
                    break;
                case ScreenPositions.BottomRight:
                    _irtb.Location = new Point(Resolution.Width - (_irtb.Width + Program.Settings.HorizontalMargin), height - (_irtb.Height + Program.Settings.VerticalMargin));
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
            _irtb.Rtf = Program.Settings.InfoText;

            // retrieving the tokens and their values
            Dictionary<string, string[]> tokens = Program.GetTokens();

            // This regular expression will find tokens in the information text
            Regex tokenRegex = new Regex("<% (?<token>.*?) %>");

            // This will find the next line break after a multi value token.
            Regex nextLineRegex = new Regex(@"(?<break>\\line|\\par)[^d]");

            // Look for the first token
            Match match = tokenRegex.Match(_irtb.Rtf);

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
                    Point position = _irtb.GetPositionFromCharIndex(tokenRegex.Match(_irtb.Text).Index);
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
                    int oldLength = _irtb.Rtf.Length;

                    // Replace the token with its values
                    _irtb.Rtf = tokenRegex.Replace(_irtb.Rtf, sb.ToString(), 1, match.Index);

                    // Finding the next line break in order to reset the indentation, we have to look right after the newly added values
                    Match nextLineMatch = nextLineRegex.Match(_irtb.Rtf, match.Index + match.Value.Length + (_irtb.Rtf.Length - oldLength));
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
                    _irtb.Rtf = tokenRegex.Replace(_irtb.Rtf, tokenValues[0], 1, match.Index);
                }

                // Find the next token
                match = tokenRegex.Match(_irtb.Rtf, match.Index);
            }
        }

        /// <summary>
        /// Gets the background image most appropriate for the current resolution of the primary display.
        /// Also places the image onto the background layer in the appropriate style (e.g. Centered, Fill, Tiled etc)
        /// 
        /// This is really long and probably could use some breaking up.
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

            // Resizing the image to match the settings and screen, including adding the background colour just in case 
            Bitmap B = new Bitmap(screenWidth, screenHeight, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            Graphics G = Graphics.FromImage(B);
            G.FillRectangle(new SolidBrush(Program.Settings.BackgroundColor.ToColor()), 0, 0, screenWidth, screenHeight);
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
            WSHScript S = Program.Settings.WSHScripts.Find(WSHScript => WSHScript.Name == ID);
            DateTime dtDeadLine = DateTime.Now.AddSeconds(S.Timeout);
            ProcessStartInfo psi = new ProcessStartInfo();
            psi.RedirectStandardOutput = true;
            psi.RedirectStandardError = true;
            psi.FileName = "wscript.exe";
            psi.WindowStyle = ProcessWindowStyle.Hidden;
            psi.Arguments = S.Name + " " + S.Parameters;
            psi.UseShellExecute = false;
            Process p = Process.Start(psi);
            while ((DateTime.Now <= dtDeadLine) && (!p.HasExited)) Thread.Sleep(1000);
            if (!p.HasExited)
            {
                p.Kill();
                return new string[] { "Runtime exceeded for script " + ID };
            }
            using (StreamReader sr = p.StandardOutput)
            {
                List<string> l = new List<string>();
                while (!sr.EndOfStream) l.Add(sr.ReadLine());
                return l.ToArray();
            }
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

        /// <summary>
        /// Renders all the layers of the image, from the base colour, through the main image, overlays and
        /// finally the rich text.
        /// </summary>
        public void RenderLayers()
        {
            Output = new Bitmap(Resolution.Width, Resolution.Height);

            // Getting a graphics object to draw the overlays on, and fill with default background colour
            Graphics backgroundGraphics = Graphics.FromImage(Output);
            backgroundGraphics.FillRectangle(new SolidBrush(Program.Settings.BackgroundColor.ToColor()), 0, 0, Output.Width, Output.Height);

            // Get the background image and draw it
            Bitmap bgImage = GetBackground();
            if (bgImage != null) backgroundGraphics.DrawImage(bgImage, 0, 0, bgImage.Width, bgImage.Height);

            // Rendering the overlays
            if (Program.Settings.Overlays.Count > 0)
            {
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
                                    left = (Resolution.Width - overlayBitmap.Width) / 2;
                                    break;
                                case HorizontalAlignment.Left:
                                    left = overlay.Margin;
                                    break;
                                case HorizontalAlignment.Right:
                                    left = Resolution.Width - (overlay.Margin + overlayBitmap.Width);
                                    break;
                            }

                            // Calculating the vertical position of the overlay
                            switch (overlay.VerticalAlignment)
                            {
                                case VerticalAlignment.Bottom:
                                    top = Resolution.Height - (overlay.Margin + overlayBitmap.Height);
                                    break;
                                case VerticalAlignment.Center:
                                    top = (Resolution.Height - overlayBitmap.Height) / 2;
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

            // Creating the transparent text box and adding it on top
            _irtb = new TransparentRichTextBox();
            _irtb.BorderStyle = System.Windows.Forms.BorderStyle.None;
            _irtb.Name = "InfoRichTextBox";
            _irtb.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            _irtb.ContentsResized += TextBoxContentsResized;
            _irtb.DetectUrls = false;
            if (Program.Settings.ShowTextBox)
                _irtb.BackColor = Color.FromArgb(Program.Settings.TextBoxOpacity, Program.Settings.BackgroundColor.R, Program.Settings.BackgroundColor.G, Program.Settings.BackgroundColor.B);

            // fill the text box with the information text
            SubstituteTokens();

            // Draw it on the background and we're done
            // Note this is where the current bugs lie - e.g. horrible AA, last line cut off.
            backgroundGraphics.DrawRtfText(_irtb.Rtf, new Rectangle(_irtb.Location, _irtb.Size));
        }

        #endregion
    }
}
