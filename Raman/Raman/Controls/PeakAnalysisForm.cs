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
    }
    
    private void btnReset_Click(object sender, EventArgs e)
    {
        try
        {
            peakAnalysisLayer.Reset();
        }
        catch (Exception ex)
        {
            FormUtil.ShowErrorOnUserAction("Reset failed.", "Error", ex);
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
            FormUtil.ShowErrorOnUserAction("Closing form failed.", "Error", ex);
        }
    }

    private void PeakAnalysisForm_FormClosing(object sender, FormClosingEventArgs e)
    {
        // if (_peakAnalysisLayer.CorrectionPoints.Any() && !_peakAnalysisLayer.IsBaselineCorrected)
        // {
        //     if (FormUtil.ShowQuestion("Do you really want to exit Baseline Correction and loose selected correction points?",
        //             "Confirmation") == DialogResult.No)
        //     {
        //         e.Cancel = true;
        //     }
        // }
    }

    private void btnExportPeaks_Click(object sender, EventArgs e)
    {
        try
        {
            ExportPeaks();
        }
        catch (Exception ex)
        {
            FormUtil.ShowErrorOnUserAction("Peaks export failed.", "Error", ex);
        }
    }

    private void ExportPeaks()
    {
        using (var saveFileDialog = new SaveFileDialog())
        {
            saveFileDialog.Filter = FILE_DIALOG_FILTER; 
            saveFileDialog.FilterIndex = 1;
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                var filePaths = saveFileDialog.FileNames.ToList();
                if (filePaths.Count == 0)
                {
                    FormUtil.ShowUserError("No file was selected.", "No file selected");
                    return;
                }
                try
                {
                    var filePath = filePaths.First();
                    peakAnalysisLayer.ExportPeaks(filePath);
                    FormUtil.ShowInfo("Peak were exported successfully.", "Export finished");
                }
                catch (Exception ex)
                {
                    FormUtil.ShowAppError("Exporting peaks failed.", "Export failed", ex);
                }
            }
        }
    }
 }