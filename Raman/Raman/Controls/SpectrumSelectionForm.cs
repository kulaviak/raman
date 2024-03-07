using Raman.Drawing;

namespace Raman.Controls
{
    public partial class SpectrumSelectionForm : Form
    {
        public SpectrumSelectionForm(List<Chart> charts)
        {
            InitializeComponent();
            AdditionalInitialization(charts);
        }

        public HashSet<string> SelectedSpectrumNames 
        {
            get
            {
                var ret = new HashSet<string>();
                foreach (var item in cbSpectra.CheckedItems)
                {
                    ret.Add(item.ToString());
                }
                return ret;
            }
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
        }

        private void btnSelectAll_Click(object sender, EventArgs e)
        {
            SelectItems(true);
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }
    }
}
