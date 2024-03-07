using System.Diagnostics;
using System.IO;
using Raman.Controls;
using Raman.Drawing;

namespace Raman;

// window zoom made according to Chat GTP query How to implement zoom to area in graphics in windows forms
public partial class MainForm : Form
{
    private const int SIDE_PANEL_WIDTH = 175;

    private List<Chart> Charts
    {
        get { return canvasPanel.Charts; }
        set
        {
            canvasPanel.Charts = value;
            canvasPanel.SetCoordSystemToShowAllCharts();
            canvasPanel.Refresh();
            EnableOrDisableItems();
        }
    }

    private void EnableOrDisableItems()
    {
        var areAnyCharts = canvasPanel.Charts.Count != 0;
        
        miZoomWindow.Enabled = areAnyCharts;
        tsbZoomToWindow.Enabled = areAnyCharts;
        
        miZoomToOriginalSize.Enabled = areAnyCharts;
        tsbZoomToOriginalSize.Enabled = areAnyCharts;
        
        miBaselineCorrection.Enabled = areAnyCharts;
        tsbBaselineCorrection.Enabled = areAnyCharts;

        miPeakAnalysis.Enabled = areAnyCharts;
        tsbPeakAnalysis.Enabled = areAnyCharts;

        miCutOff.Enabled = areAnyCharts;
        tsbCutOff.Enabled = areAnyCharts;

        miSelectSpectra.Enabled = areAnyCharts;
        tsbSelectSpectra.Enabled = areAnyCharts;
    }

    public MainForm()
    {
        InitializeComponent();
        AdditionalInitialization();
    }

    private void AdditionalInitialization()
    {
        MinimumSize = new Size(800, 600);
        canvasPanel.CoordSystem = CoordSystemCalculator.GetDefaultCoordSystem(canvasPanel.Width, canvasPanel.Height);
        HideSidePanel();
        SetFormToDefaultState();
        KeyPreview = true;
        KeyDown += OnKeyDown;
        Shown += OnShown;
        EnableOrDisableItems();
    }
    
    private void OnShown(object sender, EventArgs e)
    {
        // LoadDemoSpectrum();
        LoadDemoSpectra();
    }

    private void HideSidePanel()
    {
        splitContainer.Panel2Collapsed = true;
    }

    private void UpdateSplitter()
    {
        if (WindowState != FormWindowState.Minimized)
        {
            var splitterDistance = Width - SIDE_PANEL_WIDTH - 20;
            splitContainer.SplitterDistance = splitterDistance;
        }
    }

    private void ShowSidePanel(Form form)
    {
        form.TopLevel = false;
        splitContainer.Panel2Collapsed = false;
        form.Dock = DockStyle.Fill;
        splitContainer.Panel2.Controls.Clear();
        splitContainer.Panel2.Controls.Add(form);
        UpdateSplitter();
        form.Show();
    }

    private void miExit_Click(object sender, EventArgs e)
    {
        Close();
    }

    private void RamanForm_FormClosing(object sender, FormClosingEventArgs e)
    {
        // if (MessageBox.Show("Do you really want to exit?", "Exit Confirmation", MessageBoxButtons.YesNo,
        //         MessageBoxIcon.Question) == DialogResult.No)
        // {
        //     e.Cancel = true;
        // }
    }

    private void miOpenSingleSpectrumFiles_Click(object sender, EventArgs e)
    {
        try
        {
            OpenSingleSpectrumFiles();
        }
        catch (Exception ex)
        {
            FormUtil.ShowAppError("Opening single spectrum files failed.", "Opening files failed", ex);
        }
    }

    private void OpenSingleSpectrumFiles()
    {
        using (var openFileDialog = new OpenFileDialog())
        {
            openFileDialog.Title = "Open Single Spectrum Files";
            openFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
            openFileDialog.FilterIndex = 1;
            openFileDialog.Multiselect = true;
            if (AppSettings.SingleSpectrumOpenFileDirectory != null)
            {
                openFileDialog.InitialDirectory = AppSettings.SingleSpectrumOpenFileDirectory;
            }
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                var filePaths = openFileDialog.FileNames.ToList();
                if (filePaths.Count == 0)
                {
                    MessageBox.Show("No files were selected.", "Error", MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    return;
                }
                AppSettings.SingleSpectrumOpenFileDirectory = Path.GetDirectoryName(filePaths[0]);
                OpenSingleSpectraFilesInternal(filePaths);
            }
        }
    }

    private void OpenSingleSpectraFilesInternal(List<string> filePaths)
    {
        SetFormToDefaultState();
        var charts = new List<Chart>();
        foreach (var filePath in filePaths)
        {
            var fileReader = new SingleSpectrumFileReader(filePath);
            var points = fileReader.TryReadFile();
            var name = Path.GetFileNameWithoutExtension(filePath);
            charts.Add(new Chart(points, name));
        }
        Charts = charts;
    }

    private void LoadDemoSpectrum()
    {
        OpenSingleSpectraFilesInternal(new List<string> {"c:/github/kulaviak/raman/data/spektra/spectrum.txt"});
    }

    private void LoadDemoSpectra()
    {
        var filePaths = new List<string>
        {
            "c:/github/kulaviak/raman/data/spektra/UHK-0-1-5/UHK-0-1-5_Y0_X0.txt",
            "c:/github/kulaviak/raman/data/spektra/UHK-0-1-5/UHK-0-1-5_Y0_X1.txt",
            "c:/github/kulaviak/raman/data/spektra/UHK-0-1-5/UHK-0-1-5_Y0_X3.txt",
            "c:/github/kulaviak/raman/data/spektra/UHK-0-1-5/UHK-0-1-5_Y0_X4.txt",
            "c:/github/kulaviak/raman/data/spektra/UHK-0-1-5/UHK-0-1-5_Y1_X0.txt",
            "c:/github/kulaviak/raman/data/spektra/UHK-0-1-5/UHK-0-1-5_Y1_X1.txt",
            "c:/github/kulaviak/raman/data/spektra/UHK-0-1-5/UHK-0-1-5_Y1_X2.txt",
            "c:/github/kulaviak/raman/data/spektra/UHK-0-1-5/UHK-0-1-5_Y1_X3.txt",
            "c:/github/kulaviak/raman/data/spektra/UHK-0-1-5/UHK-0-1-5_Y1_X4.txt"
        };
        OpenSingleSpectraFilesInternal(filePaths);
    }

    private void miZoomWindow_Click(object sender, EventArgs e)
    {
        try
        {
            ZoomToWindow();
        }
        catch (Exception ex)
        {
            FormUtil.ShowAppError("Zoom to window failed.", "Error", ex);
        }
    }

    private void ZoomToWindow()
    {
        canvasPanel.SetZoomToWindowMode();
    }

    private void miZoomToOriginalSize_Click(object sender, EventArgs e)
    {
        try
        {
            canvasPanel.ZoomToSeeAllCharts();
        }
        catch (Exception ex)
        {
            FormUtil.ShowAppError("Zoom failed.", "Zoom failed", ex);
        }
    }
    
    private void miBaselineCorrection_Click(object sender, EventArgs e)
    {
        try
        {
            BaselineCorrection();
        }
        catch (Exception ex)
        {
            FormUtil.ShowAppError("Baseline correction failed.", "Error", ex);
        }
    }

    private void BaselineCorrection()
    {
        SetFormToDefaultState();
        var baselineCorrectionLayer = new BaselineCorrectionLayer(canvasPanel.CoordSystem, canvasPanel, AppSettings.AreBaselineEndsExtended, AppSettings.AreCorrectionPointsAdjusted);
        canvasPanel.BaselineCorrectionLayer = baselineCorrectionLayer;
        var form = new BaselineCorrectionForm(baselineCorrectionLayer,AppSettings.AreBaselineEndsExtended, AppSettings.AreCorrectionPointsAdjusted);
        form.Closed += BaselineForm_Closed;
        ShowSidePanel(form);
    }
    
    private void BaselineForm_Closed(object sender, EventArgs e)
    {
        try
        {
            HideSidePanel();
            canvasPanel.BaselineCorrectionLayer = null;
            Refresh();
        }
        catch (Exception ex)
        {
            FormUtil.ShowAppError("Closing Baseline Correction form failed.", "Error", ex);
        }
    }
    
    private void tsbOpenFiles_Click(object sender, EventArgs e)
    {
        try
        {
            OpenSingleSpectrumFiles();
        }
        catch (Exception ex)
        {
            FormUtil.ShowAppError("Opening single spectrum files failed.", "Opening files failed", ex);
        }
    }

    private void tsbZoomToWindow_Click(object sender, EventArgs e)
    {
        try
        {
            ZoomToWindow();
        }
        catch (Exception ex)
        {
            FormUtil.ShowAppError("Zoom failed.", "Error", ex);
        }
    }

    private void tsbZoomToOriginalSize_Click(object sender, EventArgs e)
    {
        try
        {
            canvasPanel.ZoomToSeeAllCharts();
        }
        catch (Exception ex)
        {
            FormUtil.ShowAppError("Zoom to original size failed.", "Error", ex);
        }
    }

    private void tsbBaselineCorrection_Click(object sender, EventArgs e)
    {
        try
        {
            BaselineCorrection();
        }
        catch (Exception ex)
        {
            FormUtil.ShowAppError("Baseline correction failed.", "Error", ex);
        }
    }

    private void MainForm_Resize(object sender, EventArgs e)
    {
        try
        {
            UpdateSplitter();
        }
        catch (Exception ex)
        {
            FormUtil.ShowAppError("Form resize failed.", "Error", ex);
        }
    }

    private void miPeakAnalysis_Click(object sender, EventArgs e)
    {
        try
        {
            PeakAnalysis();
        }
        catch (Exception ex)
        {
            FormUtil.ShowAppError("Peak analysis failed.", "Error", ex);
        }
    }

    private void PeakAnalysis()
    {
        SetFormToDefaultState();
        var peakAnalysisLayer = new PeakAnalysisLayer(canvasPanel.CoordSystem, canvasPanel);
        canvasPanel.PeakAnalysisLayer = peakAnalysisLayer;
        var form = new PeakAnalysisForm(peakAnalysisLayer);
        form.Closed += PeakAnalysisForm_Closed;
        ShowSidePanel(form);
    }

    private void PeakAnalysisForm_Closed(object sender, EventArgs e)
    {
        try
        {
            HideSidePanel();
            canvasPanel.PeakAnalysisLayer = null;
            Refresh();
        }
        catch (Exception ex)
        {
            FormUtil.ShowAppError("Closing Peak Analysis form failed.", "Error", ex);
        }
    }

    private void tsbPeakAnalysis_Click(object sender, EventArgs e)
    {
        try
        {
            PeakAnalysis();
        }
        catch (Exception ex)
        {
            FormUtil.ShowAppError("Peak analysis failed.", "Error", ex);
        }
    }

    private void miOpenMultiSpectrumFiles_Click(object sender, EventArgs e)
    {
        try
        {
            OpenMultiSpectrumFiles();
        }
        catch (Exception ex)
        {
            FormUtil.ShowAppError("Opening multi spectrum files failed.", "Opening files failed", ex);
        }
    }

    private void OpenMultiSpectrumFiles()
    {
        using (var openFileDialog = new OpenFileDialog())
        {
            openFileDialog.Title = "Open Multiple Spectrum Files";
            openFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
            openFileDialog.FilterIndex = 1;
            openFileDialog.Multiselect = true;
            if (AppSettings.MultipleSpectrumOpenFileDirectory != null)
            {
                openFileDialog.InitialDirectory = AppSettings.MultipleSpectrumOpenFileDirectory;
            }

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                var filePaths = openFileDialog.FileNames.ToList();
                if (filePaths.Count == 0)
                {
                    MessageBox.Show("No files were selected.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                AppSettings.MultipleSpectrumOpenFileDirectory = Path.GetDirectoryName(filePaths[0]);
                OpenMultiSpectrumFilesInternal(filePaths);
            }
        }
    }
    
    private void SetFormToDefaultState()
    {
        HideSidePanel();
        canvasPanel.SetLayersToDefaultState(statusStrip);
    }

    private void OpenMultiSpectrumFilesInternal(List<string> filePaths)
    {
        SetFormToDefaultState();
        var charts = new List<Chart>();
        foreach (var filePath in filePaths)
        {
            List<List<ValuePoint>> spectraPoints;
            try
            {
                var fileReader = new MultiSpectrumFileReader(filePath);
                spectraPoints = fileReader.TryReadFile();
            }
            catch (Exception e)
            {
                throw new AppException($"Opening file {filePath} failed.", e);
            }
            var name = Path.GetFileNameWithoutExtension(filePath);
            for (var i = 0; i < spectraPoints.Count; i++)
            {
                var spectrumPoints = spectraPoints[i];
                var chart = new Chart(spectrumPoints, name + $"_{i+1}");
                charts.Add(chart);
            }
        }
        Charts = charts;
    }
    
    private void OnKeyDown(object sender, KeyEventArgs e)
    {
        canvasPanel.HandleKeyPress(sender, e);
    }

    private void miHelp_Click(object sender, EventArgs e)
    {
        try
        {
            var path = Path.Combine(Environment.CurrentDirectory, "doc/Raman.pdf");
            // Use the default PDF viewer on the system to open the file
            Process.Start(path);
        }
        catch (Exception ex)
        {
            FormUtil.ShowAppError($"Error opening PDF: {ex.Message}", "Error", ex);
        }    
    }

    private void miCutOff_Click(object sender, EventArgs e)
    {
        try
        {
            CutOff();
        }
        catch (Exception ex)
        {
            FormUtil.ShowAppError("Cut Off failed.", "Error", ex);
        }
    }

    private void CutOff()
    {
        SetFormToDefaultState();
        var cutOffLayer = new CutOffLayer(canvasPanel.CoordSystem, canvasPanel);
        canvasPanel.CutOffLayer = cutOffLayer;
        var form = new CutOffForm(cutOffLayer);
        form.Closed += CutOffForm_Closed;
        ShowSidePanel(form);
    }
    
    private void CutOffForm_Closed(object sender, EventArgs e)
    {
        try
        {
            HideSidePanel();
            canvasPanel.CutOffLayer = null;
            Refresh();
        }
        catch (Exception ex)
        {
            FormUtil.ShowAppError("Closing Cut Off form failed.", "Error", ex);
        }
    }

    private void tsbCutOff_Click(object sender, EventArgs e)
    {
        try
        {
            CutOff();
        }
        catch (Exception ex)
        {
            FormUtil.ShowAppError("Cut Off failed.", "Error", ex);
        }
    }

    private void tsbSpectrumSelection_Click(object sender, EventArgs e)
    {
        try
        {
            ShowSpectrumSelectionForm();
        }
        catch (Exception ex)
        {
            FormUtil.ShowAppError("Selecting spectra failed.", "Error", ex);
        }
    }

    private void ShowSpectrumSelectionForm()
    {
        var orderedCharts = canvasPanel.Charts.OrderByDescending(chart => GetAverageY(chart)).ToList();
        var form = new SpectrumSelectionForm(orderedCharts);
        if (form.ShowDialog() == DialogResult.OK)
        {
            foreach (var chart in orderedCharts)
            {
                chart.IsVisible = form.SelectedSpectrumNames.Contains(chart.Name);
            }
        }
        canvasPanel.Refresh();
    }

    private decimal GetAverageY(Chart chart)
    {
        var ret = chart.Points.Average(point => point.Y);
        return ret;
    }

    private void miSelectSpectra_Click(object sender, EventArgs e)
    {
        try
        {
            ShowSpectrumSelectionForm();
        }
        catch (Exception ex)
        {
            FormUtil.ShowAppError("Selecting spectra failed.", "Error", ex);
        }
    }
}