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

    public void ShowPosition(ValuePoint position)
    {
        // Showing superscripts on label https://stackoverflow.com/questions/19682459/superscript-label-or-form-name
        lblPosition.Text = $"X Axis - Wavenumber (cm\u207B\u00B9): {Util.Format(position.X, 0)}  Y Axis - Intensity (cnts): {Util.Format(position.Y, 0)}";
    }
}