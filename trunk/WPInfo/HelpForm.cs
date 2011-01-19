using System;
using System.Windows.Forms;

namespace Ventajou.WPInfo
{
    /// <summary>
    /// A simple help dialog.
    /// </summary>
    public partial class HelpForm : Form
    {
        public HelpForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Called when the close button is clicked.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void closeButtonClicked(object sender, EventArgs e)
        {
            Close();
        }
    }
}
