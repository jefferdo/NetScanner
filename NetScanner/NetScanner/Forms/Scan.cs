using NetScanner.Model;
using NetScanner.Service;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Net;
using System.Windows.Forms;

namespace NetScanner.Forms
{
    public partial class Scan : Form
    {
        private IPAddress mask;
        private IPAddress yip;
        private IPAddress networkID;
        private Form caller;
        private Network network;
        private IList<Storage.activeNode> activeNodes;
        private double progress = 0;
        private int hostCount = 0;

        public Scan(string networkID, string mask, string yourIP, Form caller)
        {
            InitializeComponent();
            backgroundWorker.WorkerReportsProgress = true;
            backgroundWorker.WorkerSupportsCancellation = true;
            this.networkID = IPAddress.Parse(networkID);
            this.mask = IPAddress.Parse(mask);
            this.yip = IPAddress.Parse(yourIP);
            this.caller = caller;
        }

        private void Scan_Load(object sender, EventArgs e)
        {
            scanGrid.ColumnCount = 5;
            scanGrid.Columns[0].Name = "IP Address";
            scanGrid.Columns[1].Name = "Physical Address";
            scanGrid.Columns[2].Name = "Hostname";
            scanGrid.Columns[3].Name = "NIC Vendor";
            scanGrid.Columns[4].Name = "Status";
        }

        private void Scan_FormClosed(object sender, FormClosedEventArgs e)
        {
            caller.Show();
        }

        private void Scan_Shown(object sender, EventArgs e)
        {
            backgroundWorker.RunWorkerAsync();
            btn_save.Enabled = false;
            btn_scan.Text = "Cancel Scan";
        }

        public IList<Storage.activeNode> scan(IList<IPAddress> ip_range, DoWorkEventArgs e)
        {
            this.activeNodes = new List<Storage.activeNode>();
            hostCount = ip_range.Count;
            int i = 1;
            foreach (var ip_ in ip_range)
            {
                if ((backgroundWorker.CancellationPending == true))
                {
                    e.Cancel = true;
                    break;
                }
                if (ip_.ToString() == this.yip.ToString())
                {
                    Debug.WriteLine("Own IP. Skipping..");
                    continue;
                }
                try
                {
                    var hostname_ = NetScan.GetMachineNameFromIPAddress(ip_);
                    if (NetScan.pingHost(ip_.ToString()))
                    {
                        Debug.WriteLine(hostname_ + " Available ");
                        var mac_o = NetScan.getMacByIp(ip_);
                        var mac_p = new PhysicalAddressProcessor(mac_o);
                        var mac_ = PhysicalAddressProcessor.getMacString(mac_o);
                        var vendor_ = mac_p.getNICVendor();
                        activeNodes.Add(new Storage.activeNode
                        {
                            ip = ip_,
                            hostname = hostname_,
                            mac = mac_o,
                            vendor = vendor_
                        });
                        scanGrid.Invoke((Action)(() => scanGrid.Rows.Add(new string[] { ip_.ToString(), mac_.ToString(), hostname_, vendor_, "Active" })));
                    }
                }
                catch (NullReferenceException ex)
                {
                    Debug.Write(ex.Message);
                }
                catch (Exception ex)
                {
                    Debug.Write(ex.Message);
                }
                finally
                {
                    progress = ((double)100 / (double)hostCount) * (double)i;
                    backgroundWorker.ReportProgress((int)Math.Ceiling(progress));
                    Debug.WriteLine(ip_.ToString() + " " + Math.Ceiling(progress).ToString("F2"));
                    i++;
                }
            }

            return this.activeNodes;
        }

        private void backgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            toolStripProgressBar.Visible = true;
            toolStripStatusLabel.Text = "Scanning completed " + progress.ToString("F2") + "%";
            toolStripProgressBar.Value = e.ProgressPercentage;
        }

        private void backgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            toolStripProgressBar.Value = 0;
            toolStripProgressBar.Visible = false;
            toolStripStatusLabel.Text = "Scan completed";
            btn_scan.Text = "Scan";
            btn_scan.Enabled = true;
            btn_save.Enabled = true;
        }

        private void backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            toolStripStatusLabel.Text = "Initiating Scan";
            network = new Network(networkID, mask);
            activeNodes = scan(network.getIPRange(), e);

            /*foreach (var activeNode in activeNodes)
            {
            }*/
        }

        private void btn_scan_Click(object sender, EventArgs e)
        {
            if (backgroundWorker.IsBusy)
            {
                var confirmResult = MessageBox.Show("Are you sure you want to cancel?", "Cancel scanning",
                                     MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (confirmResult == DialogResult.Yes)
                {
                    backgroundWorker.CancelAsync();
                    btn_scan.Text = "Please Wait";
                    btn_scan.Enabled = false;
                }
            }
            else
            {
                scanGrid.Rows.Clear();
                activeNodes.Clear();
                backgroundWorker.RunWorkerAsync();
                btn_scan.Text = "Cancel Scan";
                btn_save.Enabled = false;
            }
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            var x = activeNodes.Count;
            if (x != 0)
            {
                var confirmResult = MessageBox.Show("Are you sure you want save the current scan details? It contains " + activeNodes.Count.ToString() + " active device information.", "Confirm",
                                                     MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (confirmResult == DialogResult.Yes)
                {
                    new Storage(DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss"), activeNodes, networkID, mask);
                    btn_save.Enabled = false;
                }
            }
            else
            {
                MessageBox.Show(this, "Nothing to save. Start scanning process and wait for information to appear", "Nothing to save", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void helpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            String str = global::NetScanner.Properties.Resources.helpURL;
            Process.Start(str);
        }

        private void reloadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Controls.Clear();
            this.InitializeComponent();
            this.Scan_Load(sender, e);
        }
    }
}