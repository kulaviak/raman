using Raman.Drawing;

namespace Raman.Controls
{
    public partial class SpectrumSelectionForm : Form
    {
        private readonly CanvasPanel canvasPanel;
        
        public SpectrumSelectionForm(List<Chart> charts, CanvasPanel canvasPanel)
        {
            this.canvasPanel = canvasPanel;
            InitializeComponent();
            AdditionalInitialization(charts);
        }

        private void AdditionalInitialization(List<Chart> charts)
        {
            foreach (var chart in charts)
            {
                cbSpectra.Items.Add(chart.Name, chart.IsVisible);
            }
        }

        private void btnUnselectAll_Click(object sender, EventArgs e)
        {
            SelectItems(false);
        }

        private void SelectItems(bool isSelected)
        {
            for (var i = 0; i < cbSpectra.Items.Count; i++)
            {
                cbSpectra.SetItemChecked(i, isSelected);
            }
            RefreshCanvasPanel();
        }

        private void RefreshCanvasPanel()
        {
            foreach (var chart in canvasPanel.Charts)
            {
                chart.IsVisible = cbSpectra.CheckedItems.Contains(chart.Name);
            }
            canvasPanel.Refresh();
        }

        private void btnSelectAll_Click(object sender, EventArgs e)
        {
            SelectItems(true);
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        private void cbSpectra_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshCanvasPanel();
        }
    }
}
