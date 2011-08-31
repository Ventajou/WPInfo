using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Ventajou.WPInfo
{
    public partial class TextMarginsForm : Form
    {
        public TextMarginsForm()
        {
            InitializeComponent();
        }

        protected override void OnShown(EventArgs e)
        {
            horizontalNumericUpDown.Value = Program.Settings.HorizontalMargin;
            verticalNumericUpDown.Value = Program.Settings.VerticalMargin;
            base.OnShown(e);
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            Program.Settings.HorizontalMargin = decimal.ToInt32(horizontalNumericUpDown.Value);
            Program.Settings.VerticalMargin = decimal.ToInt32(verticalNumericUpDown.Value);
            Close();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
