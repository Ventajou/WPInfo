using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace Ventajou.WPInfo
{
    public class ImageOverlay : ICloneable
    {
        public string FullPath { get; set; }
        public string FileName { get; set; }
        public HorizontalAlignment HorizontalAlignment { get; set; }
        public VerticalAlignment VerticalAlignment { get; set; }
        public int Margin { get; set; }

        #region ICloneable Members

        public object Clone()
        {
            ImageOverlay clone = new ImageOverlay();
            clone.FullPath = FullPath.Clone() as string;
            clone.FileName = FileName.Clone() as string;
            clone.HorizontalAlignment = HorizontalAlignment;
            clone.VerticalAlignment = VerticalAlignment;
            clone.Margin = Margin;

            return clone;
        }

        #endregion
    }
}
