using NetScanner.Service;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Forms;

namespace NetScanner.Forms
{
    public partial class History : Form
    {
        private Storage store;
        private List<Storage.Snapshotfile> snapshotfiles;

        public History()
        {
            InitializeComponent();
        }

        private void History_Load(object sender, EventArgs e)
        {
            historyGrid.ColumnCount = 5;
            historyGrid.Columns[0].Name = "Date time";
            historyGrid.Columns[1].Name = "Network ID";
            historyGrid.Columns[2].Name = "Subnet Mask";
            historyGrid.Columns[3].Name = "No. of Record";
            historyGrid.Columns[4].Name = "key";
            historyGrid.Columns[4].Visible = false;
        }

        private void History_Shown(object sender, EventArgs e)
        {
            store = new Storage();
            foreach (var snapshotfile in store.Snapshotfiles)
            {
                historyGrid.Rows.Add(new string[] { snapshotfile.datetime.ToString(), snapshotfile.netid.ToString(), snapshotfile.mask.ToString(), snapshotfile.activeNodes.Count.ToString(), snapshotfile.key });
            }
        }

        private void historyGrid_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            string datetime = "";
            string key = "";
            foreach (DataGridViewRow row in historyGrid.SelectedRows)
            {
                datetime = row.Cells[0].Value.ToString();
                key = row.Cells["key"].Value.ToString();
            }
            var snapshotfile_ = store.findFile(datetime, key);
            toolStripStatusLabel.Text = "Please Wait";
            var ssform = new ViewSnapshot(snapshotfile_, this);
            ssform.Text = "Scan History - Network ID: " + snapshotfile_.netid + " SubnetMask: " + snapshotfile_.mask + " on: " + snapshotfile_.datetime.ToString();
            ssform.Show();
            this.Hide();
            //MessageBox.Show(this, snapshotfile_.activeNodes.Count.ToString(), key, MessageBoxButtons.OK);
        }

        private void homeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var appMenu = FormManager.Current.CreateForm<AppMenu>();
            appMenu.Show();
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string datetime = "";
            string key = "";
            string titleString = "";
            List<Storage.Snapshotfile> compareFiles = new List<Storage.Snapshotfile>();
            foreach (DataGridViewRow row in historyGrid.SelectedRows)
            {
                datetime = row.Cells[0].Value.ToString();
                key = row.Cells["key"].Value.ToString();
                titleString += datetime + " " + key + "|";
                var snapshotfile_ = store.findFile(datetime, key);
                compareFiles.Add(snapshotfile_);
            }
            toolStripStatusLabel.Text = "Please Wait";
            var compareForm = new Compare(compareFiles, this);
            compareForm.Text = "Compare Snapshots: " + titleString.Split('|')[0] + " vs " + titleString.Split('|')[1];
            compareForm.Show();
            this.Hide();
        }

        private void historyGrid_SelectionChanged(object sender, EventArgs e)
        {
            if (historyGrid.SelectedRows.Count > 2)
            {
                toolStripStatusLabel.Text = "Select only 2 records for comparison";
                for (int i = 2; i < historyGrid.SelectedRows.Count; i++)
                {
                    historyGrid.SelectedRows[i].Selected = false;
                }
            }
            if (historyGrid.SelectedRows.Count == 1)
            {
                toolStripStatusLabel.Text = "Ready";
            }
            if (historyGrid.SelectedRows.Count == 2)
            {
                btn_compare.Enabled = true;
            }
            else
            {
                btn_compare.Enabled = false;
            }
        }

        private void helpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            String str = global::NetScanner.Properties.Resources.helpURL;
            Process.Start(str);
        }
    }
}