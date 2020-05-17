using NetScanner.Service;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Forms;

namespace NetScanner.Forms
{
    public partial class Compare : Form
    {
        private List<Storage.Snapshotfile> compareFiles;
        private Storage.Snapshotfile compareFile1;
        private Storage.Snapshotfile compareFile2;
        private Form caller;

        public Compare(List<Storage.Snapshotfile> compareFiles, Form caller)
        {
            this.caller = caller;
            compareFile1 = compareFiles[0];
            compareFile2 = compareFiles[1];
            InitializeComponent();
        }

        private void Compare_FormClosed(object sender, FormClosedEventArgs e)
        {
            caller.Show();
        }

        private void exitToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            this.Close();
        }

        private void Compare_Load(object sender, System.EventArgs e)
        {
            lbl_ss1.Text = compareFile1.datetime + " (Key: " + compareFile1.key + ")";
            lbl_ss2.Text = compareFile2.datetime + " (Key: " + compareFile2.key + ")";

            compareGrid1.ColumnCount = 4;
            compareGrid1.Columns[0].Name = "IP Address";
            compareGrid1.Columns[1].Name = "Physical Address";
            compareGrid1.Columns[2].Name = "Hostname";
            compareGrid1.Columns[3].Name = "NIC Vendor";

            compareGrid2.ColumnCount = 4;
            compareGrid2.Columns[0].Name = "IP Address";
            compareGrid2.Columns[1].Name = "Physical Address";
            compareGrid2.Columns[2].Name = "Hostname";
            compareGrid2.Columns[3].Name = "NIC Vendor";
        }

        private void Compare_Shown(object sender, System.EventArgs e)
        {
            foreach (var activeNode in compareFile1.activeNodes)
            {
                var mac_ = new PhysicalAddressProcessor(activeNode.mac);
                compareGrid1.Rows.Add(new string[] { activeNode.ip.ToString(), mac_.getMacString(), activeNode.hostname, mac_.getNICVendor() });
            }
            foreach (var activeNode in compareFile2.activeNodes)
            {
                var mac_ = new PhysicalAddressProcessor(activeNode.mac);
                compareGrid2.Rows.Add(new string[] { activeNode.ip.ToString(), mac_.getMacString(), activeNode.hostname, mac_.getNICVendor() });
            }
        }

        private void compareGrid1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            foreach (DataGridViewRow row1 in compareGrid1.SelectedRows)
            {
                string searchValue = row1.Cells[1].Value.ToString();

                try
                {
                    foreach (DataGridViewRow row2 in compareGrid2.Rows)
                    {
                        if (row2.Cells[1].Value.ToString().Equals(searchValue))
                        {
                            row2.Selected = true;
                            break;
                        }
                    }
                }
                catch (Exception exc)
                {
                    toolStripStatusLabel.Text = "Match Not Found";
                }
            }
        }

        private void compareGrid2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            foreach (DataGridViewRow row2 in compareGrid2.SelectedRows)
            {
                string searchValue = row2.Cells[1].Value.ToString();

                try
                {
                    foreach (DataGridViewRow row1 in compareGrid1.Rows)
                    {
                        if (row1.Cells[1].Value.ToString().Equals(searchValue))
                        {
                            row1.Selected = true;
                            break;
                        }
                    }
                }
                catch (Exception exc)
                {
                    toolStripStatusLabel.Text = "Match Not Found";
                }
            }
        }

        private void helpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            String str = global::NetScanner.Properties.Resources.helpURL;
            Process.Start(str);
        }
    }
}