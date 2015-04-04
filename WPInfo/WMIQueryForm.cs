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
    public partial class WMIQueryForm : Form
    {
        public WMIQueryForm()
        {
            InitializeComponent();
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            txtName.Enabled = true; txtName.ReadOnly = false;
            txtNamespace.Enabled = true; txtNamespace.ReadOnly = false;
            txtQuery.Enabled = true; txtQuery.ReadOnly = false;
            txtName.Focus();
            txtName.Text = "";
            txtNamespace.Text = "root\\cimV2";
            txtQuery.Text = "";
            btnSave.Enabled = true;
        }

        private void listQueries_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtName.Enabled = false; txtName.ReadOnly = true;
            txtNamespace.Enabled = false; txtNamespace.ReadOnly = true;
            txtQuery.Enabled = false; txtQuery.ReadOnly = true;
            if (listQueries.SelectedItem != null)
            {
                btnEdit.Enabled = true;
                btnDelete.Enabled = true;
            }
            else
            {
                btnEdit.Enabled = false;
                btnDelete.Enabled = false;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            listQueries.Items.Add(txtName.Text);
            listQueries.SelectedIndex = listQueries.Items.IndexOf(txtName.Text);
        }
    }
}
