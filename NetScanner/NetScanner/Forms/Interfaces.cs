using NetScanner.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Forms;

namespace NetScanner.Forms
{
    public partial class Interfaces : Form
    {
        private IList<NetInterface> ni;

        public Interfaces()
        {
            InitializeComponent();
        }

        private void Interfaces_Load(object sender, EventArgs e)
        {
            ni = new NetInterface().getInterfaces();
            interfaceGrid.ColumnCount = 6;
            interfaceGrid.Columns[0].Name = "Mac Address";
            interfaceGrid.Columns[1].Name = "Interface Name";
            interfaceGrid.Columns[2].Name = "Network ID";
            interfaceGrid.Columns[3].Name = "Subnet mask";
            interfaceGrid.Columns[4].Name = "NIC Vendor";
            interfaceGrid.Columns[5].Name = "Your IP";


            foreach (var interface_ in ni)
            {
                string[] row = new string[] { interface_.macAddress, interface_.name, interface_.netv4, interface_.mask, interface_.vendor, interface_.ipv4 };
                interfaceGrid.Rows.Add(row);
            }
        }

        private void interfaceGrid_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            string mac = "";
            string name = "";
            string ipv4 = "";
            string mask = "";
            string vendor = "";
            string yipv4 = "";
            foreach (DataGridViewRow row in interfaceGrid.SelectedRows)
            {
                mac = row.Cells[0].Value.ToString();
                name = row.Cells[1].Value.ToString();
                ipv4 = row.Cells[2].Value.ToString();
                mask = row.Cells[3].Value.ToString();
                vendor = row.Cells[4].Value.ToString();
                yipv4 = row.Cells[5].Value.ToString();
            }

            var confirmResult = MessageBox.Show("Are you sure that you want to scan networkID " + ipv4 + " subnet mask " + mask + " on " + name + "?", "Confirm",
                                     MessageBoxButtons.YesNo,MessageBoxIcon.Warning);
            if (confirmResult == DialogResult.Yes)
            {
                toolStripStatusLabel.Text = "Please Wait. Preparing to scan Network ID: " + ipv4 + " SubnetMask: " + mask;
                var scanform = new Scan(ipv4, mask, yipv4, this);
                scanform.Text = "Scan - Network ID: " + ipv4 + " SubnetMask: " + mask;
                scanform.Show();
                this.Hide();
            }
            else
            {
                // If 'No', do something here.
            }
        }

        private void refreshToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Controls.Clear();
            this.InitializeComponent();
            this.Interfaces_Load(sender, e);
        }

        private void homeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var appMenu = FormManager.Current.CreateForm<AppMenu>();
            appMenu.Show();
            this.Close();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var confirmResult = MessageBox.Show("Are you sure that you want exit?", "Confirm",
                                     MessageBoxButtons.YesNo);
            if (confirmResult == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void helpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            String str = global::NetScanner.Properties.Resources.helpURL;
            Process.Start(str);
        }
    }
}