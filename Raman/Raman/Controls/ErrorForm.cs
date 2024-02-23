using Point = System.Drawing.Point;

namespace Raman.Controls;

public partial class ErrorForm : Form
{
    private readonly Exception ex;

    private Label lblMessage;
    
    private const int MaxFormWidth = 800;

    public ErrorForm(string text, string caption, Exception ex)
    {
        this.ex = ex;
        Text = caption;
        InitializeComponent();
        AdditionalInitialization(text);
    }

    private void AdditionalInitialization(string errorMessage)
    {
        var errorIcon = new PictureBox
        {
            Image = SystemIcons.Error.ToBitmap(),
            Size = new Size(40, 40),
            // AutoSize = true,
        };
        
        var lblErrorMessage = new Label
        {
            Text = errorMessage,
            AutoSize = true,
            MaximumSize = new Size(MaxFormWidth - 20, 0),
        };

        var btnOk = new Button
        {
            Text = "OK",
            DialogResult = DialogResult.OK,
        };
        btnOk.Click += btnOk_Click;

        var btnCopyDetailsToClipboard = new Button
        {
            Text = "Copy Details to Clipboard",
            DialogResult = DialogResult.Cancel,
            Width = 150,
        };
        btnCopyDetailsToClipboard.Click += btnDetails_Click;

        var emptyLabel = new Label();
        emptyLabel.Size = new Size(10, 10);

        AutoSize = true;
        AutoSizeMode = AutoSizeMode.GrowAndShrink;

        Controls.Add(lblErrorMessage);
        Controls.Add(errorIcon);
        Controls.Add(btnCopyDetailsToClipboard);
        Controls.Add(btnOk);
        Controls.Add(emptyLabel);

        errorIcon.Location = new Point(10, 10);
        lblErrorMessage.Location = new Point(errorIcon.Right, 10);
        btnCopyDetailsToClipboard.Location = new Point(Width - btnOk.Width - btnCopyDetailsToClipboard.Width - 2 * 10, lblErrorMessage.Bottom + 10);
        btnOk.Location = new Point(Width - btnOk.Width - 10, lblErrorMessage.Bottom + 10);
        // add empty label here to make space around buttons
        emptyLabel.Location = new Point(btnOk.Right, btnOk.Bottom);
        
        // For KeyDown to work, KeyPreview must be set to true
        KeyPreview = true;
        KeyDown += ErrorForm_KeyDown;
    }
    
    private void ErrorForm_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Escape)
        {
            Close();
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