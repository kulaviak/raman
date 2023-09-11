using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Raman.Core
{
    public class Canvas
    {
        private readonly PictureBox _pictureBox;
        
        private static Canvas instance = null;
        
        public Canvas(PictureBox pictureBox)
        {
            _pictureBox = pictureBox;
            _pictureBox.Paint += pictureBox_Paint;
        }
        
        public List<Chart> Charts { get; set; } = new List<Chart>();
        
        public void Refresh()
        {
            Charts.ForEach(x => x.Draw(this));
        }
        
        private void pictureBox_Paint(object sender, PaintEventArgs e)
        {
            // Create a Graphics object from the PictureBox
            Graphics g = e.Graphics;

            // Define the pen for drawing points
            Pen pointPen = new Pen(Color.Red, 3);

            // Draw some points at specific coordinates
            g.DrawRectangle(pointPen, 50, 50, 1, 1); // 
        }
    }
}