using Raman.Drawing;

namespace Raman.Controls;

public class StatusStripLayer : LayerBase
{
    private readonly AppStatusStrip statusStrip;
    
    private readonly CanvasPanel canvasPanel;
    
    private Point location;
    
    private Timer timer;

    private const int DELAYED_RESPONSE_TIME_IN_MILISECONDS = 20;

    private const double MAX_ALLOWED_DISTANCE_FOR_CLOSEST_SPECTRUM = 50;
    
    public StatusStripLayer(CanvasCoordSystem coordSystem, AppStatusStrip statusStrip, CanvasPanel canvasPanel) : base(coordSystem)
    {
        this.statusStrip = statusStrip;
        this.canvasPanel = canvasPanel;
    }
    
    public override void HandleMouseMove(object sender, MouseEventArgs e)
    {
        location = e.Location;
        if (timer != null)
        {
            timer.Stop();
        } 
        // improve performance, don't react on every mouse location change by delaying the response
        timer = new Timer {Interval = DELAYED_RESPONSE_TIME_IN_MILISECONDS};
        timer.Tick += (_, _) =>
        {
            ShowStatusBarInfo();
            timer.Stop();
        }; 
        timer.Start();
    }
    
    private void ShowStatusBarInfo()
    {
        var spectrum = new ClosestSpectrumCalculator().GetClosestSpectrum(canvasPanel.VisibleSpectra, location, CoordSystem, MAX_ALLOWED_DISTANCE_FOR_CLOSEST_SPECTRUM);
        var positionText = GetPositionText(location);
        var text = spectrum != null ? $"{positionText}     {spectrum.Name}" : positionText;
        statusStrip.ShowText(text);
    }
    
    private string GetPositionText(Point location)
    {
        var point = CoordSystem.ToValuePoint(location);
        // Showing superscripts on label https://stackoverflow.com/questions/19682459/superscript-label-or-form-name
        var ret = $"X Axis (a.u.): {Util.Format(point.X, 0)}  Y Axis (a.u.): {Util.Format(point.Y, 0)}";
        return ret;
    }
 }