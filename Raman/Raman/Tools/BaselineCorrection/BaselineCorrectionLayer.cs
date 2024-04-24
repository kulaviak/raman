using System.IO;
using Raman.Controls;
using Raman.Drawing;
using Raman.File;

namespace Raman.Tools.BaselineCorrection;

public class BaselineCorrectionLayer : LayerBase
{
    private readonly CanvasPanel canvasPanel;
        
    private static Color COLOR = Color.Red;

    public List<ValuePoint> CorrectionPoints { get; set; } = new List<ValuePoint>();

    public bool AreBaselineEndsExtended
    {
        get => areBaselineEndsExtended;
        set
        {
            areBaselineEndsExtended = value;
            Refresh();
        }
    }
    
    public bool AreCorrectionPointsAdjusted { get; set; }

    private Stack<List<Spectrum>> spectrumHistory = new Stack<List<Spectrum>>();
    
    private Stack<List<ValuePoint>> correctionPointHistory = new Stack<List<ValuePoint>>();
    
    private bool areBaselineEndsExtended;

    public BaselineCorrectionLayer(CanvasCoordSystem coordSystem, CanvasPanel canvasPanel, bool areBaselineEndsExtended, bool areCorrectionPointsAdjusted) : base(coordSystem)
    {
        this.canvasPanel = canvasPanel;
        AreBaselineEndsExtended = areBaselineEndsExtended;
        AreCorrectionPointsAdjusted = areCorrectionPointsAdjusted;
    }
        
    public override void HandleMouseDown(object sender, MouseEventArgs e)
    {
        if (e.Button == MouseButtons.Left)
        {
            var point = CalculateCorrectionPoint(e.Location, canvasPanel.VisibleSpectra, AreCorrectionPointsAdjusted);
            CorrectionPoints.Add(point);
            Refresh();
        }
        else if (e.Button == MouseButtons.Middle && Util.IsCtrlKeyPressed())
        {
            RemoveClosestPoint(e.Location);
            Refresh();
        }
        else if (e.Button == MouseButtons.Right)
        {
            ShowContextMenu(e.Location);
        }
    }

    private ValuePoint CalculateCorrectionPoint(Point pos, List<Spectrum> spectra, bool areCorrectionPointsAdjusted)
    {
        if (areCorrectionPointsAdjusted)
        {
            var closestSpectrum = new ClosestSpectrumCalculator().GetClosestSpectrum(spectra, pos, CoordSystem); 
            var closestPoints = GetClosestPoints(closestSpectrum.Points, pos);
            var averageY = closestPoints.Average(point => point.Y);
            var ret = new ValuePoint(CoordSystem.ToValueX(pos.X), averageY);
            return ret;
        }
        else
        {
            return CoordSystem.ToValuePoint(pos.X, pos.Y);
        }
    }

    private List<ValuePoint> GetClosestPoints(List<ValuePoint> points, Point pos)
    {
        var closestPoint = GetClosestPointInXDirection(points, CoordSystem.ToValuePoint(pos));
        var ret = GetNeighbourhoodPoints(closestPoint, points);
        return ret;
    }

    private static ValuePoint GetClosestPointInXDirection(List<ValuePoint> points, ValuePoint point)
    {
        return points.MinByOrDefault(x => Math.Abs(x.X - point.X));
    }

    public void Reset()
    {
        if (!CorrectionPoints.Any())
        {
            MessageUtil.ShowInfo("There are no points to reset.", "Information");
            return;
        }
        CorrectionPoints.Clear();
        Refresh();
    }
        
    public override void Draw(Graphics graphics)
    {
        DrawBaselines(graphics);
        DrawMarks(graphics);
    }
    
    public void ImportPoints(List<ValuePoint> points)
    {
        CorrectionPoints = points;
        Refresh();
    }

    public void ExportPoints(string filePath)
    {
        if (!CorrectionPoints.Any())
        {
            MessageUtil.ShowInfo("There are no baseline points to export.", "Information");
            return;
        }
        new OnePointPerLineFileWriter().WritePoints(CorrectionPoints, filePath);
    }

    public void CorrectBaseline()
    {
        if (!CorrectionPoints.Any())
        {
            MessageUtil.ShowInfo("There are no baseline points defined.", "Information");
            return;
        }
        spectrumHistory.Push(canvasPanel.Spectra);
        correctionPointHistory.Push(CorrectionPoints);
        canvasPanel.Spectra = GetCorrectedSpectra(canvasPanel.Spectra);
        CorrectionPoints = new List<ValuePoint>();
        canvasPanel.ZoomToSeeAllSpectra();
    }

    private List<Spectrum> GetCorrectedSpectra(List<Spectrum> spectra)
    {
        var ret = new List<Spectrum>();
        foreach (var spectrum in spectra)
        {
            var newSpectrum = spectrum.IsVisible ? CorrectBaseline(spectrum) : spectrum;
            ret.Add(newSpectrum);
        }
        return ret;
    }

    public void ExportCorrectedSpectra()
    {
        var exportedSpectra = canvasPanel.Spectra.Where(x => x.IsBaselineCorrected).ToList();
        if (!exportedSpectra.Any())
        {
            MessageUtil.ShowUserError("There are no corrected spectra.", "No spectra to export");
            return;
        }
        using (var folderBrowserDialog = new FolderBrowserDialog())
        {
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                var selectedPath = folderBrowserDialog.SelectedPath;
                foreach (var spectrum in exportedSpectra)
                {
                    ExportCorrectedSpectra(spectrum, selectedPath);                                                
                }
                MessageUtil.ShowInfo("Export finished successfully.", "Export finished");
            }
        }
    }
    
    private void DrawBaselines(Graphics graphics)
    {
        if (canvasPanel.VisibleSpectra.Any() && CorrectionPoints.Count >= 4)
        {
            try
            {
                var start = GetBaselineStart();
                var end = GetBaselineEnd();
                var xPositions = GetXPositionsForBaselineDrawing(start, end);
                var baselinePoints = new SplineBaselineCalculator().GetBaseline(xPositions, CorrectionPoints);
                new CanvasDrawer(canvasPanel.CoordSystem, graphics).DrawLines(baselinePoints, Pens.Green);
            }
            catch (Exception e)
            {
                MessageUtil.ShowAppError("Drawing baseline failed.", "Error", e);
            }
        }
    }

    private double GetBaselineEnd()
    {
        if (AreBaselineEndsExtended)
        {
            var spectrumPointsMax = canvasPanel.Spectra.SelectMany(x => x.Points).Max(point => point.X);
            var correctionPointsMax = CorrectionPoints.Max(point => point.X);
            return Math.Max(spectrumPointsMax, correctionPointsMax);
        }
        else
        {
            return CorrectionPoints.Max(point => point.X);
        }
    }

    private double GetBaselineStart()
    {
        if (AreBaselineEndsExtended)
        {
            var spectrumPointsMin = canvasPanel.Spectra.SelectMany(x => x.Points).Min(point => point.X);
            var correctionPointsMin = CorrectionPoints.Min(point => point.X);
            return Math.Min(spectrumPointsMin, correctionPointsMin);
        }
        else
        {
            return CorrectionPoints.Min(point => point.X);
        }
    }

    private List<double> GetXPositionsForBaselineDrawing(double start, double end)
    {
        var diff = (end - start) / 1000;
        var ret = new List<double>();
        for (var position = start; position  <= end; position += diff)
        {
            ret.Add(position);    
        }
        return ret;
    }
    
    private List<double> GetXPositionsForBaseline(Spectrum spectrum, List<ValuePoint> spectrumCorrectionPoints)
    {
        var baselineStart = GetBaselineStart();
        var baselineEnd = GetBaselineEnd();
        var correctionPointsStart = spectrumCorrectionPoints.Select(point => point.X).Min();
        var correctionPointsEnd = spectrumCorrectionPoints.Select(point => point.X).Max();
        // limit the baseline range to the range of correction points, that determinate the baseline. Else the baseline can escape to
        // Infinity and calculation fails
        var start = Math.Max(baselineStart, correctionPointsStart);
        var end = Math.Min(baselineEnd, correctionPointsEnd);
        return spectrum.Points.Where(point => start <= point.X && point.X <= end).Select(point => point.X).ToList();
    }

    private void RemoveClosestPoint(Point location)
    {
        var point = GetPointToRemove(location);
        if (point != null)
        {
            CorrectionPoints = CorrectionPoints.Where(x => x != point).ToList();
        }
        Refresh();
    }

    private void Refresh()
    {
        canvasPanel.Refresh();
    }

    private void ShowContextMenu(Point location)
    {
        var contextMenu = new ContextMenuStrip();
        contextMenu.Items.Add("Remove Closest Point", null, (_, _) => RemoveClosestPoint(location));
        contextMenu.Show(canvasPanel, location);
    }
        
    private ValuePoint GetPointToRemove(Point pos)
    {
        var closestPoint = CorrectionPoints.MinByOrDefault(x => Util.GetPixelDistance(CoordSystem.ToPixelPoint(x), pos));
        if (closestPoint != null)
        {
            return closestPoint;
        }
        return null;
    }
        
    private void DrawMarks(Graphics graphics)
    {
        foreach (var point in CorrectionPoints)
        {
            new Mark(CoordSystem, graphics, COLOR, point).Draw();
        }
    }
    
    private Spectrum CorrectBaseline(Spectrum spectrum)
    {
        var spectrumCorrectionPoints = GetSpectrumCorrectionPoints(spectrum, CorrectionPoints, AreCorrectionPointsAdjusted);
        var xPositions = GetXPositionsForBaseline(spectrum, spectrumCorrectionPoints);
        var baselinePoints = new SplineBaselineCalculator().GetBaseline(xPositions, spectrumCorrectionPoints);
        var newSpectrumPoints = new BaselineCorrector().CorrectSpectrumByBaseline(spectrum.Points, baselinePoints);
        var ret = new Spectrum(newSpectrumPoints, spectrum.Name);
        ret.IsBaselineCorrected = true;
        return ret;
    }
    
    private static List<ValuePoint> GetSpectrumCorrectionPoints(Spectrum spectrum, List<ValuePoint> correctionPoints, bool areCorrectionPointsAdjusted)
    {
        var ret = new List<ValuePoint>();
        foreach (var correctionPoint in correctionPoints)
        {
            var spectrumPointClosestInXDirection = GetClosestPointInXDirection(spectrum.Points, correctionPoint);
            double y;
            if (areCorrectionPointsAdjusted)
            {
                var points = GetNeighbourhoodPoints(spectrumPointClosestInXDirection, spectrum.Points);
                y = points.Average(point => point.Y);
            }
            else
            {
                y = spectrumPointClosestInXDirection.Y;
            }
            var point = new ValuePoint(spectrumPointClosestInXDirection.X, y);
            ret.Add(point);
        }   
        return ret;
    }

    public void UndoBaselineCorrection()
    {
        if (spectrumHistory.Count == 0)
        {
            MessageUtil.ShowInfo("There is no undo to do.", "Information");
            return;
        }

        var spectra = spectrumHistory.Pop();
        Util.SetSpectrumVisibilityAccordingToCurrentVisibleSpectra(spectra, canvasPanel.VisibleSpectra);
        canvasPanel.Spectra = spectra;
        CorrectionPoints = correctionPointHistory.Pop();
        canvasPanel.ZoomToSeeAllSpectra();
    }
    
    private void ExportCorrectedSpectra(Spectrum spectrum, string folderPath)
    {
        var filePath = Path.Combine(folderPath, spectrum.Name + "_bc.txt");
        new OnePointPerLineFileWriter().WritePoints(spectrum.Points, filePath);
    }
 
    /// <summary>
    /// Gets neighbourhood points including the point itself.
    /// </summary>
    private static List<ValuePoint> GetNeighbourhoodPoints(ValuePoint point, List<ValuePoint> points)
    {
        var ret = new List<ValuePoint>();
        var index = points.IndexOf(point);
        if (index - 1 >= 0)
        {
            ret.Add(points[index - 1]);
        }
        if (index - 2 >= 0)
        {
            ret.Add(points[index - 2]);
        }
        if (index + 1 < points.Count)
        {
            ret.Add(points[index + 1]);
        }
        if (index + 2 >= points.Count)
        {
            ret.Add(points[index + 2]);
        }
        return ret;
    }
}