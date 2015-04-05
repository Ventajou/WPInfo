using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ventajou.WPInfo
{
    public class WSHScript : ICloneable
    {
        public static class Defaults
        {
            public const string Name = "Script1";
            public const string ScriptPath = "";
            public const string Parameters = "";
            public const int Timeout = 5;
        }
        
        public string Name { get; set; }
        public string ScriptPath { get; set; }
        public string Parameters { get; set; }
        public int Timeout { get; set; }

        #region ICloneable Members

        /// <summary>
        /// Implement the ICloneable interface
        /// </summary>
        /// <returns></returns>
        public object Clone()
        {
            WSHScript clone = new WSHScript();
            clone.Name = Name.Clone() as string;
            clone.ScriptPath = ScriptPath.Clone() as string;
            clone.Parameters = Parameters.Clone() as string;
            clone.Timeout = Timeout;

            return clone;
        }

        #endregion
    }
}
