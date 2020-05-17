using NetScanner.Service;
using System.Windows.Forms;

namespace NetScanner.Forms
{
    public partial class Compare : Form
    {
        private Storage.Snapshotfile snapshotfile;
        private Form caller;

        public Compare(Storage.Snapshotfile snapshotfile, Form caller)
        {
            this.caller = caller;

            InitializeComponent();
        }

        private void Compare_FormClosed(object sender, FormClosedEventArgs e)
        {
        }
    }
}