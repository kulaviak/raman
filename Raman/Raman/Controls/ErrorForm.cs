using Point = System.Drawing.Point;

namespace Raman.Controls;

public partial class ErrorForm : Form
{
    private readonly Exception ex;

    private Label lblMessage;

    public ErrorForm(string text, string caption, Exception ex)
    {
        this.ex = ex;
        InitializeComponent();
        lblMessage = new Label()
        {
            Text = text,
            Location = new Point(10, 10),
            AutoSize = true,
            MaximumSize = new Size(400, 600)
        };
        lblMessage.SizeChanged += LabelSizeChanged;
        Controls.Add(lblMessage);
        Text = caption;
        AutoSize = true;
        AutoSizeMode = AutoSizeMode.GrowAndShrink;
    }

    private void LabelSizeChanged(object sender, EventArgs e)
    {
        Label label = (Label)sender;
        if (label.Width > 200)
        {
            label.Width = 200;
        }
    }

    private void btnDetails_Click(object sender, EventArgs e)
    {
        try
        {
            var str = new ExceptionFormatter().ToString(ex);
            Clipboard.SetText(str);
        }
        catch (Exception ex)
        {
            MessageBox.Show("Saving error details to clipboard failed.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void btnOk_Click(object sender, EventArgs e)
    {
        Close();
    }
}