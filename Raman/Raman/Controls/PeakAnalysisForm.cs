using System.IO;
using Raman.Drawing;

namespace Raman.Controls;

public partial class PeakAnalysisForm : Form
{
    private readonly PeakAnalysisLayer peakAnalysisLayer;
    
    private const string FILE_DIALOG_FILTER = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
    
    public PeakAnalysisForm(PeakAnalysisLayer peakAnalysisLayer)
    {
        this.peakAnalysisLayer = peakAnalysisLayer;
        InitializeComponent();
        AdditionalInitialization();
    }

    private void AdditionalInitialization()
    {
        var isPeakAddedToAllCharts = true;
        cbAddToAllCharts.Checked = isPeakAddedToAllCharts;
        peakAnalysisLayer.IsPeakAddedToAllCharts = isPeakAddedToAllCharts;
    }

    private void btnReset_Click(object sender, EventArgs e)
    {
        try
        {
            peakAnalysisLayer.Reset();
        }
        catch (Exception ex)
        {
            MessageUtil.ShowAppError("Reset failed.", "Error", ex);
        }
    }

    private void btnClose_Click(object sender, EventArgs e)
    {
        try
        {
            Close();
        }
        catch (Exception ex)
        {
            MessageUtil.ShowAppError("Closing form failed.", "Error", ex);
        }
    }

    private void PeakAnalysisForm_FormClosing(object sender, FormClosingEventArgs e)
    {
        if (peakAnalysisLayer.Peaks.Any() && !peakAnalysisLayer.IsExported)
        {
            if (MessageUtil.ShowQuestion("Do you really want to exit Peak Analysis and loose selected peaks?",
                    "Confirmation") == DialogResult.No)
            {
                e.Cancel = true;
            }
        }
    }

    private void btnExportPeaks_Click(object sender, EventArgs e)
    {
        try
        {
            ExportPeaks();
        }
        catch (Exception ex)
        {
            MessageUtil.ShowAppError("Peaks export failed.", "Error", ex);
        }
    }

    private void ExportPeaks()
    {
        using (var saveFileDialog = new SaveFileDialog())
        {
            saveFileDialog.Title = "Export Peaks";
            saveFileDialog.Filter = FILE_DIALOG_FILTER; 
            saveFileDialog.FilterIndex = 1;
            if (AppSettings.PeakAnalysisSaveFileDirectory != null)
            {
                saveFileDialog.InitialDirectory = AppSettings.PeakAnalysisSaveFileDirectory;
            }
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                var filePaths = saveFileDialog.FileNames.ToList();
                if (filePaths.Count == 0)
                {
                    MessageUtil.ShowUserError("No file was selected.", "No file selected");
                    return;
                }
                try
                {
                    var filePath = filePaths.First();
                    AppSettings.PeakAnalysisSaveFileDirectory = Path.GetDirectoryName(filePath);
                    peakAnalysisLayer.ExportPeaks(filePath);
                    MessageUtil.ShowInfo("Peaks were exported successfully.", "Export finished");
                }
                catch (Exception ex)
                {
                    MessageUtil.ShowAppError("Exporting peaks failed.", "Export failed", ex);
                }
            }
        }
    }

    private void cbAddToAllCharts_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            peakAnalysisLayer.IsPeakAddedToAllCharts = ((CheckBox) sender).Checked;
        }
        catch (Exception ex)
        {
            MessageUtil.ShowAppError("Changing checkbox value failed.", "Error", ex);
        }
    }
}