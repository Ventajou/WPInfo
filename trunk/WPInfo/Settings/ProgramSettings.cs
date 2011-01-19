using System.Drawing;
using System.Collections.Generic;

namespace Ventajou.WPInfo
{
    /// <summary>
    /// Contains all of the information needed to render a wallpaper.
    /// The XmlSerializer is used to save and retrieve this information.
    /// </summary>
    public class ProgramSettings
    {
        public const int Margin = 30;

        /// <summary>
        /// The information text
        /// </summary>
        public string InfoText { get; set; }

        /// <summary>
        /// Position where the information text will be rendered
        /// </summary>
        public ScreenPositions ScreenPosition { get; set; }

        /// <summary>
        /// Background Color when no image is displayed
        /// </summary>
        public SerializableColor BackgroundColor { get; set; }

        /// <summary>
        /// Show a box under the text when a background image is displayed.
        /// </summary>
        public bool ShowTextBox { get; set; }

        /// <summary>
        /// Opacity level of the text box
        /// </summary>
        public byte TextBoxOpacity { get; set; }

        /// <summary>
        /// Color depth of the wallpaper file
        /// </summary>
        public OutputDepths OutputDepth { get; set; }

        /// <summary>
        /// Destination of the wallpaper file
        /// </summary>
        public OutputDestinations OutputDestination { get; set; }

        /// <summary>
        /// User defined destination of the wallpaper file
        /// </summary>
        public string OtherFolder { get; set; }

        /// <summary>
        /// Wallpaper file name
        /// </summary>
        public string OutputFileName { get; set; }

        /// <summary>
        /// Ignore the taskbar when placing the information text at the bottom of the screen
        /// </summary>
        public bool IgnoreTaskBar { get; set; }

        /// <summary>
        /// List of overlays to place on top of the background
        /// </summary>
        public List<ImageOverlay> Overlays { get; set; }

        /// <summary>
        /// Look for appropriate background image in folder
        /// </summary>
        public bool UseBackgroundsFolder { get; set; }

        /// <summary>
        /// Folder containing the background pictures
        /// </summary>
        public string BackgroundsFolder { get; set; }

        /// <summary>
        /// Attempts to save image paths as relative paths
        /// </summary>
        public bool SaveRelativeImagePaths { get; set; }

        public ProgramSettings()
        {
            Overlays = new List<ImageOverlay>();
            ResetValues();
        }

        /// <summary>
        /// Resets the values to their default.
        /// </summary>
        public void ResetValues()
        {
            InfoText = string.Empty;
            ScreenPosition = ScreenPositions.BottomRight;
            BackgroundColor = SerializableColor.FromColor(Color.White);
            OutputDepth = OutputDepths.CurrentDepth;
            OutputDestination = OutputDestinations.TempFolder;
            OtherFolder = string.Empty;
            OutputFileName = "wpinfo.bmp";
            IgnoreTaskBar = false;
            Overlays.Clear();
            SaveRelativeImagePaths = true;
        }
    }
}
