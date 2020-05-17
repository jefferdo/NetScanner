using NetScanner.Service;
using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace NetScanner.Forms
{
    public partial class ViewSnapshot : Form
    {
        private Form caller;
        private Storage.Snapshotfile snapshotfile;

        public ViewSnapshot(Storage.Snapshotfile snapshotfile, Form caller)
        {
            this.snapshotfile = snapshotfile;
            this.caller = caller;
            InitializeComponent();
        }

        private void ViewSnapshot_Load(object sender, EventArgs e)
        {
            snapshotGrid.ColumnCount = 4;
            snapshotGrid.Columns[0].Name = "IP Address";
            snapshotGrid.Columns[1].Name = "Physical Address";
            snapshotGrid.Columns[2].Name = "Hostname";
            snapshotGrid.Columns[3].Name = "NIC Vendor";
        }

        private void ViewSnapshot_FormClosed(object sender, FormClosedEventArgs e)
        {
            caller.Show();
        }

        private void ViewSnapshot_Shown(object sender, EventArgs e)
        {
            foreach (var activeNode in snapshotfile.activeNodes)
            {
                var mac_ = new PhysicalAddressProcessor(activeNode.mac);
                snapshotGrid.Rows.Add(new string[] { activeNode.ip.ToString(), mac_.getMacString(), activeNode.hostname, mac_.getNICVendor() });
            }
        }

        private void helpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            String str = global::NetScanner.Properties.Resources.helpURL;
            Process.Start(str);
        }
    }
}