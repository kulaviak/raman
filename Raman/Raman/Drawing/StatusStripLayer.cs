using Raman.Controls;

namespace Raman.Drawing;

public class StatusStripLayer(CanvasCoordSystem coordSystem, AppStatusStrip statusStrip, CanvasPanel canvasPanel) : LayerBase(coordSystem)
{
    public override void HandleMouseMove(object sender, MouseEventArgs e)
    {
        var position = CoordSystem.ToValuePoint(e.Location.X, e.Location.Y);
        // Showing superscripts on label https://stackoverflow.com/questions/19682459/superscript-label-or-form-name
        var positionText = $"X Axis - Wavenumber (cm\u207B\u00B9): {Util.Format(position.X, 0)}  Y Axis - Intensity (cnts): {Util.Format(position.Y, 0)}";
        var chart = GetClosestChart(position);
        var text = chart != null ? $"{positionText}     {chart.Name}" : positionText;
        statusStrip.ShowText(text);
    }

    private Chart GetClosestChart(ValuePoint position)
    {
        var ret = Util.GetClosestChart(canvasPanel.Charts, position);
        return ret;
    }
}