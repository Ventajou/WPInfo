using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ventajou.WPInfo
{
    public class WSHScript : ICloneable
    {
        public string Name { get; set; }
        public string ScriptPath { get; set; }
        public string Parameters { get; set; }

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

            return clone;
        }

        #endregion
    }
}
