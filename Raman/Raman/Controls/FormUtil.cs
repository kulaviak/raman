using NLog;

namespace Raman.Controls;

public abstract class FormUtil
{

    private static Logger _logger = LogManager.GetCurrentClassLogger();
        
    public static void ShowUserError(string text, string caption)
    {
        MessageBox.Show(text, caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
    }
    
    public static void ShowAppError(string msg, string caption, Exception ex)
    {
        msg = GetMessage(msg, ex);
        new ErrorForm(msg, caption, ex).ShowDialog();
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