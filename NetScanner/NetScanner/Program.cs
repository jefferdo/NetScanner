using NetScanner.Model;
using System;
using System.Windows.Forms;

namespace NetScanner
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(FormManager.Current);
        }
    }
}