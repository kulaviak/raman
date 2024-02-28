using Raman.Drawing;

namespace Raman.Controls;

public partial class CutOffForm : Form
{
    private readonly CutOffLayer cutOffLayer;
    
    public CutOffForm(CutOffLayer cutOffLayer)
    {
        this.cutOffLayer = cutOffLayer;
        InitializeComponent();
    }
    
    private void btnClose_Click(object sender, EventArgs e)
    {
        try
        {
            Close();
        }
        catch (Exception ex)
        {
            FormUtil.ShowAppError("Closing form failed.", "Error", ex);
        }
    }
    
    private void btnCutOff_Click(object sender, EventArgs e)
    {
        try
        {
           cutOffLayer.CutOff();
        }
        catch (Exception ex)
        {
            FormUtil.ShowAppError("Cut Off failed.", "Error", ex);
        }
    }
    
    private void btnUndoCutOff_Click(object sender, EventArgs e)
    {
        try
        {
            cutOffLayer.UndoCutOff();
        }
        catch (Exception ex)
        {
            FormUtil.ShowAppError("Undo Cut Off failed.", "Error", ex);
        }
    }

    private void btnResetPoints_Click(object sender, EventArgs e)
    {
        try
        {
            cutOffLayer.ResetPoints();
        }
        catch (Exception ex)
        {
            FormUtil.ShowAppError("Reset points failed.", "Error", ex);
        }
    }

    private void CutOffForm_FormClosing(object sender, FormClosingEventArgs e)
    {
        if (cutOffLayer.Start != null || cutOffLayer.End != null)
        {
            if (FormUtil.ShowQuestion("You selected some cut off points. Do you really want to exit Cut Off without cutting of spectra?",
                    "Confirmation") == DialogResult.No)
            {
                e.Cancel = true;
            }
        }
    }
}