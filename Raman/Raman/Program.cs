using NLog;
using Raman.Controls;

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
            AppDomain.CurrentDomain.UnhandledException += UnexpectedExceptionHandler;
            Application.Run(new MainForm());
        }

        static void UnexpectedExceptionHandler(object sender, UnhandledExceptionEventArgs args)
        {
            var e = (Exception) args.ExceptionObject;
            LogManager.GetCurrentClassLogger().Error(e, "Unexpected error happened");
            try
            {
                MessageUtil.ShowAppError("Unexpected error happened. Please close application.", "Error", e);
            }
            catch (Exception)
            {
                // fallback when for some reason showing previous error failed 
                MessageBox.Show("Unexpected error happened. Please close application.", "Unexpected error", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
    }
}