using System.Windows.Forms;
using Raman.Core;

namespace Raman.Controls
{
    public class AppStatusStrip : StatusStrip
    {
        private ToolStripStatusLabel _lblPosition;

        public AppStatusStrip()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            _lblPosition = new ToolStripStatusLabel();
            Items.AddRange(new ToolStripItem[] {_lblPosition});   
        }

        public void ShowPosition(Point position)
        {
            _lblPosition.Text = $"X: {Util.Round(position.X, 0)}  Y: {Util.Round(position.Y, 0)}";
        }
    }
}