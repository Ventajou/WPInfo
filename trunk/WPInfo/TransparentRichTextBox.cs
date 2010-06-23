using System.Windows.Forms;

namespace Ventajou.WPInfo
{
    /// <summary>
    /// A RichTextBox with a transparent background.
    /// </summary>
    class TransparentRichTextBox : RichTextBox
    {
        override protected CreateParams CreateParams
        {
            get
            {
                CreateParams createParams = base.CreateParams;
                createParams.ExStyle |= 0x20;
                return createParams;
            }
        }

        override protected void OnPaintBackground(PaintEventArgs e)
        {
        }
    }
}
