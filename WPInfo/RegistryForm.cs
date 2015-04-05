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
    public partial class RegistryForm : Form
    {
        public RegistryForm()
        {
            InitializeComponent();
            foreach (RegValue R in Program.Settings.RegValues)
            {
                listQueries.Items.Add(R.Name);
            }
            cbHive.Items.Clear();
            cbHive.Items.Add(RegValue.HKCR);
            cbHive.Items.Add(RegValue.HKCU);
            cbHive.Items.Add(RegValue.HKLM);
            cbHive.Items.Add(RegValue.HKU);
            cbHive.Items.Add(RegValue.HKCC);
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            txtName.Enabled = true; txtName.ReadOnly = false;
            cbHive.Enabled = true;
            txtPath.Enabled = true; txtPath.ReadOnly = false;
            txtValue.Enabled = true; txtValue.ReadOnly = false;
            txtName.Focus();
            txtName.Text = "";
            txtPath.Text = "";
            txtValue.Text = "";
            btnSave.Enabled = true;
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            txtName.Enabled = true; txtName.ReadOnly = false;
            cbHive.Enabled = true;
            txtPath.Enabled = true; txtPath.ReadOnly = false;
            txtValue.Enabled = true; txtValue.ReadOnly = false;
            btnSave.Enabled = true;
        }

        private void listQueries_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtName.Enabled = false; txtName.ReadOnly = true;
            cbHive.Enabled = false;
            txtPath.Enabled = false; txtPath.ReadOnly = true;
            txtValue.Enabled = false; txtValue.ReadOnly = true;
            if (listQueries.SelectedItem != null)
            {
                RegValue R = Program.Settings.RegValues.Find(RegValue => RegValue.Name == (string)listQueries.SelectedItem);
                btnEdit.Enabled = true;
                btnDelete.Enabled = true;
                txtName.Text = R.Name;
                cbHive.SelectedIndex = cbHive.Items.IndexOf(R.Key);
                txtPath.Text = R.Path;
                txtValue.Text = R.Value;
            }
            else
            {
                btnEdit.Enabled = false;
                btnDelete.Enabled = false;
                txtName.Text = "";
                cbHive.SelectedIndex = 0;
                txtPath.Text = "";
                txtValue.Text = "";
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            RegValue oldR = Program.Settings.RegValues.Find(RegValue => RegValue.Name == (string)listQueries.SelectedItem);
            RegValue R = new RegValue();
            R.Name = txtName.Text;
            R.Key = (string)cbHive.SelectedItem;
            R.Path = txtPath.Text;
            R.Value = txtValue.Text;
            if (oldR != null) Program.Settings.RegValues.Remove(oldR);
            Program.Settings.RegValues.Add(R);
            if ((oldR != null) && (oldR.Name != R.Name))
                listQueries.Items.RemoveAt(listQueries.SelectedIndex);
            else if ((oldR == null) || (oldR.Name != R.Name))
                listQueries.Items.Add(txtName.Text);
            listQueries.SelectedIndex = listQueries.Items.IndexOf(txtName.Text);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            RegValue R = Program.Settings.RegValues.Find(RegValue => RegValue.Name == (string)listQueries.SelectedItem);
            Program.Settings.RegValues.Remove(R);
            listQueries.Items.RemoveAt(listQueries.SelectedIndex);
        }

    }
}
