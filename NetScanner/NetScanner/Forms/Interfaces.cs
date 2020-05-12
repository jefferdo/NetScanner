using NetScanner.Model;
using System;
using System.Collections.Generic;
using System.Threading;
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
            interfaceGrid.ColumnCount = 5;
            interfaceGrid.Columns[0].Name = "Mac Address";
            interfaceGrid.Columns[1].Name = "Interface Name";
            interfaceGrid.Columns[2].Name = "IP Address";
            interfaceGrid.Columns[3].Name = "Subnet mask";
            interfaceGrid.Columns[4].Name = "NIC Vendor";

            foreach (var interface_ in ni)
            {
                string[] row = new string[] { interface_.macAddress, interface_.name, interface_.ipv4, interface_.mask, interface_.vendor };
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
            foreach (DataGridViewRow row in interfaceGrid.SelectedRows)
            {
                mac = row.Cells[0].Value.ToString();
                name = row.Cells[1].Value.ToString();
                ipv4 = row.Cells[2].Value.ToString();
                mask = row.Cells[3].Value.ToString();
                vendor = row.Cells[4].Value.ToString();
            }

            var confirmResult = MessageBox.Show("Are you sure that you want to scan networkID " + ipv4 + " subnet mask " + mask + " on " + name + "?", "Confirm",
                                     MessageBoxButtons.YesNo);
            if (confirmResult == DialogResult.Yes)
            {
                // If 'Yes', do something here.
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
    }
}