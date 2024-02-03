using System.IO;
using Raman.Controls;
using Raman.Drawing;

namespace Raman;

// window zoom made according to Chat GTP query How to implement zoom to area in graphics in windows forms
public partial class MainForm : Form
{
    private const int SIDE_PANEL_WIDTH = 175;
        
    public MainForm()
    {
        InitializeComponent();
        AdditionalInitialization();
    }

    private void AdditionalInitialization()
    {
        MinimumSize = new Size(800, 600);
        LoadDemoSpectrum();
        // LoadDemoSpectra();
        HideSidePanel();
        canvasPanel.StatusStripLayer = new StatusStripLayer(canvasPanel.CoordSystem, statusStrip);
    }

    private void HideSidePanel()
    {
        splitContainer.Panel2Collapsed = true;
    }

    private void UpdateSplitter()
    {
        splitContainer.SplitterDistance = Width - SIDE_PANEL_WIDTH - 20;
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
        OpenSingleSpektrumFiles();
    }

    private void OpenSingleSpektrumFiles()
    {
        using (var openFileDialog = new OpenFileDialog())
        {
            // don't specify initial directory - it will remember the last one
            // openFileDialog.InitialDirectory = "C:\\"; 
            openFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*"; 
            openFileDialog.FilterIndex = 1;
            openFileDialog.Multiselect = true;
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                var filePaths = openFileDialog.FileNames.ToList();
                if (filePaths.Count == 0)
                {
                    MessageBox.Show("No files were selected.", "Error", MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    return;
                }
                OpenSingleSpectraFilesInternal(filePaths);
            }
        }
    }

    private void OpenSingleSpectraFilesInternal(List<string> filePaths)
    {
        var charts = new List<Chart>();
        var ignoredLines = new List<string>();
        foreach (var filePath in filePaths)
        {
            var fileReader = new SingleSpectrumFileReader(filePath);
            var points = fileReader.TryReadFile();
            var name = Path.GetFileNameWithoutExtension(filePath);
            charts.Add(new Chart(points, name));
            if (points.Count < 2)
            {
                MessageBox.Show($"File {filePath} has less 2 points. File is ignored.", "Error", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                continue;
            }
            if (fileReader.IgnoredLines.Count != 0)
            {
                ignoredLines.AddRange(fileReader.IgnoredLines);
            }
        }

        if (ignoredLines.Count != 0)
        {
            var maxCount = 10;
            if (ignoredLines.Count > maxCount) ignoredLines = ignoredLines.Take(maxCount).ToList();
            var str = string.Join("\n", ignoredLines);
            MessageBox.Show($"Following lines were ignored (Showing max {maxCount} lines): {str}", "Error", MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        }
        canvasPanel.Charts = charts;
        canvasPanel.Refresh();
    }
        
    private void LoadDemoSpectrum()
    {
        OpenSingleSpectraFilesInternal(new List<string>{"c:/github/kulaviak/raman/data/spectrum.txt"});
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
        ZoomToWindow();
    }

    private void ZoomToWindow()
    {
        canvasPanel.IsZooming = true;
    }

    private void miZoomToOriginalSize_Click(object sender, EventArgs e)
    {
        canvasPanel.ZoomToOriginalSize();
    }

    private void miRefresh_Click(object sender, EventArgs e)
    {
        Refresh();
    }

    private void miBaselineCorrection_Click(object sender, EventArgs e)
    {
        BaselineCorrection();
    }

    private void BaselineCorrection()
    {
        canvasPanel.BaselineCorrectionLayer = new BaselineCorrectionLayer(canvasPanel.CoordSystem, canvasPanel);
        var form = new BaselineCorrectionForm(canvasPanel.BaselineCorrectionLayer);
        form.Closed += BaselineForm_Closed;
        ShowSidePanel(form);
    }

    private void BaselineForm_Closed(object sender, EventArgs e)
    {
        HideSidePanel();
        canvasPanel.BaselineCorrectionLayer = null;
        Refresh();
    }
    
    private void tsbRefresh_Click(object sender, EventArgs e)
    {
        Refresh();
    }

    private void tsbOpenFiles_Click(object sender, EventArgs e)
    {
        OpenSingleSpektrumFiles();
    }

    private void tsbZoomToWindow_Click(object sender, EventArgs e)
    {
        ZoomToWindow();
    }

    private void tsbZoomToOriginalSize_Click(object sender, EventArgs e)
    {
        canvasPanel.ZoomToOriginalSize();
    }

    private void tsbBaselineCorrection_Click(object sender, EventArgs e)
    {
        BaselineCorrection();
    }

    private void MainForm_Resize(object sender, EventArgs e)
    {
        UpdateSplitter();
    }

    private void miPeakAnalysis_Click(object sender, EventArgs e)
    {
        PeakAnalysis();
    }

    private void PeakAnalysis()
    {
        canvasPanel.PeakAnalysisLayer = new PeakAnalysisLayer(canvasPanel.CoordSystem, canvasPanel);
        // var form = new PeForm(canvasPanel.BaselineCorrectionLayer);
        // form.Closed += BaselineForm_Closed;
        // ShowSidePanel(form);
    }

    private void tsbPeakAnalysis_Click(object sender, EventArgs e)
    {
        PeakAnalysis();
    }

    private void miOpenMultiSpectrumFiles_Click(object sender, EventArgs e)
    {
        OpenMultiSpectrumFiles();
    }

    private void OpenMultiSpectrumFiles()
    {
        using (var openFileDialog = new OpenFileDialog())
        {
            // don't specify initial directory - it will remember the last one
            // openFileDialog.InitialDirectory = "C:\\"; 
            openFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*"; 
            openFileDialog.FilterIndex = 1;
            openFileDialog.Multiselect = true;
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                var filePaths = openFileDialog.FileNames.ToList();
                if (filePaths.Count == 0)
                {
                    MessageBox.Show("No files were selected.", "Error", MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    return;
                }
                OpenMultiSpectrumFilesInternal(filePaths);
            }
        }
    }

    private void OpenMultiSpectrumFilesInternal(List<string> filePaths)
    {
        var charts = new List<Chart>();
        var ignoredLines = new List<string>();
        foreach (var filePath in filePaths)
        {
            var fileReader = new MultiSpectrumFileReader(filePath);
            var multipleSpectraPoints = fileReader.TryReadFile();
            var name = Path.GetFileNameWithoutExtension(filePath);
            foreach (var spectrumPoints in multipleSpectraPoints)
            {
                charts.Add(new Chart(spectrumPoints, name));
                if (multipleSpectraPoints.Count < 2)
                {
                    MessageBox.Show($"File {filePath} has less 2 points. File is ignored.", "Error", MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    continue;
                }
                if (fileReader.IgnoredLines.Count != 0)
                {
                    ignoredLines.AddRange(fileReader.IgnoredLines);
                }
            }
        }

        if (ignoredLines.Count != 0)
        {
            var maxCount = 10;
            if (ignoredLines.Count > maxCount) ignoredLines = ignoredLines.Take(maxCount).ToList();
            var str = string.Join("\n", ignoredLines);
            MessageBox.Show($"Following lines were ignored (Showing max {maxCount} lines): {str}", "Error", MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        }
        canvasPanel.Charts = charts;
        canvasPanel.Refresh();
    }
}