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

    private void btnSetStartPoint_Click(object sender, EventArgs e)
    {
        cutOffLayer.Mode = CutOffMode.StartPoint;
    }

    private void btnSetEndPoint_Click(object sender, EventArgs e)
    {
        cutOffLayer.Mode = CutOffMode.EndPoint;
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
}