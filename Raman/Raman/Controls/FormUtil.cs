using NLog;

namespace Raman.Controls;

public abstract class FormUtil
{

    private static Logger _logger = LogManager.GetCurrentClassLogger();
        
    public static void ShowUserError(string text, string caption)
    {
        MessageBox.Show(text, caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
    }
        
    public static void ShowAppError(string text, string caption, Exception ex)
    {
        // text += " Note: Error information was copied to clipboard.";
        MessageBox.Show(text, caption + ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
        _logger.Error(ex, caption);
        // var str = new ExceptionFormatter().ToString(ex);
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