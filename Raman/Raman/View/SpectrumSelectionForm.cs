using Raman.Drawing;

namespace Raman.View
{
    public partial class SpectrumSelectionForm : Form
    {
        private readonly CanvasPanel canvasPanel;
        
        public SpectrumSelectionForm(List<Spectrum> spectra, CanvasPanel canvasPanel)
        {
            this.canvasPanel = canvasPanel;
            InitializeComponent();
            AdditionalInitialization(spectra);
        }

        private void AdditionalInitialization(List<Spectrum> spectra)
        {
            foreach (var spectrum in spectra)
            {
                cbSpectra.Items.Add(spectrum.Name, spectrum.IsVisible);
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
            foreach (var spectrum in canvasPanel.Spectra)
            {
                spectrum.IsVisible = cbSpectra.CheckedItems.Contains(spectrum.Name);
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
