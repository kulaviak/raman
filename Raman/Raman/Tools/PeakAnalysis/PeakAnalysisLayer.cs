using Raman.Controls;
using Raman.Drawing;

namespace Raman.Tools.PeakAnalysis;

public class PeakAnalysisLayer : LayerBase
{
    private readonly CanvasPanel canvasPanel;

    private static Pen PEN = Pens.Orange;

    private ValuePoint start;
    
    private ValuePoint currentPoint;

    public List<Peak> Peaks { get; set; } = new List<Peak>();

    private List<Peak> VisiblePeaks => Peaks.Where(peak => peak.Spectrum.IsVisible).ToList();
    
    public bool IsExported { get; set; }
    
    public bool IsPeakAddedToAllSpectra { get; set; }

    public PeakAnalysisLayer(CanvasCoordSystem coordSystem, CanvasPanel canvasPanel) : base(coordSystem)
    {
        this.canvasPanel = canvasPanel;
    }
        
    public override void HandleMouseDown(object sender, MouseEventArgs e)
    {
        if (e.Button == MouseButtons.Left)
        {
            if (start == null)
            {
                start = CoordSystem.ToValuePoint(e.Location.X, e.Location.Y);
            }
            else
            {
                var end = CoordSystem.ToValuePoint(e.Location.X, e.Location.Y);
                if (IsPeakAddedToAllSpectra)
                {
                    var peaks = GetPeaksForAllSpectra(start, end);
                    Peaks.AddRange(peaks);
                }
                else
                {
                    var point = canvasPanel.CoordSystem.ToValuePoint(e.Location);
                    var spectrum = new NewClosestSpectrumCalculator().GetClosestSpectrum(canvasPanel.VisibleSpectra, point);                    
                    var peak = GetPeakForSpectrum(spectrum, start, end);
                    if (peak != null)
                    {
                        Peaks.Add(peak);
                    }
                }
                IsExported = false;
                start = null;
            }
            Refresh();
        }
        else if (e.Button == MouseButtons.Middle && Util.IsCtrlKeyPressed())
        {
            RemoveClosestPeak(e.Location);
            Refresh();
        }
        else if (e.Button == MouseButtons.Right)
        {
            ShowContextMenu(e.Location);
        }
    }

    private List<Peak> GetPeaksForAllSpectra(ValuePoint start, ValuePoint end)
    {
        var ret = new List<Peak>();
        foreach (var spectrum in canvasPanel.VisibleSpectra)
        {
            var peak = GetPeakForSpectrum(spectrum, start, end);
            if (peak != null)
            {
                ret.Add(peak);
            }
        }
        return ret;
    }

    private Peak GetPeakForSpectrum(Spectrum spectrum, ValuePoint userDefinedStart, ValuePoint userDefinedEnd)
    {
        var startPointAtSpectrum = GetPointAtSpectrum(userDefinedStart, spectrum);
        var endPointAtSpectrum = GetPointAtSpectrum(userDefinedEnd, spectrum);
        var top = GetTopPoint(startPointAtSpectrum, endPointAtSpectrum, spectrum);
        // it can happen if user selects the point wrong way, like double click, so there is no point between start and end point
        if (top == null)
        {
            return null;
        }
        // if end is before start, then swap it
        if (endPointAtSpectrum.X < startPointAtSpectrum.X)
        {
            (startPointAtSpectrum, endPointAtSpectrum) = (endPointAtSpectrum, startPointAtSpectrum);
        }
        var peak = new Peak(startPointAtSpectrum, endPointAtSpectrum, top, spectrum);
        return peak;
    }

    private ValuePoint GetPointAtSpectrum(ValuePoint point, Spectrum spectrum)
    {
        var y = spectrum.GetValue(point.X);
        if (y != null)
        {
            var ret = new ValuePoint(point.X, y.Value);
            return ret;
        }
        // if the x value of the point is out of range the spectrum, then return first or last point of spectrum
        else
        {
            if (point.X < spectrum.Points.First().X)
            {
                return spectrum.Points.First();
            }
            else
            {
                return spectrum.Points.Last();
            }
        }
    }

    public override void HandleMouseMove(object sender, MouseEventArgs e)
    {
        currentPoint = CoordSystem.ToValuePoint(e.Location.X, e.Location.Y);
        canvasPanel.Refresh();
    }

    public void Reset()
    {
        if (!Peaks.Any())
        {
            MessageUtil.ShowInfo("There are no peak to reset.", "Information");
            return;
        }
        Peaks.Clear();
        Refresh();
    }
        
    public override void Draw(Graphics graphics)
    {
        DrawPeaks(graphics);
        DrawCurrentLine(graphics);
    }

    public override void HandleKeyPress(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Escape && currentPoint != null)
        {
            start = null;
            Refresh();
        }
    }

    private void DrawCurrentLine(Graphics graphics)
    {
        if (start != null && currentPoint != null)
        {
            new CanvasDrawer(CoordSystem, graphics).DrawLine(start, currentPoint, PEN);
        }
    }

    private void DrawPeaks(Graphics graphics)
    {
        var visiblePeaks = Peaks.Where(x => x.Spectrum.IsVisible).ToList();
        foreach (var peak in visiblePeaks)
        {
            DrawPeak(peak, graphics);
        }
    }

    private void DrawPeak(Peak peak, Graphics graphics)
    {
        var canvasDrawer = new CanvasDrawer(CoordSystem, graphics);
        canvasDrawer.DrawLine(peak.Base, PEN);
        canvasDrawer.DrawLine(peak.Vertical, PEN);
    }
    
    private ValuePoint GetTopPoint(ValuePoint start, ValuePoint end, Spectrum spectrum)
    {
        var ret = spectrum.Points.Where(point => start.X < point.X && point.X <= end.X).MaxByOrDefault(point => point.Y);
        return ret;
    }

    /// <summary>
    /// Expects, that user clicks on vertical or bottom line of the peak. So it finds the closest peak according to it.
    /// </summary>
    private void RemoveClosestPeak(Point location)
    {
        var peak = new PeakToRemoveCalculator().GetPeakToRemove(VisiblePeaks, CoordSystem, location);
        if (peak != null)
        {
            Peaks = Peaks.Where(x => x != peak).ToList();
        }
        Refresh();
    }
    
    private void Refresh()
    {
        canvasPanel.Refresh();
    }

    private void ShowContextMenu(Point point)
    {
        var contextMenu = new ContextMenuStrip();
        contextMenu.Items.Add("Remove Closest Line", null, (_, _) => RemoveClosestPeak(point));
        contextMenu.Show(canvasPanel, point);
    }

    public void ExportPeaks(string filePath)
    {
        if (!VisiblePeaks.Any())
        {
            MessageUtil.ShowInfo("There are no peaks to export.", "Information");
            return;
        }
        new PeakAnalysisExporter().ExportPeaks(filePath, VisiblePeaks);
    }
}