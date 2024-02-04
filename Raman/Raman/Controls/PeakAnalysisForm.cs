using Raman.Drawing;

namespace Raman.Controls;

public partial class PeakAnalysisForm : Form
{
    private readonly PeakAnalysisLayer peakAnalysisLayer;
    
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
    }
}