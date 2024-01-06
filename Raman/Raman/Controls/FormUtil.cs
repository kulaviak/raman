using System;
using System.Windows.Forms;

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
            MessageBox.Show(text, caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}