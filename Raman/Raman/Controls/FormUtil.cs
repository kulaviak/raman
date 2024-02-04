using NLog;

namespace Raman.Controls;

public abstract class FormUtil
{

    private static Logger _logger = LogManager.GetCurrentClassLogger();
        
    public static void ShowUserError(string text, string caption)
    {
        MessageBox.Show(text, caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
    }
    
    [Obsolete]
    public static void ShowAppError(string text, string caption, Exception ex)
    {
        MessageBox.Show(text, caption + ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
        _logger.Error(ex, caption);
    }
    
    public static void ShowErrorOnUserAction(string msg, string caption, Exception ex)
    {
        msg = GetMessage(msg, ex);
        new ErrorForm(msg, caption, ex).ShowDialog();
        // MessageBox.Show(msg, caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
        _logger.Error(ex, msg);
    }

    private static string GetMessage(string msg, Exception ex)
    {
        var str = "";
        while (ex is AppException)
        {
            str += " Reason: " + ex.Message;
            ex = ex.InnerException;
        }
        var ret = msg + str;
        return ret;
    }

    public static void ShowInfo(string text, string caption)
    {
        MessageBox.Show(text, caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
    }
    
    public static DialogResult ShowQuestion(string text, string caption)
    {
        return MessageBox.Show(text, caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
    }
}