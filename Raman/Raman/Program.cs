using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Raman
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(UnexpectedExceptionHandler);
            Application.Run(new RamanForm());
        }

        static void UnexpectedExceptionHandler(object sender, UnhandledExceptionEventArgs args)
        {
            var e = (Exception) args.ExceptionObject;
            MessageBox.Show("Unexpected error happened. Please close application.", "Unexpected error", MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        }
    }
}