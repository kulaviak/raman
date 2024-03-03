namespace Raman.Controls;

public class AppStatusStrip : StatusStrip
{
    private ToolStripStatusLabel lblPosition;

    public AppStatusStrip()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        lblPosition = new ToolStripStatusLabel();
        Items.AddRange(new ToolStripItem[] {lblPosition});   
    }

    public void ShowText(string text)
    {
        lblPosition.Text = text;
    }
}