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
        public WSHScriptForm()
        {
            InitializeComponent();
            foreach (WSHScript W in Program.Settings.WSHScripts)
            {
                listQueries.Items.Add(W.Name);
            }
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            txtName.Enabled = true; txtName.ReadOnly = false;
            txtPath.Enabled = true; txtPath.ReadOnly = false;
            txtParams.Enabled = true; txtParams.ReadOnly = false;
            txtName.Focus();
            txtName.Text = "";
            txtPath.Text = "";
            txtParams.Text = "";
            btnSave.Enabled = true;
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            txtName.Enabled = true; txtName.ReadOnly = false;
            txtPath.Enabled = true; txtPath.ReadOnly = false;
            txtParams.Enabled = true; txtParams.ReadOnly = false;
            btnSave.Enabled = true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            WSHScript oldW = Program.Settings.WSHScripts.Find(WSHScript => WSHScript.Name == (string)listQueries.SelectedItem);
            WSHScript W = new WSHScript();
            W.Name = txtName.Text;
            W.ScriptPath = txtPath.Text;
            W.Parameters = txtParams.Text;
            if (oldW != null) Program.Settings.WSHScripts.Remove(oldW);
            Program.Settings.WSHScripts.Add(W);
            if ((oldW != null) && (oldW.Name != W.Name))
                listQueries.Items.RemoveAt(listQueries.SelectedIndex);
            else if ((oldW == null) || (oldW.Name != W.Name))
                listQueries.Items.Add(txtName.Text);
            listQueries.SelectedIndex = listQueries.Items.IndexOf(txtName.Text);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            WSHScript W = Program.Settings.WSHScripts.Find(WSHScript => WSHScript.Name == (string)listQueries.SelectedItem);
            Program.Settings.WSHScripts.Remove(W);
            listQueries.Items.RemoveAt(listQueries.SelectedIndex);
        }

        private void listQueries_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtName.Enabled = false; txtName.ReadOnly = true;
            txtPath.Enabled = false; txtPath.ReadOnly = true;
            txtParams.Enabled = false; txtParams.ReadOnly = true;
            if (listQueries.SelectedItem != null)
            {
                WSHScript W = Program.Settings.WSHScripts.Find(WSHScript => WSHScript.Name == (string)listQueries.SelectedItem);
                btnEdit.Enabled = true;
                btnDelete.Enabled = true;
                txtName.Text = W.Name;
                txtPath.Text = W.ScriptPath;
                txtParams.Text = W.Parameters;
            }
            else
            {
                btnEdit.Enabled = false;
                btnDelete.Enabled = false;
                txtName.Text = "";
                txtPath.Text = "";
                txtParams.Text = "";
            }
        }
    }
}
