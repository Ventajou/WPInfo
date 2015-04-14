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
using System.Text.RegularExpressions;

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
        private static Size ForcedResolution;

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

            if (!string.IsNullOrEmpty(parameters.Resolution))
            {
                Regex Validator = new Regex("(^\\d*)x(\\d*)$");       // Digits x digits - Width x Height
                if (Validator.IsMatch(parameters.Resolution))
                {
                    Match m = Validator.Match(parameters.Resolution);
                    int W = Int32.Parse(m.Groups[1].Captures[0].ToString());
                    int H = Int32.Parse(m.Groups[2].Captures[0].ToString());
                    ForcedResolution = new System.Drawing.Size(W, H);
                }
            }

            if (fileLoaded && parameters.Silent)
            {
                SetWallpaper(null);
            }
            else
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                MainForm mf = new MainForm();
                if (fileLoaded) mf.FileName = parameters.LoadFile;
                if (ForcedResolution != Size.Empty) mf.Resolution = ForcedResolution;
                Application.Run(mf);
            }
        }

        public static Dictionary<string, string[]> GetTokens()
        {
            //TODO: Add more tokens in there!
            Dictionary<string, string[]> returnValue = new Dictionary<string, string[]>();

            // User name
            returnValue.Add(Tokens.UserName, new string[] { Environment.UserName });

            // Host name
            returnValue.Add(Tokens.HostName, new string[] { Environment.MachineName });

            // IPv4 Addresses
            IPHostEntry hostEntry = Dns.GetHostEntry(Dns.GetHostName());
            List<string> addresses = new List<string>();
            foreach (IPAddress address in hostEntry.AddressList)
            {
                if (address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork) addresses.Add(address.ToString());
            }
            returnValue.Add(Tokens.IPv4, addresses.ToArray());

            // IPv6 Addresses
            addresses.Clear();
            foreach (IPAddress address in hostEntry.AddressList)
            {
                if (address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetworkV6) addresses.Add(address.ToString());
            }
            returnValue.Add(Tokens.IPv6, addresses.ToArray());

            // User Logon Domain
            returnValue.Add(Tokens.LogonDomain, new string[] { Environment.UserDomainName });

            // OS Name
            ManagementObjectSearcher objectSearcher = new ManagementObjectSearcher("SELECT * FROM Win32_OperatingSystem");
            ManagementObjectCollection objectCollection = objectSearcher.Get();

            foreach (ManagementObject managementObject in objectCollection)
            {
                returnValue.Add(Tokens.OS, new string[] { managementObject.GetPropertyValue("Caption").ToString() });
            }

            // OS Service Pack
            returnValue.Add(Tokens.OS_SP, new string[] { Environment.OSVersion.ServicePack });

            // OS Version
            returnValue.Add(Tokens.OS_Ver, new string[] { Environment.OSVersion.Version.ToString() });

            // Free Space on drives
            DriveInfo[] drives = DriveInfo.GetDrives();
            List<string> freeSpace = new List<string>();
            foreach (DriveInfo drive in drives)
            {
                try
                {
                    if (drive.DriveType == DriveType.Fixed)
                    {
                        freeSpace.Add(string.Format(@"{0}\ {1} GB free", drive.Name, (drive.AvailableFreeSpace / (1024 * 1024)).ToString("##,#")));
                    }
                }
                catch (Exception) { }
            }
            returnValue.Add(Tokens.FreeSpace, freeSpace.ToArray());

            // TODO: Arbitrary WMI Query?! Need to prompt at time of add, store query, run query as needed
            // Design thoughts: Collection of objects in Program Settings (which means they're saved in the .WPI) - done!
            // Props Name, Namespace, Query? - done! Allow select by name and insert into info
            returnValue.Add(Tokens.WMIData, new string[] { "WMI Query: Placeholder" });

            //TODO: Arbitrary WScript?! Need to prompt at time of add, store script name, run script as needed
            // Design thoughts: Collection of objects in Program Settings (which means they're saved in the .WPI)
            // Props Name, ScriptPath, Params? Allow select by name and insert into info
            returnValue.Add(Tokens.WSHScript, new string[] { "Windows Script: Placeholder" });

            //TODO: Arbitrary Registry?! Need to prompt at time of add, store hive and path.
            // Design thoughts: Collection of objects in Program Settings (which means they're saved in the .WPI)
            // Props Name, Path? Allow select by name and insert into info
            returnValue.Add(Tokens.RegistryValue, new string[] { "Registry Data: Placeholder" });

            return returnValue;
        }

        public static string WrapTokenKey(string key)
        {
            return Tokens.TokenLeft + key + Tokens.TokenRight;
        }

        //public static Bitmap CaptureWindow(Form form)
        //{
        //    Bitmap bitmap = new Bitmap(form.Width, form.Height);
        //    Graphics graphics = Graphics.FromImage(bitmap);

        //    bool result = PrintWindow(form.Handle, graphics.GetHdc(), 0);
        //    graphics.ReleaseHdc();
        //    graphics.Flush();

        //    return bitmap;
        //}

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

            if (renderForm == null)
                if (ForcedResolution != Size.Empty)
                    renderForm = new RenderForm(ForcedResolution);
                else
                    renderForm = new RenderForm();

            renderForm.RenderLayers();

            Bitmap b = renderForm.Output;
            // TODO: Selectable image format (at least BMP/JPG/PNG)
            b.Save(destinationPath, ImageFormat.Bmp);

            renderForm.Close();

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
