using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ventajou.WPInfo
{
    public class Registry : ICloneable
    {
        public string Name { get; set; }
        public string Hive { get; set; }
        public string Path { get; set; }

        #region ICloneable Members

        /// <summary>
        /// Implement the ICloneable interface
        /// </summary>
        /// <returns></returns>
        public object Clone()
        {
            Registry clone = new Registry();
            clone.Name = Name.Clone() as string;
            clone.Hive = Hive.Clone() as string;
            clone.Path = Path.Clone() as string;

            return clone;
        }

        #endregion

    }
}
