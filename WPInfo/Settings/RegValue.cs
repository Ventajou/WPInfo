using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Win32;

namespace Ventajou.WPInfo
{
    public class RegValue : ICloneable
    {
        public const string HKLM = "HKEY_LOCAL_MACHINE";
        public const string HKCU = "HKEY_CURRENT_USER";
        public const string HKCC = "HKEY_CURRENT_CONFIG";
        public const string HKCR = "HKEY_CLASSES_ROOT";
        public const string HKU  = "HKEY_USERS";

        public string Name { get; set; }
        public string Key { get; set; }
        public string Path { get; set; }
        public string Value { get; set; }

        #region ICloneable Members

        /// <summary>
        /// Implement the ICloneable interface
        /// </summary>
        /// <returns></returns>
        public object Clone()
        {
            RegValue clone = new RegValue();
            clone.Name = Name.Clone() as string;
            clone.Key = Key.Clone() as string;
            clone.Path = Path.Clone() as string;
            clone.Value = Value.Clone() as string;

            return clone;
        }

        #endregion

    }
}
