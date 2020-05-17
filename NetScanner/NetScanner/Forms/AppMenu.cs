using System;
using System.Windows.Forms;

namespace NetScanner.Forms
{
    public partial class AppMenu : Form
    {
        public AppMenu()
        {
            InitializeComponent();
        }

        private void btn_exit_Click(object sender, EventArgs e)
        {
            var confirmResult = MessageBox.Show("Are you sure you want exit?", "Confirm",
                                     MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (confirmResult == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void btn_scan_Click(object sender, EventArgs e)
        {
            btn_scan.Enabled = false;
            btn_exit.Enabled = false;
            btn_sss.Enabled = false;
            toolStripStatusLabel.Text = "Please Wait. Scanning for available interfaces";
            var interfaces = FormManager.Current.CreateForm<Interfaces>();
            interfaces.Show();
            this.Close();
        }

        private void btn_sss_Click(object sender, EventArgs e)
        {
            btn_scan.Enabled = false;
            btn_exit.Enabled = false;
            btn_sss.Enabled = false;
            toolStripStatusLabel.Text = "Please Wait. Scanning for available interfaces";
            var interfaces = FormManager.Current.CreateForm<History>();
            interfaces.Show();
            this.Close();
        }
    }
}