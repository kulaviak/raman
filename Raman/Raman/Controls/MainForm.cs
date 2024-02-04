using System.IO;
using Raman.Controls;
using Raman.Drawing;
using Point = Raman.Core.Point;

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
        try
        {
            OpenSingleSpectrumFiles();
        }
        catch (Exception ex)
        {
            FormUtil.ShowErrorOnUserAction("Opening single spectrum files failed.", "Opening files failed", ex);
        }
    }

    private void OpenSingleSpectrumFiles()
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
        foreach (var filePath in filePaths)
        {
            var fileReader = new SingleSpectrumFileReader(filePath);
            var points = fileReader.TryReadFile();
            var name = Path.GetFileNameWithoutExtension(filePath);
            charts.Add(new Chart(points, name));
        }

        canvasPanel.Charts = charts;
        canvasPanel.Refresh();
    }

    private void LoadDemoSpectrum()
    {
        OpenSingleSpectraFilesInternal(new List<string> {"c:/github/kulaviak/raman/data/spectrum.txt"});
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
            FormUtil.ShowErrorOnUserAction("Zoom to window failed.", "Error", ex);
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
            canvasPanel.ZoomToOriginalSize();
        }
        catch (Exception ex)
        {
            FormUtil.ShowErrorOnUserAction("Zoom failed.", "Zoom failed", ex);
        }
    }

    private void miRefresh_Click(object sender, EventArgs e)
    {
        try
        {
            Refresh();
        }
        catch (Exception ex)
        {
            FormUtil.ShowErrorOnUserAction("Refresh failed.", "Refresh failed", ex);
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
            FormUtil.ShowErrorOnUserAction("Baseline correction failed.", "Error", ex);
        }
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
        try
        {
            HideSidePanel();
            canvasPanel.BaselineCorrectionLayer = null;
            Refresh();
        }
        catch (Exception ex)
        {
            FormUtil.ShowErrorOnUserAction("Closing baseline form failed.", "Error", ex);
        }
    }

    private void tsbRefresh_Click(object sender, EventArgs e)
    {
        try
        {
            Refresh();
        }
        catch (Exception ex)
        {
            FormUtil.ShowErrorOnUserAction("Refresh failed.", "Error", ex);
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
            FormUtil.ShowErrorOnUserAction("Opening single spectrum files failed.", "Opening files failed", ex);
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
            FormUtil.ShowErrorOnUserAction("Zoom failed.", "Error", ex);
        }
    }

    private void tsbZoomToOriginalSize_Click(object sender, EventArgs e)
    {
        try
        {
            canvasPanel.ZoomToOriginalSize();
        }
        catch (Exception ex)
        {
            FormUtil.ShowErrorOnUserAction("Zoom to original size failed.", "Error", ex);
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
            FormUtil.ShowErrorOnUserAction("Baseline correction failed.", "Error", ex);
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
            FormUtil.ShowErrorOnUserAction("Form resize failed.", "Error", ex);
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
            FormUtil.ShowErrorOnUserAction("Peak analysis failed.", "Error", ex);
        }
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
        try
        {
            PeakAnalysis();
        }
        catch (Exception ex)
        {
            FormUtil.ShowErrorOnUserAction("Peak analysis failed.", "Error", ex);
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
            FormUtil.ShowErrorOnUserAction("Opening multi spectrum files failed.", "Opening files failed", ex);
        }
    }

    private void OpenMultiSpectrumFiles()
    {
        using (var openFileDialog = new OpenFileDialog())
        {
            // don't specify initial directory - it will remember the last oneI
            // openFileDialog.InitialDirectory = "C:\\"; 
            openFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
            openFileDialog.FilterIndex = 1;
            openFileDialog.Multiselect = true;
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                var filePaths = openFileDialog.FileNames.ToList();
                if (filePaths.Count == 0)
                {
                    MessageBox.Show("No files were selected.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                OpenMultiSpectrumFilesInternal(filePaths);
            }
        }
    }

    private void OpenMultiSpectrumFilesInternal(List<string> filePaths)
    {
        var charts = new List<Chart>();
        foreach (var filePath in filePaths)
        {
            List<List<Point>> spectraPoints;
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
            foreach (var spectrumPoints in spectraPoints)
            {
                charts.Add(new Chart(spectrumPoints, name));
            }
        }
        canvasPanel.Charts = charts;
        canvasPanel.Refresh();
    }

    private void testToolStripMenuItem_Click(object sender, EventArgs e)
    {
        FormUtil.ShowErrorOnUserAction("Remember that ICO files can contain multiple image sizes for different display contexts " +
            "(e.g., small vs. large icons). If you have a specific requirement for different sizes, you may need to create an ICO " +
            "file with the necessary sizes from your PNG image. There are online tools and", "Error", new Exception("Demo"));
    }
}