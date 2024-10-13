using System.Threading;
using NLog;
using Raman.Controls;

namespace Raman
{
    static class Program
    {

        private static string UNEXPECTED_ERROR_MESSAGE =
            "Unexpected error occurred. Application might start to work incorrectly. It is better to close application.";
        
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            // in production mode catch all unhandled exceptions
            // https://stackoverflow.com/questions/5762526/how-can-i-make-something-that-catches-all-unhandled-exceptions-in-a-winforms-a
            if (!System.Diagnostics.Debugger.IsAttached)
            {
                // Add the event handler for handling UI thread exceptions to the event.
                Application.ThreadException += UIThreadException;
                // Set the unhandled exception mode to force all Windows Forms errors
                // to go through our handler.
                Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
                // Add the event handler for handling non-UI thread exceptions to the event. 
                AppDomain.CurrentDomain.UnhandledException += UnexpectedExceptionHandler;
            }
            Application.Run(new MainForm());
        }

        private static void UIThreadException(object sender, ThreadExceptionEventArgs args)
        {
            try
            {
                var e = args.Exception;
                MessageUtil.ShowAppError(UNEXPECTED_ERROR_MESSAGE, "Error", e);
            }
            catch (Exception)
            {
                // fallback when for som e reason showing previous error failed 
                MessageBox.Show(UNEXPECTED_ERROR_MESSAGE, "Unexpected error", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
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