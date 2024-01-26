using System;
using System.Windows.Forms;
using TurboLabs.EMOS.Common;

namespace Raman.Controls
{
    public abstract class FormUtil
    {
        public static void ShowUserError(string text, string caption)
        {
            MessageBox.Show(text, caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        
        public static void ShowAppError(string text, string caption, Exception ex)
        {
            text += " Note: Error information was copied to clipboard.";
            MessageBox.Show(text, caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            var str = new ExceptionFormatter().ToString(ex);
            Clipboard.SetText(str);           
        }
        
        public static void ShowInfo(string text, string caption)
        {
            MessageBox.Show(text, caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}