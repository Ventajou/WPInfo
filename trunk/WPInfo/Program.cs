using System;
using System.Collections.Generic;
using System.Drawing;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Microsoft.Win32;
using System.Drawing.Imaging;
using System.Management;
using Microsoft.Test.CommandLineParsing;
using System.Xml.Serialization;

namespace Ventajou.WPInfo
{
    static class Program
    {
        [DllImport("USER32.dll")]
        private static extern bool PrintWindow(IntPtr hwnd, IntPtr hdcBlt, uint nFlags);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        static extern int SystemParametersInfo(int uAction, int uParam, string lpvParam, int fuWinIni);

        const int SPI_SETDESKWALLPAPER = 20;
        const int SPIF_UPDATEINIFILE = 0x01;
        const int SPIF_SENDWININICHANGE = 0x02;

        public static ProgramSettings Settings { get; set; }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Settings = new ProgramSettings();

            Parameters parameters = new Parameters();
            try
            {
                CommandLineParser.ParseArguments(parameters, args);
            }
            catch (Exception) { }

            if (parameters.Help)
            {
                using (HelpForm helpForm = new HelpForm())
                {
                    helpForm.ShowDialog();
                    return;
                }
            }

            bool fileLoaded = false;
            if (!string.IsNullOrEmpty(parameters.LoadFile))
            {
                try
                {
                    LoadFile(parameters.LoadFile);
                    fileLoaded = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Invalid settings file.\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            if (fileLoaded && parameters.Silent)
            {
                //SetWallpaper();
            }
            else
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new MainForm());
            }
        }

        public static Dictionary<string, string[]> GetTokens()
        {
            //TODO: Add more tokens in there!
            Dictionary<string, string[]> returnValue = new Dictionary<string, string[]>();

            // User name
            returnValue.Add("User Name", new string[] { Environment.UserName });

            // Host name
            returnValue.Add("Host Name", new string[] { Environment.MachineName });

            // IPv4 Addresses
            IPHostEntry hostEntry = Dns.GetHostEntry(Dns.GetHostName());
            List<string> addresses = new List<string>();
            foreach (IPAddress address in hostEntry.AddressList)
            {
                if (address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork) addresses.Add(address.ToString());
            }
            returnValue.Add("IPv4 Addresses", addresses.ToArray());

            // IPv6 Addresses
            addresses.Clear();
            foreach (IPAddress address in hostEntry.AddressList)
            {
                if (address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetworkV6) addresses.Add(address.ToString());
            }
            returnValue.Add("IPv6 Addresses", addresses.ToArray());

            // User Logon Domain
            returnValue.Add("Logon Domain", new string[] { Environment.UserDomainName });

            // OS Name
            ManagementObjectSearcher objectSearcher = new ManagementObjectSearcher("SELECT * FROM Win32_OperatingSystem");
            ManagementObjectCollection objectCollection = objectSearcher.Get();

            foreach (ManagementObject managementObject in objectCollection)
            {
                returnValue.Add("Operating System", new string[] { managementObject.GetPropertyValue("Caption").ToString() });
            }

            // OS Service Pack
            returnValue.Add("OS Service Pack", new string[] { Environment.OSVersion.ServicePack });

            // OS Version
            returnValue.Add("OS Version", new string[] { Environment.OSVersion.Version.ToString() });

            // Free Space on drives
            DriveInfo[] drives = DriveInfo.GetDrives();
            List<string> freeSpace = new List<string>();
            foreach (DriveInfo drive in drives)
            {
                if (drive.DriveType == DriveType.Fixed)
                {
                    freeSpace.Add(string.Format(@"{0}\ {1} GB free", drive.Name, (drive.AvailableFreeSpace / (1024 * 1024)).ToString("##,#")));
                }
            }
            returnValue.Add("Free Space", freeSpace.ToArray());

            return returnValue;
        }

        public static string WrapTokenKey(string key)
        {
            return "<% " + key + " %>";
        }

        public static Bitmap CaptureWindow(Form form)
        {
            Bitmap bitmap = new Bitmap(form.Width, form.Height);
            Graphics graphics = Graphics.FromImage(bitmap);

            bool result = PrintWindow(form.Handle, graphics.GetHdc(), 0);
            graphics.ReleaseHdc();
            graphics.Flush();

            return bitmap;
        }

        public static void SetWallpaper(RenderForm renderForm)
        {
            string destinationPath = string.Empty;

            switch (Settings.OutputDestination)
            {
                case OutputDestinations.WindowsFolder:
                    destinationPath = "%windir%";
                    break;
                case OutputDestinations.AppDataFolder:
                    destinationPath = @"%userprofile%\Local Settings\Application Data\";
                    break;
                case OutputDestinations.TempFolder:
                    destinationPath = "%temp%";
                    break;
                case OutputDestinations.OtherFolder:
                    destinationPath = Settings.OtherFolder;
                    break;
            }

            destinationPath = Environment.ExpandEnvironmentVariables(Path.Combine(destinationPath, Settings.OutputFileName));
            //RenderForm renderForm = new RenderForm();
            //using (RenderForm renderForm = new RenderForm())
            {
                // Somehow if I render the form offscreen, the background picture will not be rendered.
               // renderForm.Show();
                //renderForm.Location = new Point(0, 0);
               // renderForm.BringToFront();
               // renderForm.Refresh();

                

                Bitmap b = CaptureWindow(renderForm);
                b.Save(destinationPath, ImageFormat.Bmp);

                renderForm.Close();
            }

#if !DEBUG
            // I don't want to actually change the wallpaper on my dev box!
            RegistryKey key = Registry.CurrentUser.OpenSubKey("Control Panel\\Desktop", true);
            key.SetValue(@"WallpaperStyle", "1");
            key.SetValue(@"TileWallpaper", "0");

            SystemParametersInfo(SPI_SETDESKWALLPAPER, 0, destinationPath, SPIF_UPDATEINIFILE | SPIF_SENDWININICHANGE);
#endif
        }

        public static void LoadFile(string fileName)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(ProgramSettings));
            Stream file = new FileStream(fileName, FileMode.Open, FileAccess.Read);
            Program.Settings = (ProgramSettings)serializer.Deserialize(file);
            file.Close();
        }
    }
}
