namespace Raman.Controls;

public class AppStatusStrip : StatusStrip
{
    private ToolStripStatusLabel lblPosition;

    public AppStatusStrip()
    {
        InitializeComponent();
    }
    
    public void ShowText(string text)
    {
        lblPosition.Text = text;
    }

    private void InitializeComponent()
    {
        lblPosition = new ToolStripStatusLabel();
        Items.AddRange(new ToolStripItem[] {lblPosition});   
    }
}