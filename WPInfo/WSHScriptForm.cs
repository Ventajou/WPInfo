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
    public partial class WSHScriptForm : Form
    {
        /// <summary>
        /// Constructor and loads existing items into list box
        /// </summary>
        public WSHScriptForm()
        {
            InitializeComponent();
            // Add any existing items from the Program.Settings collection
            foreach (WSHScript W in Program.Settings.WSHScripts)
            {
                listQueries.Items.Add(W.Name);
            }
        }

        /// <summary>
        /// Handles clicks on New button, including edit mode enable/disable, defaults
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnNew_Click(object sender, EventArgs e)
        {
            // Set fields and buttons to editing mode, clear any lingering data
            txtName.Enabled = true; txtName.ReadOnly = false;
            txtPath.Enabled = true; txtPath.ReadOnly = false;
            txtParams.Enabled = true; txtParams.ReadOnly = false;
            numTimeout.Enabled = true; numTimeout.ReadOnly = false;
            txtName.Focus();
            txtName.Text = WSHScript.Defaults.Name;
            txtPath.Text = WSHScript.Defaults.ScriptPath;
            txtParams.Text = WSHScript.Defaults.Parameters;
            numTimeout.Value = WSHScript.Defaults.Timeout;
            btnSave.Enabled = true;
        }

        /// <summary>
        /// Handles clicks on Edit button, including edit mode enable/disable. Note that correct data has been
        /// loaded by list selection change handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEdit_Click(object sender, EventArgs e)
        {
            // Set fields and buttons to editing mode
            txtName.Enabled = true; txtName.ReadOnly = false;
            txtPath.Enabled = true; txtPath.ReadOnly = false;
            txtParams.Enabled = true; txtParams.ReadOnly = false;
            numTimeout.Enabled = true; numTimeout.ReadOnly = false;
            btnSave.Enabled = true;
        }

        /// <summary>
        /// Handles clicks on Save button, including copying data back to Program.Settings
        /// and forcing list selection index update to revert to non-edit mode
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            // Find the old one if it exists so that we can replace/update as necessary.
            WSHScript oldW = Program.Settings.WSHScripts.Find(WSHScript => WSHScript.Name == (string)listQueries.SelectedItem);
            WSHScript W = new WSHScript();
            W.Name = txtName.Text;
            W.ScriptPath = txtPath.Text;
            W.Parameters = txtParams.Text;
            W.Timeout = Convert.ToInt32(numTimeout.Value);
            // Remove the old one if needed
            if (oldW != null) Program.Settings.WSHScripts.Remove(oldW);
            Program.Settings.WSHScripts.Add(W);

            // Now work out what we have to do to the list of objects
            if ((oldW != null) && (oldW.Name != W.Name))
                listQueries.Items.RemoveAt(listQueries.SelectedIndex);
            else if ((oldW == null) || (oldW.Name != W.Name))
                listQueries.Items.Add(txtName.Text);

            // Update so the right item is selected (and force refresh if it didn't move)
            listQueries.SelectedIndex = listQueries.Items.IndexOf(txtName.Text);
            listQueries_SelectedIndexChanged(sender, e);
        }

        /// <summary>
        /// Handles clicks on Delete button, including edit mode enable/disable, removing
        /// old item from the Program.Settings collection
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDelete_Click(object sender, EventArgs e)
        {

            WSHScript W = Program.Settings.WSHScripts.Find(WSHScript => WSHScript.Name == (string)listQueries.SelectedItem);
            Program.Settings.WSHScripts.Remove(W);
            listQueries.Items.RemoveAt(listQueries.SelectedIndex);
        }

        /// <summary>
        /// Handles selection changes in the list of scripts. Enforces editing/not editing mode, preloads
        /// data to each field based on selected ID.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listQueries_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtName.Enabled = false; txtName.ReadOnly = true;
            txtPath.Enabled = false; txtPath.ReadOnly = true;
            txtParams.Enabled = false; txtParams.ReadOnly = true;
            numTimeout.Enabled = false; numTimeout.ReadOnly = true;
            if (listQueries.SelectedItem != null)
            {
                WSHScript W = Program.Settings.WSHScripts.Find(WSHScript => WSHScript.Name == (string)listQueries.SelectedItem);
                btnEdit.Enabled = true;
                btnDelete.Enabled = true;
                txtName.Text = W.Name;
                txtPath.Text = W.ScriptPath;
                txtParams.Text = W.Parameters;
                numTimeout.Value = W.Timeout;
            }
            else
            {
                btnEdit.Enabled = false;
                btnDelete.Enabled = false;
                txtName.Text = WSHScript.Defaults.Name;
                txtPath.Text = WSHScript.Defaults.ScriptPath;
                txtParams.Text = WSHScript.Defaults.Parameters;
                numTimeout.Value = WSHScript.Defaults.Timeout;
            }
        }
    }
}
