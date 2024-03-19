using Raman.Controls;
using Raman.Drawing;

namespace Raman.Tools.CutOff;

public class CutOffLayer(CanvasCoordSystem coordSystem, CanvasPanel canvasPanel) : LayerBase(coordSystem)
{
    public ValuePoint Start => start;
    
    public ValuePoint End => end;

    private ValuePoint start;
    
    private ValuePoint end;

    private static Color COLOR = Color.Red;

    private Stack<List<Spectrum>> spectrumHistory = new Stack<List<Spectrum>>();
    
    private Stack<ValuePoint> startHistory = new Stack<ValuePoint>();
    
    private Stack<ValuePoint> endHistory = new Stack<ValuePoint>();

    public void CutOff()
    {
        if (start == null)
        {
            MessageUtil.ShowInfo("Start point is not set.", "Information");
            return;
        }
        if (end == null)
        {
            MessageUtil.ShowInfo("End point is not set.", "Information");
            return;
        }
        startHistory.Push(start);
        endHistory.Push(end);
        spectrumHistory.Push(canvasPanel.Spectra);
        canvasPanel.Spectra = GetCutOffSpectra(canvasPanel.Spectra, start.X, end.X);
        start = null;
        end = null;
        Refresh();
    }
    
    public void UndoCutOff()
    {
        if (spectrumHistory.Count == 0)
        {
            MessageUtil.ShowInfo("There is no undo to do.", "Information");
            return;
        }
        start = startHistory.Pop();
        end = endHistory.Pop();
        var spectra = spectrumHistory.Pop();
        Util.SetSpectrumVisibilityAccordingToCurrentVisibleSpectra(spectra, canvasPanel.VisibleSpectra);
        canvasPanel.Spectra = spectra;
        Refresh();
    }
    
    public override void HandleMouseDown(object sender, MouseEventArgs e)
    {
        if (e.Button == MouseButtons.Left)
        {
            if (start == null)
            {
                start = CoordSystem.ToValuePoint(e.Location.X, e.Location.Y);
            }
            else if (end == null)
            {
                end = CoordSystem.ToValuePoint(e.Location.X, e.Location.Y);
            }
        }
        else if (e.Button == MouseButtons.Right)
        {
            ShowContextMenu(e.Location);
        }
        Refresh();
    }
    
    public override void Draw(Graphics graphics)
    {
        if (start != null)
        {
            new Mark(CoordSystem, graphics, COLOR, start).Draw();
        }
        if (end != null)
        {
            new Mark(CoordSystem, graphics, COLOR, end).Draw();
        }
    }

    public void ResetPoints()
    {
        if (start == null && end == null)
        {
            MessageUtil.ShowInfo("There are no points to reset.", "Information");
            return;
        }
        start = null;
        end = null;
        Refresh();
    }

    private static List<Spectrum> GetCutOffSpectra(List<Spectrum> spectra, double start, double end)
    {
        var ret = new List<Spectrum>();
        foreach (var spectrum in spectra)
        {
            var newSpectrum = spectrum.IsVisible ? GetCutOffSpectrum(spectrum, start, end) : spectrum;
            ret.Add(newSpectrum);
        }
        return ret;
    }

    private static Spectrum GetCutOffSpectrum(Spectrum spectrum, double start, double end)
    {
        var points = spectrum.Points.Where(point => start <= point.X && point.X <= end).ToList();
        var ret = new Spectrum(points, spectrum.Name);
        return ret;
    }

    private void RemoveClosestPoint(Point pos)
    {
        var points = new List<ValuePoint>();
        if (start != null)
        {
            points.Add(start);
        }
        if (end != null)
        {
            points.Add(end);
        }
        var closestPoint = points.MinByOrDefault(x => Util.GetPixelDistance(CoordSystem.ToPixelPoint(x), pos));
        if (closestPoint != null)
        {
            if (closestPoint == start)
            {
                start = null;
            }
            else if (closestPoint == end)
            {
                end = null;
            }
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
        contextMenu.Items.Add("Remove Closest Point", null, (_, _) => RemoveClosestPoint(point));
        contextMenu.Show(canvasPanel, point);
    }
}